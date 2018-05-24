using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _123TribeFrameworker.Entity;

namespace _123TribeFrameworker.Services
{
    public interface IDirLayerService
    {
        List<searchSecondDir_Result> searchSecondDir(string roleId, int ID);
        string searchMainDir(string roleId);
    }
}
