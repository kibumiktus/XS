using System.Linq;

namespace XS.Core.Abstract
{
    public interface IRepository<T>
    {
        void Add(T value);
        void Update(T value);
        void Delete(T value);        
        IQueryable<T> GetAll();
    }
}