using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.DirModels;
using _123TribeFrameworker.Models.QueryModel;

namespace _123TribeFrameworker.Services
{
    public interface ISecondLevelDirService
    {
        Task<Result<Pager<List<SecondLevel>>>> getSecondLevelDir(Pager<List<SecondLevel>> pager,SecondMenuQuery condition);
        Task<Result<SecondLevel>> getSingleSecondDir(int id);
        Task<Result<SecondLevel>> addSecondLevelDir(SecondLevelDirModel model,string userName);
        Task<Result<SecondLevel>> updateSecondLevelDir(SecondLevelDirModel model, string userName);
        Task<Result<int>> deleteSecondLevelDir(int id);
    }
}