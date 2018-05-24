using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _123TribeFrameworker.Entity
{
    public class Result<T>
    {
        public bool  result { get; set; }= true;
        public T data { get; set; }
        public string message { get; set; } = "";

        public  void addError(string message)
        {
            this.result = false;
            this.message = message;
        }
    }
}