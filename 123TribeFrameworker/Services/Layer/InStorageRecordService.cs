using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using _123TribeFrameworker.DAO;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Services;
using Unity.Attributes;

namespace _123TribeFrameworker.Services.Layer
{
    public class InStorageRecordService : IInStorageRecordService
    {
        [Dependency]
        public IInStorageRecordDAO dao { get; set; }
        
    }
}