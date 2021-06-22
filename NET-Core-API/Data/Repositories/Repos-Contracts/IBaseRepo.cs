using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NET_Core_API.Data.Repositories.Repos_Contracts
{
    public interface IBaseRepo<T>
    {
        List<T> GetAll();

        T Get(int id);

        int Save(T item);

        int Delete(T item);
    }
}
