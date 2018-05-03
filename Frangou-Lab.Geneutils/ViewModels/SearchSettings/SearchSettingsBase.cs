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
using System.Runtime.CompilerServices;
using Prism.Mvvm;

namespace FrangouLab.Geneutils.ViewModels.SearchSettings
{
    public abstract class SearchSettingsBase : BindableBase, ISearchSettings
    {
        private bool _isRnaInput;

        public SearchMode SearchMode
        {
            get;
            set;
        }

        public bool IsRnaInput
        {   
            get
            {
                return _isRnaInput;
            }

            set
            {
                SetProperty(ref _isRnaInput, value);
            }
        }

        protected void SetProperty<T>(ref T storage, T value, Action propertyChangedCallback, [CallerMemberName] string propertyName = null)        
        {
            if (SetProperty(ref storage, value, propertyName))
            {
                propertyChangedCallback();
            }
        }

        protected T Default<T>(bool flag, T value, T defaultValue)
        {
            return flag ? value : defaultValue;
        }

        public abstract IDictionary<String, String> Accept(ISettingsVisitor visitor);   
    }
}
