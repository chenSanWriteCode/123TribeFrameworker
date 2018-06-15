﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.QueryModel;

namespace _123TribeFrameworker.Services
{
    public interface IOrderDetailInfoService:ICommonService<OrderDetailInfo>
    {
        Task<Result<List<OrderDetailInfo>>> addRange(List<OrderDetailInfo> list);
        Pager<List<OrderDetailInfo>> searchByCondition(Pager<List<OrderDetailInfo>> pager, OrderDetailInfoQuery condition);
        List<OrderDetailInfo> searchAllByCondition(OrderDetailInfoQuery condition);
        Task<Result<int>> receiveOrder(List<InStorageRecord> list,string userName);
        Task<Result<int>> deleteByOrderNo(string orderNo);
    }
}
