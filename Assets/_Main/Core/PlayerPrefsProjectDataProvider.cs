using System.Collections.Generic;
using R3;
using UnityEngine;

namespace Core
{
    public class PlayerPrefsProjectDataProvider : IProjectDataProvider
    {
        public Proxy.ProjectDataProxy ProjectData { get; private set; }

        private Data.ProjectData _initialProjectData;
        
        public Observable<Proxy.ProjectDataProxy> LoadProjectData()
        {
            Debug.LogWarning("Remove temporal editor code (PlayerPrefs load)");
//#if UNITY_EDITOR
            //PlayerPrefs.DeleteKey(Constant.Names.PlayerPrefs.PROJECT_DATA_KEY);
//#endif
            
            if (!PlayerPrefs.HasKey(Constant.Names.PlayerPrefs.PROJECT_DATA_KEY))
            {
                ProjectData = CreateNewProjectData();
                SaveProjectData();
            }
            else
            {
                var json = PlayerPrefs.GetString(Constant.Names.PlayerPrefs.PROJECT_DATA_KEY);
                _initialProjectData = JsonUtility.FromJson<Data.ProjectData>(json);
                ProjectData = new Proxy.ProjectDataProxy(_initialProjectData);
            }
            
            return Observable.Return(ProjectData);
        }

        public Observable<bool> SaveProjectData()
        {
            var json = JsonUtility.ToJson(_initialProjectData);
            PlayerPrefs.SetString(Constant.Names.PlayerPrefs.PROJECT_DATA_KEY, json);
            return Observable.Return(true);
        }

        public Observable<bool> ResetProjectData()
        {
            ProjectData = CreateNewProjectData();
            SaveProjectData();
            
            return Observable.Return(true);
        }

        private Proxy.ProjectDataProxy CreateNewProjectData()
        {
            Debug.LogWarning("Create initial projectData settings. " +
                             $"Remove temporal data creation from {GetType().Name}");
            
            _initialProjectData = new Data.ProjectData
            {
                vocabularies = new List<Data.VocabularyData>
                {
                    new()
                    {
                        entityID = 0,
                        key = "Vocabulary0",
                        title = "Новый словарь 1",
                        isDone = false,
                        isIncluded = true,
                        vocabularyEntries = new List<Data.VocabularyEntryData>
                        {
                            new()
                            {
                                key = "Entry0",
                                isDone = false,
                                word = "One",
                                translation = "Один"
                            },
                            new()
                            {
                                key = "Entry1",
                                isDone = false,
                                word = "Two",
                                translation = "Два"
                            }
                        }
                    }
                },
            };
            var proxy = new Proxy.ProjectDataProxy(_initialProjectData);
            _ = proxy.GetGlobalEntityId;
            return proxy;
        }
    }
}