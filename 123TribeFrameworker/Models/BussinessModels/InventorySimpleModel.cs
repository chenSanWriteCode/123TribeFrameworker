using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _123TribeFrameworker.Models.BussinessModels
{
    public class InventorySimpleModel
    {
        public int materialId { get; set; }

        public string materialName { get; set; }
        public string mat_size { get; set; }
        public float count { get; set; }
    }
}