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
using System.Linq;
using System.Windows.Input;
using FrangouLab.Geneutils.Domain;
using FrangouLab.Geneutils.Service;

namespace FrangouLab.Geneutils.ViewModels
{
    public class InputViewModel : ViewModelBase, IInputViewModel
    {
        private readonly IExtensions _extensions;
        private File _inputFile;
        private File _referenceFile;
        private Extension _selectedExtension;
        
        public ExtensionCollection Extensions
        {
            get;
        }

        public Extension SelectedExtension
        {   
            get
            {
                return _selectedExtension;
            }

            set
            {
                SetProperty(ref _selectedExtension, value);
            }
        }

        public File InputFile
        {
            get
            {
                return _inputFile;
            }

            internal set
            {
                SetProperty(ref _inputFile, value);
                OnPropertyChanged(() => IsInputFileSelected);
            }
        }

        public File ReferenceFile
        {
            get
            {
                return _referenceFile;
            }

            internal set
            {
                SetProperty(ref _referenceFile, value);
                OnPropertyChanged(() => IsReferenceFileSelected);
            }
        }

        public bool IsReferenceFileSelected => _referenceFile != null;

        public bool IsInputFileSelected => _inputFile != null;

        public ICommand OpenInputFileCommand 
            => CommandFactory.OpenFileCommand(OpenInputFileCommandHandler, _extensions.InputFileExtensions);

        public ICommand OpenReferenceFileCommand 
            => CommandFactory.OpenFileCommand(OpenReferenceFileCommandHandler, _extensions.ReferenceFileExteniosns);  
            
        public ICommand RemoveRefereceFileCommand 
            => CommandFactory.Command((Action) RemoveRefereceFileCommandHandler);

        public InputViewModel(IExtensions extensions)               
        {
            if (extensions == null)
                throw new ArgumentNullException(nameof(extensions));

            _extensions = extensions;

            Extensions = new ExtensionCollection(_extensions.DisplayInputFileExtensions);
            SelectedExtension = Extensions.FirstOrDefault();
        }

        private void OpenReferenceFileCommandHandler(File file)
        {
            ReferenceFile = file;
        }

        private void OpenInputFileCommandHandler(File file) 
        {
            InputFile = file;
        }

        private void RemoveRefereceFileCommandHandler()
        {
            ReferenceFile = null;
        }
    }
}
