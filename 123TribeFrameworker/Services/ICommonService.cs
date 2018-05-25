using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _123TribeFrameworker.Entity;

namespace _123TribeFrameworker.Services
{
    public interface ICommonService<T> where T:class,new()
    {
        Task<Result<T>> addAsync(T model);
        Pager<List<T>> searchByCondition(Pager<List<T>> pager, T condition);

        Task<T> searchByid(int id);

        Task<Result<int>> deleteByIdAsync(int id);

        Task<Result<int>> update(T model);
    }
}
