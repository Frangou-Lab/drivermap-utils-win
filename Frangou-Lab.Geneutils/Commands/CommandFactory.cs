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
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using FrangouLab.Geneutils.Domain;
using FrangouLab.Geneutils.Service;
using Prism.Commands;

namespace FrangouLab.Geneutils.Commands
{
    public class CommandFactory : ICommandFactory
    {
        private readonly IDialogService _dialogService;

        public CommandFactory(IDialogService dialogService)
        {
            if (dialogService == null)
                throw new ArgumentNullException(nameof(dialogService));

            _dialogService = dialogService;
        }

        public ICommand Command<TParameter>(Action<TParameter> execute)
        {
            return new DelegateCommand<TParameter>(execute);
        }

        public ICommand Command(Action execute)
        {
            return new DelegateCommand(execute);
        }

        public DelegateCommand Command(Action execute, Func<bool> canExecute)
        {
            return new DelegateCommand(execute, canExecute);
        }

        public DelegateCommand Command(Func<Task> execute)
        {
            return new DelegateCommand(() => execute.Invoke().GetAwaiter());
        }

        public DelegateCommand Command(Func<Task> execute, Func<bool> canExecute)
        {
            return new DelegateCommand(async () => { await execute(); },
                                       () => canExecute());
        }

        public virtual ICommand OpenFileCommand(Action<File> execute)
        {
            return new OpenFileCommand(_dialogService, execute);
        }
        
        public virtual ICommand OpenFileCommand(Action<File> execute, IEnumerable<string> extenions)
        {
            return new OpenFileCommand(_dialogService, execute)
            {
                Extensions = extenions
            };
        }

        public virtual ICommand SaveFileCommand(Action<File> execute)
        {
            return new SaveFileCommand(_dialogService, execute);
        }

        public CompositeCommand CompositeCommand()
        {
            return new CompositeCommand();
        }
    }
}
