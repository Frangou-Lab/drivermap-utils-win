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

using System.Windows.Input;
using FrangouLab.Geneutils.Domain;

namespace FrangouLab.Geneutils.ViewModels.Degisn
{
    public class DesignInputViewModel : IInputViewModel
    {
        public ExtensionCollection Extensions { get; }
        public Extension SelectedExtension { get; set; }
        public File InputFile { get; }
        public File ReferenceFile { get; }
        public ICommand OpenInputFileCommand { get; }
        public ICommand OpenReferenceFileCommand { get; }
        public ICommand RemoveRefereceFileCommand { get; }
        public bool IsReferenceFileSelected { get; }
        public bool IsInputFileSelected { get; }
    }
}
