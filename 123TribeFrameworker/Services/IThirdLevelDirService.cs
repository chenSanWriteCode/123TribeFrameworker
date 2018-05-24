using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.DirModels;

namespace _123TribeFrameworker.Services
{
    public interface IThirdLevelDirService
    {
        Pager<List<ThirdLevelDirModel>> getThirdLevelDir(Pager<ThirdLevelDirModel> pager);
        ThirdLevelDirModel getSingleThirdDir(int id);
        Result<ThirdLevelDirModel> addThirdLevelDir(ThirdLevelDirModel model);
        Result<ThirdLevelDirModel> updateThirdLevelDir(ThirdLevelDirModel model);
        Result<ThirdLevelDirModel> deleteThirdLevelDir(int id);
    }
}
