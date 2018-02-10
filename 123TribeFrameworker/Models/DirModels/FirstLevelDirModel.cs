using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _123TribeFrameworker.Models.DirModels
{
    public class FirstLevelDirModel
    {
        public FirstLevelDirModel()
        { }
        public FirstLevelDirModel(FirstLevelDirModel_update model_upd)
        {
            if (model_upd.id_upd.HasValue)
            {
                this.id = model_upd.id_upd.Value;
            }
            this.orderId = model_upd.orderId_upd.Value;
            this.content = model_upd.content_upd?.ToString();
        }
        public int? id { get; set; }
        public int? orderId { get; set; }
        public string content { get; set; }
        public DateTime? createdDate { get; set; }
        public string createdBy { get; set; }
        public DateTime? lastUpdatedDate { get; set; }
        public string lastUpdatedBy { get; set; }
    }
}