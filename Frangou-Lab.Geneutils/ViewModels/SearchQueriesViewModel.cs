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
using System.Collections.ObjectModel;
using System.Windows.Input;
using FrangouLab.Geneutils.Domain;
using FrangouLab.Geneutils.Service;
using Prism.Commands;

namespace FrangouLab.Geneutils.ViewModels
{
    public class SearchQueriesViewModel : ViewModelBase, ISearchQueriesViewModel
    {
        private readonly ISearchService _searchService;
        private readonly IExtensions _extensions;

        private DelegateCommand _clearQueriesCommand;
        private ICommand _openSavedSearchCommand;
        private ICommand _addQueryCommand;

        public QueryCollection Queries { get; } = new QueryCollection();

        public event Action QueriesChanged;

        public DelegateCommand ClearQueriesCommand 
            => Command(ref _clearQueriesCommand, factory => factory.Command((Action) ClearQueries, IsValid));

        public ICommand OpenSavedSearchCommand 
            => Command(ref _openSavedSearchCommand, factory => factory.OpenFileCommand(OpenSavedSearchCommandHandler, _extensions.InputQueriesFileExteniosns)); 

        public ICommand AddQueryCommand 
            => Command(ref _addQueryCommand, factory => factory.Command<String>(AddQueryCommandHandler)); 

        public SearchQueriesViewModel(ISearchService searchService, IExtensions extensions)
        {
            if (searchService == null)
                throw new ArgumentNullException(nameof(searchService));

            if (extensions == null)
                throw new ArgumentNullException(nameof(extensions));

            _searchService = searchService;
            _extensions = extensions;

            Queries.CollectionChanged += (sender, args) =>
            {
                ClearQueriesCommand.RaiseCanExecuteChanged();
                RaiseQueriesChanged();
            };
        }

        private void OpenSavedSearchCommandHandler(File file)
        {
            var savedQueries = _searchService.SelectSavedSearch(file);

            ClearQueries();
            Queries.AddRange(savedQueries);
        }

        private void ClearQueries()
        {
            Queries.Clear();
        }

        private void AddQueryCommandHandler(String query)
        {
            Queries.Add(query);
        }

        public override bool IsValid()
        {
            return !Queries.IsEmpty();
        }

        private void RaiseQueriesChanged()
        {
            QueriesChanged?.Invoke();
        }
    }
}
