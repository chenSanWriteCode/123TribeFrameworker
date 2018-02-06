using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models;

namespace _123TribeFrameworker.Services
{
    public interface IFirstLevelDir
    {
        List<FirstLevelDirModel> getFirstLevelDir(FirstLevelDirModel model);



    }
}
