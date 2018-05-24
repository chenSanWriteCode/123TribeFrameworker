using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _123TribeFrameworker.Entity;

namespace _123TribeFrameworker.Models.DirModels
{
    public class RoleMenuEdit
    {
        public AplicationRole role { get; set; }
        public List<RoleProp> firstDirs { get; set; }
        public List<RoleProp> secondDirs { get; set; }
        public List<RoleProp> thirdDirs { get; set; }
    }
    public class RoleMenuModified
    {
        public string roleId { get; set; }
        public int[] firstDirIds { get; set; }
        public int[] secondDirIds { get; set; }
        public int[] thirdDirIds { get; set; }
    }
}




