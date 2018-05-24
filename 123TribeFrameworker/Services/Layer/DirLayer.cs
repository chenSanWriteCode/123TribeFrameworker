using System;
using System.Collections.Generic;
using System.Linq;
using _123TribeFrameworker.Entity;
using Newtonsoft.Json;

namespace _123TribeFrameworker.Services.Layer
{
    public class DirLayer: IDirLayerService
    {
        private practiceEntities entities = new practiceEntities();
        public List<searchSecondDir_Result> searchSecondDir(string roleId ,int ID)
        {
            List<searchSecondDir_Result> secondDirs = new List<searchSecondDir_Result>();
            secondDirs = entities.searchSecondDir(roleId,ID).ToList();
            for (int i = 0; i < secondDirs.Count; i++)
            {
                string title = secondDirs[i].title;
                int secondID = entities.SecondLevel.First(a => a.title == title).id;
                var thirdDirs = entities.searchThirdDir(roleId,secondID ).ToList(); 
                if (thirdDirs != null)
                {
                    secondDirs[i].children = thirdDirs;
                }
            }
            return secondDirs;
        }

        public string searchMainDir(string roleId)
        {
            string mainDir= entities.searchMainDir(roleId).ToList()[0];
            return mainDir;
        }
    }
}