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
using System.Collections.ObjectModel;
using System.Linq;

namespace FrangouLab.Geneutils.Domain
{
    public class QueryCollection : ObservableCollection<Query>
    {
        public QueryCollection()
        {
            
        }

        public QueryCollection(IEnumerable<string> queries)
        {
            if (queries == null)
                throw new ArgumentNullException(nameof(queries));

            foreach (var query in queries)
                Add(query);
        }

        public void Add(string query)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            var typedQuery = new Query(query);
            Add(typedQuery);
        }

        public string[] ConvertToStringArray()  
        {
            return Items
                .Select(item => String.Copy(item.Item))
                .ToArray();
        }

        public bool IsEmpty()   
        {
            return Count == 0;
        }
    }
}
