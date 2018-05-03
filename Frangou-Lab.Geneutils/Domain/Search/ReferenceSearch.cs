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
using System.Threading.Tasks;
using FrangouLab.Geneutils.Search;
using FrangouLab.Geneutils.ViewModels;
using FrangouLab.Geneutils.ViewModels.SearchSettings;

namespace FrangouLab.Geneutils.Domain.Search
{
    public class ReferenceSearchExecutor : SearchExecutorBase<ReferenceSearch, ReferenceSettings>, IReferenceSearch 
    {
        public ReferenceSearchExecutor(ISearchViewModel searchViewModel) : base(searchViewModel)
        {
            Settings.Input = Copy(searchViewModel.InputViewModel.ReferenceFile.Path);
            Settings.Output = Copy(searchViewModel.InputViewModel.InputFile.Folder);
            Settings.IsOnlyMixedStrainPrimers = IsOnlyMixedStrainPrimers(searchViewModel);
        }

        public override async Task Execute(Predicate<float> progress)
        {
            await Run(() => Search.Search(Settings, progress));
        }

        private static bool IsOnlyMixedStrainPrimers(ISearchViewModel searchViewModel)
        {
            return searchViewModel.SearchModeViewModel.SearchMode == SearchMode.TwoSetSearch;
        }
    }
}