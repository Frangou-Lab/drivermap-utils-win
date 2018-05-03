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
using FrangouLab.Geneutils.Domain.Search;
using Prism.Mvvm;

namespace FrangouLab.Geneutils.ViewModels
{
    public class SearchViewModel : BindableBase, ISearchViewModel
    {
        private ISearchQueriesViewModel _searchQueriesViewModel;

        public event EventHandler ValidationChanged;

        public SearchFactory SearchFactory => new SearchFactory(this);

        public IInputViewModel InputViewModel
        {
            get;
        }

        public ISearchModeViewModel SearchModeViewModel
        {
            get;
        }

        public ISearchQueriesViewModel SearchQueriesViewModel
        {
            get { return _searchQueriesViewModel; }
            set
            {
                _searchQueriesViewModel = value;

                if (_searchQueriesViewModel != null)
                    _searchQueriesViewModel.QueriesChanged += SearchCommandRaise;
            }
        }

        public bool IsAcceptReferenceSearch => SearchQueriesIsValid(InputViewModel.IsReferenceFileSelected);

        public bool IsAcceptGeneralSearch => SearchQueriesIsValid(InputViewModel.IsInputFileSelected);

        public SearchViewModel(IInputViewModel inputViewModel, ISearchModeViewModel searchModeViewModel, ISearchQueriesViewModel searchQueriesViewModel)
        {
            if (inputViewModel == null)
                throw new ArgumentNullException(nameof(inputViewModel));

            if (searchModeViewModel == null)
                throw new ArgumentNullException(nameof(searchModeViewModel));

            if (searchQueriesViewModel == null)
                throw new ArgumentNullException(nameof(searchQueriesViewModel));

            InputViewModel = inputViewModel;
            SearchModeViewModel = searchModeViewModel;
            SearchQueriesViewModel = searchQueriesViewModel;
        }

        private void SearchCommandRaise()
        {
            OnPropertyChanged(() => IsAcceptGeneralSearch);
            OnValidationChanged();
        }

        private bool SearchQueriesIsValid(bool isValid) 
        {
            return isValid && SearchQueriesViewModel.IsValid();
        }

        protected virtual void OnValidationChanged()
        {
            ValidationChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
