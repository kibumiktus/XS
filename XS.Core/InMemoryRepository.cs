using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using XS.Core.Abstract;
using XS.Core.Model;

namespace XS.Core
{
    public class InMemoryRepository : ISorage
    {
        private Dictionary<string, XSEntity> _storage;        
        private bool _canReturnNullIfNotExists;

        public InMemoryRepository(bool canReturnNullIfNotExists)
        {
            _storage = new Dictionary<string, XSEntity>();
            _canReturnNullIfNotExists = canReturnNullIfNotExists;
        }

        public void Add(XSEntity value)
        {
            _storage.Add(value.Hash, value);
        }

        public void Update(XSEntity value)
        {
            _storage[value.Hash] = value;
        }

        public void Delete(XSEntity value)
        {
            _storage.Remove(value.Hash);
        }

        public IQueryable<XSEntity> GetAll()
        {
            return _storage.Values.AsQueryable();
        }

        public StorageType Type => StorageType.Memory;

        public XSEntity GetByHash(string hash)
        {
            return (_canReturnNullIfNotExists && !_storage.ContainsKey(hash)) ? null : _storage[hash];
        }

        public bool Exists(string hash)
        {
            return _storage.ContainsKey(hash);
        }
    }
}
