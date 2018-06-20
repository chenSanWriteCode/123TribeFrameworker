using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using _123TribeFrameworker.Entity;

namespace _123TribeFrameworker.DAO.BussinessDAO
{
    public class InStorageRecordDAO : IInStorageRecordDAO
    {
        public Task<Result<int>> add(InStorageRecord t)
        {
            throw new NotImplementedException();
        }
        public Task<Result<int>> deleteById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<InStorageRecord> searchById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> update(InStorageRecord t)
        {
            throw new NotImplementedException();
        }
    }
}