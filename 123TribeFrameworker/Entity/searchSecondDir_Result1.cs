using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _123TribeFrameworker.Entity
{
    public partial class searchSecondDir_Result
    {
        public virtual List<searchThirdDir_Result> children { get; set; }
    }
}