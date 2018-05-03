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
using System.IO;

namespace FrangouLab.Geneutils.Domain
{
    public class File
    {
        protected File() { }

        public File(string name, string path)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (path == null)
                throw new ArgumentNullException(nameof(path));

            Name = name;
            Path = path;
        }

        public virtual string Path
        {
            get; 
        }

        public virtual string Name
        {
            get;
        }

        public virtual string Folder => System.IO.Path.GetDirectoryName(Path);
    }
}
