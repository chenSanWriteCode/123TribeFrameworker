using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.DirModels;

namespace _123TribeFrameworker.Services
{
    public interface IDirLayerService
    {
        List<SecondDirDisplayModel> searchSecondDir(string roleId, int ID);
        string searchMainDir(string roleId);
    }
}
