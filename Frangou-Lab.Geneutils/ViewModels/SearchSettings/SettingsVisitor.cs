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
using FrangouLab.Geneutils.Search;

namespace FrangouLab.Geneutils.ViewModels.SearchSettings
{
    public class SettingsVisitor : ISettingsVisitor
    {  
        public IDictionary<string, string> Visit(SingleQuerySearchSettings model)
        {
            return Create(model, settings =>
            {
                settings.Add(model.IsAllowMismatches, SearchFlags.kMismatchesEnabled, model.Mismatches);
                settings.Add(model.IsSearchBothStrands, SearchFlags.kForwardAndReverseComplementsSearch);
            }); 
        }

        public IDictionary<string, string> Visit(PairedQuerySearchSettings model)
        {
            var settings = (Settings) Visit((SingleQuerySearchSettings) model);

            settings.Add(model.IsContextLength, SearchFlags.kContextEnabled, model.ContextLength);
            settings.Add(model.IsLimitAmpliconLength, SearchFlags.kMaxAmpliconSize, model.LimitAmpliconLengthMax);
            settings.Add(model.IsLimitAmpliconLength, SearchFlags.kMinAmpliconSize, model.LimitAmpliconLengthMin);

            return settings;
        }

        public IDictionary<string, string> Visit(BindingTargetsSearchSettings model)
        {
            return Create(model, settings =>
            {
                settings.Add(model.IsRnaPrimers, SearchFlags.kRnaPrimersSearch);
            });
        }

        private static Settings Create(SearchSettingsBase searchSettingsBase, Action<Settings> fillSettings)
        {   
            var settings = new Settings
            {
                { searchSettingsBase.IsRnaInput, SearchFlags.kRnaSequenceSearch }
            };

            SetSearchMode(settings, searchSettingsBase);
            fillSettings(settings);
            return settings;
        }

        private static void SetSearchMode(Settings settings, ISearchSettings searchSettingsBase)
        {
            var searchMode = GetSearchMode(searchSettingsBase.SearchMode);
            if (searchMode != null)
            {
                settings.Add(String.Copy(searchMode), Settings.AcceptFlag);
            }
        }

        private static string GetSearchMode(SearchMode searchMode)  
        {       
            switch (searchMode)
            {
                case SearchMode.PairedQuery:
                    return SearchFlags.kPairedQueryExtraction;
                case SearchMode.TwoSetSearch:
                    return SearchFlags.kMixedStrainPairedPrimerSearch;
                case SearchMode.BindingTargets:
                    return SearchFlags.kSearchBindingTargets;
                case SearchMode.CoupledQueries:
                    return SearchFlags.kCoupledQueries;
            }

            return null;
        }
    }
}
