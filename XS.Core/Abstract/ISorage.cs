using XS.Core.Model;

namespace XS.Core.Abstract
{
    public interface ISorage : IRepository<XSEntity>
    {
        StorageType Type { get; }
        XSEntity GetByHash(string hash);
        bool Exists(string hash);
    }

    public enum StorageType
    {
        Memory,
        DataBase,
        ExternalService
    }
}