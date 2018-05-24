using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.DirModels;

namespace _123TribeFrameworker.Services
{
    public interface IFirstLevelDirService
    {
        Pager<List<FirstLevelDirModel>> getFirstLevelDir(Pager<FirstLevelDirModel> pager);
        Dictionary<int, string> getFirstLevelDirDict();
        Result<FirstLevelDirModel> addFirstLevelDir(FirstLevelDirModel model);
        Result<FirstLevelDirModel> updateFirstLevelDir(FirstLevelDirModel model);
        Result<FirstLevelDirModel> deleteFirstLevelDir(int id);
    }
}