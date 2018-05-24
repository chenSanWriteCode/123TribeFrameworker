using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _123TribeFrameworker.Models.DirModels
{
    public class ThirdLevelDirModel
    {
        public ThirdLevelDirModel()
        { }
        public ThirdLevelDirModel(ThirdLevelDirModel_update model_upd)
        {
            if (model_upd.id_upd.HasValue)
            {
                this.id = model_upd.id_upd.Value;
            }
            this.orderId = model_upd.orderId_upd.Value;
            this.title = model_upd.content_upd;
            this.secondLevelID = model_upd.secondLevelID_upd;
            this.url = model_upd.url_upd;
        }
        public int? id { get; set; }
        public int? orderId { get; set; }
        public string title { get; set; } 
        public int? secondLevelID { get; set; }
        public string  secondLevelName { get; set; }
        public string url { get; set; }
        public DateTime? createdDate { get; set; }
        public string createdBy { get; set; }
        public DateTime? lastUpdatedDate { get; set; }
        public string lastUpdatedBy { get; set; }
    }
}