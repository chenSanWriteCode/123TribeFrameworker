namespace _123TribeFrameworker.Migrations.LayerMigrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Entity;

    internal sealed class Configuration : DbMigrationsConfiguration<_123TribeFrameworker.Entity.LayerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"Migrations\LayerMigrations";
        }

        protected override void Seed(LayerDbContext context)
        {
            if (context.orderStatus.Where(x => x.statusKey == "generated").Count()==0)
            {
                OrderStatus model = new OrderStatus() { statusKey="generated",status = "���·�" };
                context.orderStatus.Add(model);
            }
            if (context.orderStatus.Where(x => x.statusKey == "completed").Count() == 0)
            {
                OrderStatus model = new OrderStatus() { status = "���",statusKey="completed" };
                context.orderStatus.Add(model);
            }
            if (context.orderStatus.Where(x => x.statusKey == "excepted").Count() == 0)
            {
                OrderStatus model = new OrderStatus() { status = "�ջ��쳣",statusKey= "excepted" };
                context.orderStatus.Add(model);
            }
            context.SaveChanges();
        }
    }
}
