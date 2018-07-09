using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.BussinessModels;

namespace _123TribeFrameworker.DAO.BussinessDAO
{
    public class SaleDAO : ISaleDAO
    {
        /// <summary>
        /// 收货
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        /// <remarks>
        /// 1. 判断库存是否有货:不需要判断，在第二步会顺带判断
        /// 2. 按照入库时间正序排列，减库存
        /// 3. 增加交易记录与收益记录
        /// </remarks>
        public async Task<Result<string>> receive(List<SaleModel> list)
        {
            Result<string> result = new Result<string>();
            try
            {
                using (LayerDbContext context = new LayerDbContext())
                {
                    //2. 按照入库时间正序排列，减库存
                    List<Inventory> inventoryList = null;
                    foreach (var item in list)
                    {
                        inventoryList = context.inventory.Where(x => x.materialId == item.materialId).OrderBy(x => x.createdDate).ToList();
                        //卖出数量
                        float saleCount = item.count;
                        //成本价综合
                        float sumPriceIn = 0;
                        for (int i = 0; i < inventoryList.Count; i++)
                        {
                            if (inventoryList[i].count > saleCount)
                            {

                                inventoryList[i].count -= saleCount;
                                sumPriceIn += inventoryList[i].priceIn * saleCount;
                                saleCount = 0;
                                break;
                            }
                            else
                            {
                                saleCount -= inventoryList[i].count;
                                sumPriceIn += inventoryList[i].priceIn * inventoryList[i].count;
                                context.inventory.Remove(inventoryList[i]);
                            }
                        }
                        //saleCount !=0 说明 库存货不够了，需要报异常，让客户重新选择下单
                        if (saleCount!=0)
                        {
                            throw new Exception(item.materialName+" 库存不足,仅剩余"+(item.count-saleCount).ToString());
                        }
                        //3.1增加交易记录
                        TradingRecord trade = new TradingRecord
                        {
                            cashOrder = item.cashOrder,
                            count = item.count,
                            createdBy = item.createdBy,
                            createdDate = DateTime.Now,
                            materialId = item.materialId
                        };
                        context.tradingRecord.Add(trade);
                        //3.2 增加收益记录
                        ProfitRecord profit = new ProfitRecord
                        {
                            createdDate = trade.createdDate,
                            priceIn = sumPriceIn,
                            priceOut = item.priceOut,
                            profit = item.priceOut - sumPriceIn,
                            cashOrder=item.cashOrder,
                            materialId=item.materialId
                        };
                        context.profitRecord.Add(profit);
                    }
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception err)
            {
                result.addError(err.Message);
            }
            return result;
        }


        #region system-provided

        public Task<Result<int>> add(SaleModel t)
        {
            throw new NotImplementedException();
        }
        public Task<Result<int>> deleteById(int id)
        {
            throw new NotImplementedException();
        }



        public Task<SaleModel> searchById(int id)
        {
            throw new NotImplementedException();
        }
        public Task<Result<int>> update(SaleModel t)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}