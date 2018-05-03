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

using System.Collections.Generic;
using System.Collections.Specialized;

namespace FrangouLab.Geneutils.Domain
{
    public class ObservableQueue<T> : Queue<T>, INotifyCollectionChanged    
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public new virtual T Dequeue()
        {
            var item = base.Dequeue();
            OnCollectionChanged(NotifyCollectionChangedAction.Remove, item);
            return item;
        }

        public new virtual void Enqueue(T item)
        {
            base.Enqueue(item);
            OnCollectionChanged(NotifyCollectionChangedAction.Add, item);
        }

        protected virtual void OnCollectionChanged(NotifyCollectionChangedAction action, T item)
        {
            var args = new NotifyCollectionChangedEventArgs(action, item);
            CollectionChanged?.Invoke(this, args);
        }
    }
}
