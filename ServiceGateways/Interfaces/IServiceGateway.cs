using System.Collections.Generic;
using ServiceGateways.Entities;

namespace ServiceGateways.Interfaces
{
    public interface IServiceGateway<T, TK> where T : AbstractEntity
    {
        T Create(T t);
        T Read(TK id);
        List<T> ReadAll();
        T Update(T t);
        bool Delete(TK id);

    }
}
