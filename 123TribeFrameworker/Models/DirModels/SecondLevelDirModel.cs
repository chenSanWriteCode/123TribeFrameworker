using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _123TribeFrameworker.Models.DirModels
{
    public class SecondLevelDirModel
    {
        public int id { get; set; }
        public int orderId { get; set; } = 1;
        [Required]
        public string title { get; set; }
        [Required]
        public int firstLevelID { get; set; }
        [Required]
        public string url { get; set; }
        public string open { get; set; } = "false";
    }
}