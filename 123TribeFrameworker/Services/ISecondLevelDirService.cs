using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.DirModels;

namespace _123TribeFrameworker.Services
{
    public interface ISecondLevelDirService
    {
        Pager<List<SecondLevelDirModel>> getSecondLevelDir(Pager<SecondLevelDirModel> pager);
        Dictionary<int, string> getSecondLevelDirDict();
        SecondLevelDirModel getSingleSecondDir(int id);
        Result<SecondLevelDirModel> addSecondLevelDir(SecondLevelDirModel model);
        Result<SecondLevelDirModel> updateSecondLevelDir(SecondLevelDirModel model);
        Result<SecondLevelDirModel> deleteSecondLevelDir(int id);
    }
}