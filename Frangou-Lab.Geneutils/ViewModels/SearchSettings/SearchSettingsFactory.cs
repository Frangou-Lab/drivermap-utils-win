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
using FrangouLab.Geneutils.Resources;

namespace FrangouLab.Geneutils.ViewModels.SearchSettings
{
    public class SearchSettingsFactory : ISearchSettingsFactory
    {
        public ISearchSettings Create(SearchMode searchMode)
        {
            var settings = CreateSettings(searchMode);
            settings.SearchMode = searchMode;
            return settings;
        }

        private static ISearchSettings CreateSettings(SearchMode searchMode)
        {
            switch (searchMode)
            {
                case SearchMode.SingleQuery:
                    return new SingleQuerySearchSettings();
                case SearchMode.BindingTargets:
                    return new BindingTargetsSearchSettings();
                case SearchMode.PairedQuery:
                case SearchMode.TwoSetSearch:
                case SearchMode.CoupledQueries:
                    return new PairedQuerySearchSettings();
                default:
                    throw new NotSupportedException(Language.SearchSettingsFactoryNotSupportedExceptionMessage);
            }
        }
    }
}
