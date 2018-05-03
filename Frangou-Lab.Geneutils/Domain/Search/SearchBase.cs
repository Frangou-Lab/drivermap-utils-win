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
using FrangouLab.Geneutils.ViewModels;

namespace FrangouLab.Geneutils.Domain.Search
{   
    public abstract class SearchExecutorBase<TSearch, TSettings> : ISearch
        where TSearch : new() 
        where TSettings : Geneutils.Search.Settings, new()
    {
        protected TSearch Search = new TSearch();

        protected TSettings Settings = new TSettings();

        public abstract Task Execute(Predicate<float> progress);

        protected SearchExecutorBase(ISearchViewModel searchViewModel)
        {
            Settings.Queries = searchViewModel.SearchQueriesViewModel.Queries.ConvertToStringArray();
        }

        protected static string Copy(string str)
        {
            return str == null ? null : String.Copy(str);
        }

        protected async Task Run(Action action)
        {
            await Task.Run(action);
        }
    }
}
