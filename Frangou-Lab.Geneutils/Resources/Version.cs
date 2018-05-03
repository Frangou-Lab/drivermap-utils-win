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

using System.Diagnostics;

namespace FrangouLab.Geneutils.Resources
{
    public class Version
    {
        private static string _current;

        public static string Current => _current ?? (_current = GetProductVersion());

        private static string GetProductVersion()
        {
            var assembly = GetCurrentAssemblyFileName();
            var versionInfo = FileVersionInfo.GetVersionInfo(assembly);

            return versionInfo.ProductVersion;
        }

        private static string GetCurrentAssemblyFileName()
        {
            var assembly = GetCurrentAssembly();
            return assembly.Location;
        }

        private static System.Reflection.Assembly GetCurrentAssembly()
        {
            return System.Reflection.Assembly.GetExecutingAssembly();
        }
    }
}
