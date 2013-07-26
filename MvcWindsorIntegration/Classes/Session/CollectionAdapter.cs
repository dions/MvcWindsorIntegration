using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MvcWindsorIntegration.Classes.Session
{
    public class CollectionAdapter<T> : ICollection<T>, ICollection
    {
        readonly ICollection _collection;

        public void CopyTo(Array array, int index)
        {
            _collection.CopyTo(array, index);
        }

        public object SyncRoot
        {
            get { return _collection.SyncRoot; }
        }

        public bool IsSynchronized
        {
            get { return _collection.IsSynchronized; }
        }

        public CollectionAdapter(ICollection collection)
        {
            this._collection = collection;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _collection.Cast<T>().GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public void Add(T item)
        {
            throw new NotSupportedException();
        }

        public void Clear()
        {
            throw new NotSupportedException();
        }

        public bool Contains(T item)
        {
            return _collection.Cast<T>().Any(x => Equals(x, item));
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _collection.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            throw new NotSupportedException();
        }

        public int Count
        {
            get { return _collection.Count; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }
    }

    public class DictionaryEnumerator<TKey, TValue> : IDictionaryEnumerator, IDisposable
    {
        readonly IEnumerator<KeyValuePair<TKey, TValue>> _enumerator;

        public DictionaryEnumerator(IEnumerator<KeyValuePair<TKey, TValue>> enumerator)
        {
            this._enumerator = enumerator;
        }

        public void Dispose()
        {
            _enumerator.Dispose();
        }

        public bool MoveNext()
        {
            return _enumerator.MoveNext();
        }

        public void Reset()
        {
            _enumerator.Reset();
        }

        public object Current
        {
            get { return _enumerator.Current; }
        }

        public object Key
        {
            get { return _enumerator.Current.Key; }
        }

        public object Value
        {
            get { return _enumerator.Current.Value; }
        }

        public DictionaryEntry Entry
        {
            get { return new DictionaryEntry(Key, Value); }
        }
    }
}
