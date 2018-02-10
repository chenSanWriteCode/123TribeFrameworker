using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _123TribeFrameworker.Entity
{
    public class Message
    {
        public Message(string msg)
        {
            if (msg.Contains("成功")|| msg.Contains("success"))
            {
                this.success = "success";
            }
            else
            {
                this.success = "failure";
            }
            this.msg = msg;
        }
        public string msg { get; set; }
        public string  success { get; set; }
    }
}