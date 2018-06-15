using System.Collections.Generic;
using System.Threading.Tasks;
using _123TribeFrameworker.Entity;

namespace _123TribeFrameworker.DAO
{
    public interface ICommonDAO<T> where T:class,new()
    {
        Task<T> searchById(int id);

        Task<Result<int>> add(T t);

        Task<Result<int>> update(T t);

        Task<Result<int>> deleteById(int id);
    }
}
