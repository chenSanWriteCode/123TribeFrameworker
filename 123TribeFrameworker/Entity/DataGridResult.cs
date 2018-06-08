using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _123TribeFrameworker.Entity
{
    public class DataGridResult<T>
    {
        public string result { get; set; } = "success";
        public string message { get; set; } = "";
        public DataGridPager pager { get; set; }
        public T data { get; set; }

    }
    public class DataGridPager
    {
        public int page { get; set; }
        public int recTotal { get; set; }
        public int recPerPage { get; set; }
    }
}