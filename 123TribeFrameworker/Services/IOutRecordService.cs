using System.Collections.Generic;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.BussinessModels;
using _123TribeFrameworker.Models.QueryModel;

namespace _123TribeFrameworker.Services
{
    public interface IOutRecordService
    {
        Pager<List<OutRecordModel>> searchByCondition(Pager<List<OutRecordModel>> pager, OutRecordQuery t);
    }
}
