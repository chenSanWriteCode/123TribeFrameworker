﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace _123TribeFrameworker.Entity
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class practiceEntities : DbContext
    {
        public practiceEntities()
            : base("name=practiceEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<SecondLevel> SecondLevel { get; set; }
        public virtual DbSet<ThirdLevel> ThirdLevel { get; set; }
        public virtual DbSet<FirstLevel> FirstLevel { get; set; }
    
        public virtual ObjectResult<string> searchMainDir()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("searchMainDir");
        }
    
        public virtual ObjectResult<searchSecondDir_Result> searchSecondDir(Nullable<int> firstLevelID)
        {
            var firstLevelIDParameter = firstLevelID.HasValue ?
                new ObjectParameter("firstLevelID", firstLevelID) :
                new ObjectParameter("firstLevelID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<searchSecondDir_Result>("searchSecondDir", firstLevelIDParameter);
        }
    
        public virtual ObjectResult<searchThirdDir_Result> searchThirdDir(Nullable<int> secondLevelID)
        {
            var secondLevelIDParameter = secondLevelID.HasValue ?
                new ObjectParameter("secondLevelID", secondLevelID) :
                new ObjectParameter("secondLevelID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<searchThirdDir_Result>("searchThirdDir", secondLevelIDParameter);
        }
    }
}
