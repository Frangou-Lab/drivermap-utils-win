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

namespace FrangouLab.Geneutils.Domain.Search
{
    public class GeneralSearchExecutor : SearchExecutorBase<GeneralSearch, GeneralSettings>, IGeneralSearch 
    {
        public string OutputFile
        {
            get
            {
                return Settings.Output;
            }

            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentNullException();

                Settings.Output = value;
            }
        }

        public GeneralSearchExecutor(ISearchViewModel searchViewModel) : base(searchViewModel)
        {
            if (searchViewModel == null)
                throw new ArgumentNullException(nameof(searchViewModel));

            Settings.Input = new[] { Copy(searchViewModel.InputViewModel.InputFile.Path) };
            Settings.Parameters = searchViewModel.SearchModeViewModel.Parameters;

            var extension = searchViewModel.InputViewModel.SelectedExtension.Value;
            if (extension != null)
                AddExtension(extension);
        }

        private void AddExtension(string extension)
        {
            var key = SearchFlags.kInputFormat;

            if (Settings.Parameters.ContainsKey(key))
            {
                Settings.Parameters[key] = extension;
            }
            else
            {
                Settings.Parameters.Add(key, extension);
            }
        }

        public override async Task Execute(Predicate<float> progress)
        {
            await Run(() => Search.Search(Settings, progress));
        }
    }
}
