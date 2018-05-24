using System.Collections.Generic;
using System.Threading.Tasks;
using _123TribeFrameworker.Entity;

namespace _123TribeFrameworker.DAO
{
    public interface ICommonDAO<T> where T:class,new()
    {
        List<T> searchByCondition(Pager<List<T>> pager, T t);

        int searchCountByCondition(T t);

        T searchById(int id);

        Task<int> add(T t);

        Task<int> update(T t);

        Task<int> deleteById(int id);
    }
}
