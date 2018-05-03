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

namespace FrangouLab.Geneutils.ViewModels.SearchSettings
{
    public class SingleQuerySearchSettings : SearchSettingsBase    
    {
        public const int DefaultMismatchesCount = 1;   

        private bool _isSearchBothStrands;
        private bool _isAllowMismatches;
        private int _mismatchesCount;

        public bool IsSearchBothStrands   
        {   
            get
            {
                return _isSearchBothStrands;
            }

            set
            {
                SetProperty(ref _isSearchBothStrands, value);
            }
        }

        public bool IsAllowMismatches
        {   
            get
            {
                return _isAllowMismatches;    
            }

            set
            {
                SetProperty(ref _isAllowMismatches, value, () =>
                {
                    OnPropertyChanged(nameof(Mismatches));
                });
            }
        }

        public int Mismatches   
        {
            get
            {
                return Default(IsAllowMismatches, _mismatchesCount, DefaultMismatchesCount);
            }

            set
            {
                SetProperty(ref _mismatchesCount, value);   
            }
        }

        public override IDictionary<String, String> Accept(ISettingsVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
