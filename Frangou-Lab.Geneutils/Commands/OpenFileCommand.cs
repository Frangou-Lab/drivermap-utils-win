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
using FrangouLab.Geneutils.Domain;
using FrangouLab.Geneutils.Service;

namespace FrangouLab.Geneutils.Commands
{
    public class OpenFileCommand : FileCommand 
    {
        private readonly IDialogService _dialogService;
        private IEnumerable<string> _extensions = new List<string>();

        public IEnumerable<string> Extensions
        {
            get
            {
                return _extensions;
            }

            set
            {
                if (value == null)
                    throw new ArgumentNullException();

                _extensions = value;
            }
        }

        public OpenFileCommand(IDialogService dialogService, Action<File> execute) 
            : base(execute)
        {
            if (dialogService == null)
                throw new ArgumentNullException(nameof(dialogService));

            _dialogService = dialogService;
        }

        protected override File OpenFile()
        {
            return _dialogService.OpenFileDialog(_extensions);
        }
    }
}
