using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _123TribeFrameworker.CommonTools
{
    public class CommonTool
    {
    }
    /// <summary>
    /// 柱状图/线图工具类
    /// </summary>
    public class LineDataTool
    {
        public DateTime date { get; set; }
        public float floatData { get; set; }
        public int intData { get; set; }
    }
    /// <summary>
    /// 饼图工具类
    /// </summary>
    public class PieDataTool
    {
        public string label { get; set; }
        public string color { get; set; } = "red";
        public float value { get; set; }
    }
    /// <summary>
    /// 订单工具类
    /// </summary>
    public class OrderTool
    {
        /// <summary>
        /// 产生订单量
        /// </summary>
        public int[] produceOrderNum { get; set; }
        /// <summary>
        /// 异常订单量
        /// </summary>
        public int[] exceptedOrderNum { get; set; }
        /// <summary>
        /// 完成订单量
        /// </summary>
        public int[] completedOrderNum { get; set; }
    }
    /// <summary>
    /// 库存工具类
    /// </summary>
    public class InventoryTool
    {
        /// <summary>
        /// 警戒值
        /// </summary>
        public float[] alarmCount { get; set; }
        /// <summary>
        /// 实际库存
        /// </summary>
        public float[] inventoryCount { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string[] materialName { get; set; }
    }
}