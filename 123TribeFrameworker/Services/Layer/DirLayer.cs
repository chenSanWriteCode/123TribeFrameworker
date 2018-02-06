using System;
using System.Collections.Generic;
using System.Linq;
using _123TribeFrameworker.Entity;
using Newtonsoft.Json;

namespace _123TribeFrameworker.Services.Layer
{
    public class DirLayer
    {
        private practiceEntities entities = new practiceEntities();
        public List<searchSecondDir_Result> searchSecondDir(int ID)
        {
            List<searchSecondDir_Result> secondDirs = new List<searchSecondDir_Result>();
            secondDirs = entities.searchSecondDir(ID).ToList();
            for (int i = 0; i < secondDirs.Count; i++)
            {
                string title = secondDirs[i].title;
                int secondID = entities.SecondLevel.First(a => a.title == title).id;
                var thirdDirs = entities.searchThirdDir(secondID).ToList(); ;
                if (thirdDirs != null)
                {
                    secondDirs[i].children = thirdDirs;
                }
            }
            return secondDirs;
        }

        public string searchMainDir()
        {
            string mainDir= entities.searchMainDir().ToList()[0];
            return mainDir;
        }
    }
}