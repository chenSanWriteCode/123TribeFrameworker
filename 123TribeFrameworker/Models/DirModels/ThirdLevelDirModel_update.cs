﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _123TribeFrameworker.Models.DirModels
{
    public class ThirdLevelDirModel_update
    {
        public int? id_upd { get; set; }
        public int? orderId_upd { get; set; }
        public string content_upd { get; set; }
        public int secondLevelID { get; set; }
        public string url { get; set; }
    }
}