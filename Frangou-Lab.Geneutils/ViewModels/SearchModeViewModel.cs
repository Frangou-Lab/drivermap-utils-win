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
using FrangouLab.Geneutils.Extensions;
using FrangouLab.Geneutils.ViewModels.SearchSettings;

namespace FrangouLab.Geneutils.ViewModels
{
    public class SearchModeViewModel : ViewModelBase, ISearchModeViewModel
    {
        private SearchMode _searchMode = SearchMode.SingleQuery;

        private readonly ISearchSettingsFactory _settingsFactory;
        private readonly ISettingsVisitor _settingsVisitor;
        private readonly Dictionary<SearchMode, ISearchSettings> _settingCache = new Dictionary<SearchMode, ISearchSettings>();

        public SearchMode SearchMode
        {
            get
            {
                return _searchMode;
            }

            set
            {
                if (_searchMode == value)
                    return;

                _searchMode = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(Settings));
            }
        }

        public ISearchSettings Settings
        {
            get
            {
                return _settingCache
                    .GetOrSet(SearchMode, key => _settingsFactory.Create(key));
            }
        }

        public IDictionary<String, String> Parameters => Settings.Accept(_settingsVisitor);

        public SearchModeViewModel(ISearchSettingsFactory settingsFactory, ISettingsVisitor settingsVisitor)
        {
            if (settingsFactory == null)
                throw new ArgumentNullException(nameof(settingsFactory));

            if (settingsVisitor == null)
                throw new ArgumentNullException(nameof(settingsVisitor));

            _settingsFactory = settingsFactory;
            _settingsVisitor = settingsVisitor;
        }
    }
}
