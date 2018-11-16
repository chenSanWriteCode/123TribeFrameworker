using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.QueryModel;

namespace _123TribeFrameworker.DAO
{
    public interface ISecondLevelDirDAO
    {
        Result<List<SecondLevel>> getSecondLevelDir(Pager<List<SecondLevel>> pager, SecondMenuQuery condition);
        int getSecondLevelDirCount(SecondMenuQuery condition);
        Result<SecondLevel> getSingleSecondDir(int id);

        Task<Result<SecondLevel>> updateSecondLevelDir(SecondLevel model);
        Task<Result<SecondLevel>> addSecondLevelDir(SecondLevel model);
        Result<int> deleteSecondlevelDir(int id);
    }
}
