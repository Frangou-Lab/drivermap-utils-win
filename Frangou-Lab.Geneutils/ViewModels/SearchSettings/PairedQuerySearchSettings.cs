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
    public class PairedQuerySearchSettings : SingleQuerySearchSettings
    {
        public const int DefaultContextLength = 5;
        public const int DefaultLimitAmpliconLengthMin = 75;
        public const int DefaultLimitAmpliconLengthMax = 350;

        private bool _сontextLength;
        private bool _isLimitAmpliconLength;
        private int _contextLengthCount;
        private int _limitAmpliconLengthMin;
        private int _limitAmpliconLengthMax;

        public bool IsContextLength
        {   
            get
            {
                return _сontextLength;
            }

            set
            {
                SetProperty(ref _сontextLength, value, () =>
                {
                    OnPropertyChanged(nameof(ContextLength));
                });
            }
        }

        public int ContextLength       
        {
            get
            {
                return Default(IsContextLength, _contextLengthCount, DefaultContextLength);
            }

            set
            {
                SetProperty(ref _contextLengthCount, value);
            }
        }

        public bool IsLimitAmpliconLength   
        {
            get
            {
                return _isLimitAmpliconLength;
            }

            set
            {
                SetProperty(ref _isLimitAmpliconLength, value, () =>
                {
                    OnPropertyChanged(nameof(LimitAmpliconLengthMin));
                    OnPropertyChanged(nameof(LimitAmpliconLengthMax));
                });
            }
        }

        public int LimitAmpliconLengthMin
        {
            get
            {
                return Default(IsLimitAmpliconLength, _limitAmpliconLengthMin, DefaultLimitAmpliconLengthMin);
            }

            set
            {
                SetProperty(ref _limitAmpliconLengthMin, value);
            }
        }

        public int LimitAmpliconLengthMax   
        {
            get
            {
                return Default(IsLimitAmpliconLength, _limitAmpliconLengthMax, DefaultLimitAmpliconLengthMax);
            }

            set
            {
                SetProperty(ref _limitAmpliconLengthMax, value);
            }
        }

        public override IDictionary<String, String> Accept(ISettingsVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
