#region License

// Copyright 2018 Frangou Lab
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Windows.Input;
using FrangouLab.Geneutils.Domain;
using FrangouLab.Geneutils.Domain.Search;
using FrangouLab.Geneutils.Service;
using Prism.Commands;

namespace FrangouLab.Geneutils.ViewModels
{
    // TODO: SRP violation
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ISearchService _searchService;
        private readonly IDispatcher _dispatcher;
        private readonly ObservableQueue<ISearchFactory> _searchQueue = new ObservableQueue<ISearchFactory>();            

        private File _outputFile;
        private ICommand _openOutputCommand;
        private CompositeCommand _searchCommand;
        private DelegateCommand _addToQueue;
        private DelegateCommand _removeFromQueueCommand;
        private DelegateCommand _generalSearchCommand;
        private ISearchViewModel _searchViewModel;
        private bool _isSearching;
        private int _progress;

        public bool IsSearching
        {
            get
            {
                return _isSearching;
            }

            internal set
            {
                SetProperty(ref _isSearching, value);
            }
        }

        public int Progress    
        {
            get
            {
                return _progress;
            }

            set
            {
                SetProperty(ref _progress, value);
            }  
        }

        public ILoggerViewModel LoggerViewModel { get; }

        public ISearchViewModel SearchViewModel
        {
            get { return _searchViewModel; }
            set
            {
                if (_searchViewModel != value)
                {
                    _searchViewModel = value;
                    _searchViewModel.ValidationChanged += ValidationChangedhandler;
                }
            }
        }

        public int QueueCount => _searchQueue.Count;
        
        public File OutputFile
        {
            get
            {
                return _outputFile ?? (_outputFile = DefaultOutputFile.Instance);
            }

            internal set
            {
                SetProperty(ref _outputFile, value);
            }
        }

        public CompositeCommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                {
                    _searchCommand = new CompositeCommand();
                    _searchCommand.RegisterCommand(GeneralSearchCommand);
                    //_searchCommand.RegisterCommand(LoggerViewModel.GeneralSearchCommand);
                }

                return _searchCommand;
            }
        }

        public ICommand OpenOutputFileCommand
            => Command(ref _openOutputCommand, factory => factory.SaveFileCommand(OpenOutputFileCommandHandler));

        public DelegateCommand GeneralSearchCommand 
            => Command(ref _generalSearchCommand, factory => factory.Command(GeneralSearchCommandHandler, CanGeneralSearchCommandHandler));

        public DelegateCommand AddToQueueCommand
            => Command(ref _addToQueue, factory => factory.Command((Action) AddToQueueCommandHandler, IsValidGeneralSearch));

        public DelegateCommand RemoveFromQueueCommand
            => Command(ref _removeFromQueueCommand, factory => factory.Command((Action)RemoveFromQueueCommandHandler, CanRemoveFromQueueCommandHandler));

        public MainWindowViewModel(ISearchViewModel searchViewModel, ILoggerViewModel loggerViewModel, ISearchService searchService, IDispatcher dispatcher)
        {
            if (searchViewModel == null)    
                throw new ArgumentNullException(nameof(searchViewModel));

            if (loggerViewModel == null)
                throw new ArgumentNullException(nameof(loggerViewModel));

            if (searchService == null)
                throw new ArgumentNullException(nameof(searchService));

            if (dispatcher == null)
                throw new ArgumentNullException(nameof(dispatcher));

            SearchViewModel = searchViewModel;
            LoggerViewModel = loggerViewModel;

            _searchService = searchService;
            _dispatcher = dispatcher;
            _searchQueue.CollectionChanged += OnQueueChanged;
        }

        private void ValidationChangedhandler(object sender, EventArgs e)
        {
            RaiseExecuteChangedForAddButtons();
        }

        private void RaiseExecuteChangedForAddButtons()
        {
            GeneralSearchCommand.RaiseCanExecuteChanged();
            AddToQueueCommand.RaiseCanExecuteChanged();
        }

        private void OpenOutputFileCommandHandler(File file)
        {
            OutputFile = file;
        }

        private bool CanGeneralSearchCommandHandler()
        {
            return IsValidGeneralSearch() || QueueHasCommand();
        }

        private async Task GeneralSearchCommandHandler()
        {
            if (IsSearching)
            {
                await AddToQueue();
            }
            else
            {
                if (QueueHasCommand() || await AddToQueue())
                {
                    await Search();
                }
            }
        }

        private async Task<bool> AddToQueue()
        {
            var canExecute = AddToQueueCommand.CanExecute();
            if (canExecute)
                /* await */ AddToQueueCommand.Execute();

            return canExecute;
        } 

        private async Task Search()
        {   
            try
            {
                IsSearching = true;
                LoggerViewModel.GeneralSearchCommand.Execute(null);

                var currentSearch = DequeueSettings();
                await _searchService.Search(currentSearch, ProgressSearchCallback);
            }
            finally 
            {
                IsSearching = false;
                Progress = 0;
            }
        }

        private bool ProgressSearchCallback(float progress)    
        {
            _dispatcher.BeginInvoke(() =>
            {
                Progress = (int)progress;
            });

            return false;
        }

        private void AddToQueueCommandHandler()
        {
            var search = SearchViewModel.SearchFactory;
            search.SetOutput(OutputFile);

            _searchQueue.Enqueue(search);   
        }

        private void RemoveFromQueueCommandHandler()
        {
            DequeueSettings();
        }

        private void OnQueueChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(() => QueueCount);

            RaiseExecuteChangedForAddButtons();
            RemoveFromQueueCommand.RaiseCanExecuteChanged();
        }

        private bool CanRemoveFromQueueCommandHandler()
        {
            return QueueHasCommand();
        }

        private bool QueueHasCommand()
        {
            return _searchQueue.Count > 0;
        }

        private bool IsValidGeneralSearch()
        {
            return SearchViewModel.IsAcceptGeneralSearch;
        }

        private ISearchFactory DequeueSettings()
        {
            return _searchQueue.Dequeue();
        }
    }
}
