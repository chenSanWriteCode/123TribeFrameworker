//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class ThirdLevel
    {
        public int id { get; set; }
        public Nullable<int> orderId { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public Nullable<int> secondLevelId { get; set; }
        public string open { get; set; }
        public Nullable<int> activityFlag { get; set; }
        public string createdBy { get; set; }
        public Nullable<System.DateTime> createdDate { get; set; }
        public string lastUpdatedBy { get; set; }
        public Nullable<System.DateTime> lastUpdatedDate { get; set; }
    
        public virtual SecondLevel SecondLevel { get; set; }
    }
}
