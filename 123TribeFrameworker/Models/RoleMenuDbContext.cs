namespace _123TribeFrameworker.Models
{
    using System.Data.Entity;
    using Entity;

    public class RoleMenuDbContext : DbContext
    {
        //您的上下文已配置为从您的应用程序的配置文件(App.config 或 Web.config)
        //使用“RoleMenuDbContext”连接字符串。默认情况下，此连接字符串针对您的 LocalDb 实例上的
        //“_123TribeFrameworker.Models.RoleMenuDbContext”数据库。
        // 
        //如果您想要针对其他数据库和/或数据库提供程序，请在应用程序配置文件中修改“RoleMenuDbContext”
        //连接字符串。
        public RoleMenuDbContext()
            : base("practice")
        {
        }

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public DbSet<RoleMenu> roleMenus { get; set; }
        static RoleMenuDbContext()
        {
            Database.SetInitializer<RoleMenuDbContext>(new RoleMenuDbInit());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RoleMenu>().ToTable("RoleMenu");
        }
    }
    public class RoleMenuDbInit : NullDatabaseInitializer<RoleMenuDbContext>
    {

    }
}