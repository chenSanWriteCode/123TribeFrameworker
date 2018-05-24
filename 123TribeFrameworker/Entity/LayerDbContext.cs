namespace _123TribeFrameworker.Entity
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;

    public class LayerDbContext : DbContext
    {
        //您的上下文已配置为从您的应用程序的配置文件(App.config 或 Web.config)
        //使用“LayerDbContext”连接字符串。默认情况下，此连接字符串针对您的 LocalDb 实例上的
        //“_123TribeFrameworker.Models.LayerDbContext”数据库。
        // 
        //如果您想要针对其他数据库和/或数据库提供程序，请在应用程序配置文件中修改“LayerDbContext”
        //连接字符串。
        public LayerDbContext()
            : base("name=practice")
        {
        }

        //为您要在模型中包含的每种实体类型都添加 DbSet。有关配置和使用 Code First  模型
        //的详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=390109。
        /// <summary>
        /// 订单
        /// </summary>
        public virtual DbSet<OrderInfo> orderInfo { get; set; }
        /// <summary>
        /// 订单详情
        /// </summary>
        public virtual DbSet<OrderDetailInfo> orderDetailInfo { get; set; }
        /// <summary>
        /// 利润记录表
        /// </summary>
        public virtual DbSet<ProfitRecord> profitRecord { get; set; }
        /// <summary>
        /// 交易记录表
        /// </summary>
        public virtual DbSet<TradingRecord> tradingRecord { get; set; }
        /// <summary>
        /// 库存表
        /// </summary>
        public virtual DbSet<Inventory> inventory { get; set; }
        /// <summary>
        /// 单据状态
        /// </summary>
        public virtual DbSet<OrderStatus> orderStatus { get; set; }
        /// <summary>
        /// 物料
        /// </summary>
        public virtual DbSet<MaterialInfo> materialInfos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}