using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _123TribeFrameworker.Entity
{
    public class Result<T>
    {
        public bool  success { get; set; }= true;
        public T data { get; set; }
        public string message { get; set; } = "";

        public  void addError(string message)
        {
            this.success = false;
            this.message = message+";";
        }
    }
}