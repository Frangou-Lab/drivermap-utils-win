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
using Microsoft.Win32;

namespace FrangouLab.Geneutils.Service
{
    internal class DialogService : IDialogService   
    {
        private readonly IDialogFilterFactory _dialogFilterFactory;

        public DialogService(IDialogFilterFactory dialogFilterFactory)
        {
            if (dialogFilterFactory == null)
                throw new ArgumentNullException(nameof(dialogFilterFactory));

            _dialogFilterFactory = dialogFilterFactory;
        }

        public File OpenFileDialog(IEnumerable<string> extensions)  
        {
            if (extensions == null)
                throw new ArgumentNullException(nameof(extensions));

            var filter = _dialogFilterFactory.Create(extensions);
            return ShowDialog<OpenFileDialog>(filter);
        }

        public File SaveFileDialog()
        {
            return ShowDialog<SaveFileDialog>();
        }

        private static File ShowDialog<TDialog>(string filter = null) where TDialog : FileDialog, new()
        {
            var dialog = new TDialog {
                Filter = filter
            };

            var isSelected = dialog.ShowDialog();
            if (isSelected == true)
            {
                return ConvertToFile(dialog);
            }

            return null;
        }

        private static File ConvertToFile(FileDialog dialog)
        {
            return new File(dialog.SafeFileName, dialog.FileName);
        }
    }
}
