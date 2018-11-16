using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _123TribeFrameworker.Models.QueryModel
{
    public class SecondMenuQuery:Query
    {
        public string title { get; set; }

        public int firstLevelID { get; set; }
    }
}