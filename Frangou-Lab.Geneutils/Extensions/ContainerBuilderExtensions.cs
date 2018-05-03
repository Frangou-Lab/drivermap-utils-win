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

namespace FrangouLab.Geneutils.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterSingleton<TDependency, TImplementation>(this ContainerBuilder builder)     
        {
            builder
                .RegisterType<TImplementation>()
                .As<TDependency>()
                .SingleInstance();
        }

        public static void RegisterView<TDependency, TImplementation>(this ContainerBuilder builder)    
        {
            builder
                .RegisterType<TImplementation>()
                .As<TDependency>()
                .AsSelf()
                .PropertiesAutowired(new DependencyAttributePropertySelector());
        }

        public static void RegisterView<TImplementation>(this ContainerBuilder builder)
        {
            builder
                .RegisterType<TImplementation>()
                .AsSelf()
                .PropertiesAutowired(new DependencyAttributePropertySelector());
        }
    }
}
