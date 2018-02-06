using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models;

namespace _123TribeFrameworker.Services.Layer
{
    public class FirstLevelDir : IFirstLevelDir
    {
        public List<FirstLevelDirModel> getFirstLevelDir(FirstLevelDirModel model)
        {
            List<FirstLevelDirModel> returnList = new List<FirstLevelDirModel>();
            List<firstLevel> firstLevelList = new List<firstLevel>();
            practiceEntities entities = new practiceEntities();
            firstLevelList = entities.firstLevel.Where(x => x.id > 0).ToList();
            foreach (var item in firstLevelList)
            {
                FirstLevelDirModel demo = new FirstLevelDirModel();
                demo.content = item.midContent;
                demo.createdBy = item.createdBy;
                demo.createdDate = item.createdDate;
                demo.lastUpdatedBy = item.lastUpdatedBy;
                demo.lastUpdatedDate = item.lastUpdatedDate;
                returnList.Add(demo);
            }
            return returnList;
        }
    }
}