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
using FrangouLab.Geneutils.Domain;
using FrangouLab.Geneutils.Domain.Search;

namespace FrangouLab.Geneutils.Service
{
    public class SearchService : ISearchService
    {
        private readonly ISaveQueriesService _saveQueriesService;

        public SearchService(ISaveQueriesService saveQueriesService)
        {
            if (saveQueriesService == null)
                throw new ArgumentNullException(nameof(saveQueriesService));

            _saveQueriesService = saveQueriesService;
        }

        public QueryCollection SelectSavedSearch(File file)
        {
            var queries = _saveQueriesService.OpenFromFile(file);
            return new QueryCollection(queries);
        }

        public async Task Search(ISearchFactory searchFactory)
        {
            await Search(searchFactory, progress => false);
        }

        public async Task Search(ISearchFactory searchFactory, Predicate<float> update)
        {
            await Execute(searchFactory.GeneralSearch, update);
            await Execute(searchFactory.ReferenceSearch, update);
        }

        private static async Task Execute(ISearch search, Predicate<float> update)
        {
            if (search != null)
            {
                update(0);
                await search.Execute(update);
            }
        }
    }
}
