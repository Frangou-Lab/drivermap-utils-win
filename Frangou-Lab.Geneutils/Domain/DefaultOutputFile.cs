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

namespace FrangouLab.Geneutils.Domain
{
    public class DefaultOutputFile : File
    {
        public static File Instance = new DefaultOutputFile();

        private readonly string _file = "Default.csv";
        private readonly string _directory = AppDomain.CurrentDomain.BaseDirectory;

        private DefaultOutputFile() { }

        public override string Name => _file;

        public override string Path => System.IO.Path.Combine(_directory, _file);

        public override string Folder => _directory; 
    }
}
