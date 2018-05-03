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
using Prism.Commands;

namespace FrangouLab.Geneutils.Commands
{
    public interface ICommandFactory
    {
        ICommand Command(Action execute);

        ICommand Command<TParameter>(Action<TParameter> execute);

        DelegateCommand Command(Action execute, Func<bool> canExecute);

        DelegateCommand Command(Func<Task> execute);

        DelegateCommand Command(Func<Task> execute, Func<bool> canExecute);
        
        ICommand OpenFileCommand(Action<File> execute, IEnumerable<string> extenions);
            
        ICommand OpenFileCommand(Action<File> execute);

        ICommand SaveFileCommand(Action<File> execute);

        CompositeCommand CompositeCommand();
    }
}