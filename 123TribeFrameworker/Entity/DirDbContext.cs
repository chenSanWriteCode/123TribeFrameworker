namespace _123TribeFrameworker.Entity
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;

    public class DirDbContext : DbContext
    {
        //您的上下文已配置为从您的应用程序的配置文件(App.config 或 Web.config)
        //使用“DirDbContext”连接字符串。默认情况下，此连接字符串针对您的 LocalDb 实例上的
        //“_123TribeFrameworker.Entity.DirDbContext”数据库。
        // 
        //如果您想要针对其他数据库和/或数据库提供程序，请在应用程序配置文件中修改“DirDbContext”
        //连接字符串。
        public DirDbContext()
            : base("name=practice")
        {
        }
        public virtual DbSet<FirstLevel> firstLevels { get; set; }
        public virtual DbSet<SecondLevel> secondLevels { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<FirstLevel>().Property(p => p.RowVersion).IsRowVersion();
            modelBuilder.Entity<SecondLevel>().Property(p => p.RowVersion).IsRowVersion();

        }
    }

    
}