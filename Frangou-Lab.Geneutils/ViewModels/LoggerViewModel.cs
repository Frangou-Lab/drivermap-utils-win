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
using System.Windows.Input;
using FrangouLab.Geneutils.Service;

namespace FrangouLab.Geneutils.ViewModels
{
    public class LoggerViewModel : ViewModelBase, IDisposable, ILoggerViewModel
    {
        private string _log;

        private readonly IDispatcher _dispatcher;
        private ISearchOutput _searchOutput;
        private ICommand _generalSearchCommand;

        public string Log
        {
            get
            {
                return _log;
            }

            set
            {
                SetProperty(ref _log, value);
            }
        }

        private ISearchOutput SearchOutput
        {
            get { return _searchOutput; }
            set
            {
                if (_searchOutput == value)
                    return;

                _searchOutput = value;
                _searchOutput.RecieveMessage += RecieveMessage;
            }
        }

        public ICommand GeneralSearchCommand 
            => Command(ref _generalSearchCommand, factory => factory.Command((Action) Clean));

        public LoggerViewModel(IDispatcher dispatcher, ISearchOutput searchOutput)
        {
            if (dispatcher == null)
                throw new ArgumentNullException(nameof(dispatcher));

            if (searchOutput == null)
                throw new ArgumentNullException(nameof(searchOutput));
                
            _dispatcher = dispatcher;
            SearchOutput = searchOutput;
        }
        
        private void RecieveMessage(object sender, string message)
        {
            _dispatcher.BeginInvoke(() => Add(message));
        }

        private void Add(string message)
        {
            Log += message;
        }

        private void Clean()
        {
            Log = String.Empty;
        }

        public void Dispose()
        {
            if (SearchOutput != null)
            {
                SearchOutput.RecieveMessage -= RecieveMessage;
            }
        }
    }
}
