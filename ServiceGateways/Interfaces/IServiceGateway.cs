using System.Collections.Generic;
using ServiceGateways.Entities;

namespace ServiceGateways.Interfaces
{
    public interface IServiceGateway<T, K> where T : AbstractEntity
    {
        T Create(T t);
        T Read(K id);
        List<T> ReadAll();
        T Update(T t);
        bool Delete(K id);

    }
}
