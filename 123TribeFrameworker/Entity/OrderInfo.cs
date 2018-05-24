using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _123TribeFrameworker.Entity
{
    [Table("Layer_OrderInfo")]
    /// <summary>
    /// 业务_订单
    /// </summary>
    public class OrderInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        [MaxLength(50)]
        [Required]
        [Key]
        public string orderNo { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        [Required]
        public int status { get; set; }
        /// <summary>
        /// 合计进货价格
        /// </summary>
        public double sumPrice { get; set; }
        /// <summary>
        /// 是否打印
        /// </summary>
        [DefaultValue(0)]
        public int isPrinted { get; set; }
        /// <summary>
        /// 打印时间
        /// </summary>
        public DateTime? printedTime { get; set; }
        /// <summary>
        /// 打印次数
        /// </summary>
        [DefaultValue(0)]
        public int printedNum { get; set; }
        [MaxLength(20)]
        public string createdBy { get; set; }
        /// <summary>
        /// 进货单下发时间
        /// </summary>
        public DateTime createdDate { get; set; } = DateTime.Now;
        public DateTime? lastUpdatedDate { get; set; }
        [MaxLength(20)]
        public string lastUpdatedBy { get; set; }
        /// <summary>
        /// 收货时间
        /// </summary>
        public DateTime? receivedDate { get; set; }
        [ForeignKey("status")]
        public virtual OrderStatus orderStatus { get; set; }
    }
    /// <summary>
    /// 业务_订单详情
    /// </summary>
    [Table("Layer_OrderDetailInfo")]
    public class OrderDetailInfo
    {
        [Key, Column(Order = 1)]
        public int id { get; set; }
        [Required]
        [Key, Column(Order = 2)]
        [MaxLength(50)]
        public string orderNo { get; set; }
        /// <summary>
        /// 物料id
        /// </summary>
        [Required]
        public int materialId { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string materialName { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int num { get; set; }
        /// <summary>
        /// 供货商
        /// </summary>
        [MaxLength(50)]
        public string factory { get; set; }

        public DateTime createdDate { get; set; } = DateTime.Now;
        public string createdBy { get; set; }
        public DateTime? lastUpdatedDate { get; set; }
        public string lastUpdatedBy { get; set; }
        [ForeignKey("orderNo")]
        public virtual OrderInfo orderInfo { get; set; }
        [ForeignKey("materialId")]
        public virtual MaterialInfo materialInfo { get; set; }
    }
    /// <summary>
    /// 收益记录表
    /// </summary>
    [Table("Layer_ProfitRecord")]
    public class ProfitRecord
    {
        [Key]
        public int id { get; set; }
        /// <summary>
        /// 交易记录id
        /// </summary>
        public int recordId { get; set; }
        /// <summary>
        /// 成本价
        /// </summary>
        public double priceIn { get; set; }
        /// <summary>
        /// 零售价
        /// </summary>
        public double priceOut { get; set; }
        /// <summary>
        /// 利润
        /// </summary>
        public double profit { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createdDate { get; set; } = DateTime.Now;
        [ForeignKey("recordId")]
        public TradingRecord record { get; set; }
    }
    /// <summary>
    /// 交易记录
    /// </summary>
    [Table("Layer_TradingRecord")]
    public class TradingRecord
    {
        [Key]
        public int id { get; set; }
        /// <summary>
        /// 物料id
        /// </summary>
        public int materialId { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int count { get; set; } = 1;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createdDate { get; set; } = DateTime.Now;
        /// <summary>
        /// 交易人
        /// </summary>
        public string createdBy { get; set; }
        [ForeignKey("materialId")]
        public MaterialInfo materialInfo { get; set; }
    }
    /// <summary>
    /// 库存 后期可以多长时间一删
    /// </summary>
    [Table("Layer_Inventory")]
    public class Inventory
    {
        [Key]
        public int id { get; set; }
        /// <summary>
        /// 物料id
        /// </summary>
        [Required]
        [Index("UI_priceInAndmaterialId", 1, IsUnique = true)]
        public int materialId { get; set; }
        /// <summary>
        /// 成本价
        /// </summary>
        [Required]
        [Index("UI_priceInAndmaterialId",2,IsUnique =true)]
        public double priceIn { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [Required]
        public int count { get; set; }
        /// <summary>
        /// 回执单编号
        /// </summary>
        [MaxLength(20)]
        public string receivedOrder { get; set; }
        /// <summary>
        /// 入库时间
        /// </summary>
        public DateTime createdDate { get; set; } = DateTime.Now;
    }
    /// <summary>
    /// 基础表_订单状态
    /// </summary>
    [Table("Base_OrderStatus")]
    public class OrderStatus
    {
        [Key]
        public int id { get; set; }
        [MaxLength(20)]
        public string status { get; set; }
    }
    /// <summary>
    /// 基础表_物料
    /// </summary>
    [Table("Base_MaterialInfo")]
    public class MaterialInfo
    {
        [Key]
        public int id { get; set; }
        /// <summary>
        /// 产品编码
        /// </summary>
        [MaxLength(20)]
        public string material { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        [MaxLength(50)]
        [Required]
        [Index(IsUnique = true)]
        public string materialName { get; set; }
        /// <summary>
        /// 别名
        /// </summary>
        [MaxLength(50)]
        public string alias { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        [MaxLength(20)]
        public string mat_type { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        [MaxLength(20)]
        public string mat_size { get; set; }
        /// <summary>
        /// 颜色
        /// </summary>
        [MaxLength(20)]
        public string mat_color { get; set; }
        /// <summary>
        /// 重量
        /// </summary>
        public double weight { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [MaxLength(10)]
        public string unit { get; set; }
        /// <summary>
        /// 参考进货价格
        /// </summary>
        [Required]
        public double referencePriceIn { get; set; }
        /// <summary>
        /// 参考销售价格
        /// </summary>
        public double referencePriceOut { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(200)]
        public string remark { get; set; }
    }
}