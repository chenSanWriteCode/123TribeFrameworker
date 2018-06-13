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
        public Task<int> add(InStorageRecord t)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<int>> addRange(List<InStorageRecord> list)
        {
            Result<int> result = new Result<int>();
            try
            {
                LayerDbContext context = new LayerDbContext();
                if (list.Count > 0)
                {
                    context.inStorageRecord.AddRange(list);
                }
                result.data = await context.SaveChangesAsync();
            }
            catch (Exception err)
            {
                result.addError(err.Message);
            }
            return result;
        }

        public Task<int> deleteById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<InStorageRecord> searchById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> update(InStorageRecord t)
        {
            throw new NotImplementedException();
        }
    }
}