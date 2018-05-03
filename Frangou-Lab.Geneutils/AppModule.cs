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

using Autofac;
using FrangouLab.Geneutils.Commands;
using FrangouLab.Geneutils.Extensions;
using FrangouLab.Geneutils.Service;
using FrangouLab.Geneutils.ViewModels;
using FrangouLab.Geneutils.ViewModels.SearchSettings;

namespace FrangouLab.Geneutils
{
    public class AppModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterSingleton<ISearchService, SearchService>();
            builder.RegisterSingleton<ISearchOutput, SearchOutput>();
            builder.RegisterSingleton<ISearchSettingsFactory, SearchSettingsFactory>();
            builder.RegisterSingleton<ISettingsVisitor, SettingsVisitor>();
            builder.RegisterSingleton<IDialogFilterFactory, DialogFilterFactory>();
            builder.RegisterSingleton<IDialogService, DialogService>();
            builder.RegisterSingleton<ICommandFactory, CommandFactory>();
            builder.RegisterSingleton<IExtensions, Service.Extensions>();
            builder.RegisterSingleton<IDispatcher, Dispatcher>();
            builder.RegisterSingleton<ISaveQueriesService, SaveQueriesService>();

            builder.RegisterView<ISearchViewModel, SearchViewModel>();
            builder.RegisterView<ILoggerViewModel, LoggerViewModel>();
            builder.RegisterView<IInputViewModel, InputViewModel>();
            builder.RegisterView<ISearchQueriesViewModel, SearchQueriesViewModel>();
            builder.RegisterView<ISearchModeViewModel, SearchModeViewModel>();

            builder.RegisterView<MainWindowViewModel>();
        }
    }
}
