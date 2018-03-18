using System;
using System.Collections.Generic;
using System.Linq;
using XS.Core.Abstract;
using XS.Core.Model;

namespace XS.Core
{
    public class XS_Service
    {
        private Settings _settings;
        private List<ISorage> _repositories;
        private readonly Generator _generator;

        private ISorage InMemoryReposity =>
            _repositories.FirstOrDefault(x => x.Type == StorageType.Memory);


        public XS_Service(Settings settings)
        {
            _settings = settings;
            _repositories = new List<ISorage>();
            _generator = new Generator(settings);
            Register(new InMemoryRepository(true));
        }

        public string this[string index]
        {
            get => Get(index);
            set => Store(index);
        }

        public void Register(ISorage _repository)
        {
            _repositories.Add(_repository);
        }

        public string Store(string value)
        {
            var key = _generator.Generate(x=>!Exists(x));
            var entity = new XSEntity {Hash = key, Value = value};
            _repositories.ForEach(x=>x.Add(entity));
            return key;
        }

        private bool Exists(string value)
        {            
            return InMemoryReposity.Exists(value) || _repositories.Any(x=> x.Exists(value));
        }

        public string Get(string key)
        {
            Func<ISorage, XSEntity> get = item => item.GetByHash(key);
            var result = get(InMemoryReposity) ?? _repositories.Select(get).FirstOrDefault(x => x != null);
            return result.Value;
        }

        public void Update()
        {
            var items = _repositories.SelectMany(x => x.GetAll()).AsQueryable();
            foreach (var item in items)
            {
                foreach (var repo in _repositories)
                {
                    if (repo.Exists(item.Hash))
                        continue;
                    repo.Add(item);
                }                 
            }
        }
    }
}
