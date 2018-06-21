using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _123TribeFrameworker.CommonTools
{
    public class DirTools
    {
        public string afterContent { get;  }  = "</h3></a></li>";

        public string beforContent { get; } = "<li><a href=\"#\"><h3>";
        
    }
    /// <summary>
    /// 目录级别
    /// </summary>
    public enum DirLevel
    {
        FirstLevel=1,
        SecondLevel,
        ThirdLLevel
    }
}