using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _123TribeFrameworker.Entity
{
    public class Result<T>
    {
        public string  result { get; set; }= "success";
        public T data { get; set; }
        public string message { get; set; } = "";

        public Pager pager { get; set; }
    }
}