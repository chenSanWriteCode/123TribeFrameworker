using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _123TribeFrameworker.Models
{
    public class FirstLevelDirModel
    {
        public int id { get; set; }
        public string content { get; set; }
        public DateTime? createdDate { get; set; }
        public string createdBy { get; set; }
        public DateTime? lastUpdatedDate { get; set; }
        public string lastUpdatedBy { get; set; }
    }
}