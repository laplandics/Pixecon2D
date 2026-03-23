using System.Collections.Generic;
using R3;
using Settings;
using UnityEngine;

namespace Data
{
    public class PlayerPrefsProjectDataProvider : IProjectDataProvider
    {
        public Proxy.ProjectDataProxy ProjectData { get; private set; }
        private ProjectData _initialProjectData;
        private ISettingsProvider _settingsProvider;
        
        public Observable<Proxy.ProjectDataProxy> LoadProjectData(ISettingsProvider settingsProvider)
        {
            _settingsProvider = settingsProvider;
            
#if UNITY_EDITOR
            Debug.LogWarning("Remove temporal editor code (PlayerPrefs load)");
            PlayerPrefs.DeleteKey(Constant.Names.PlayerPrefs.PROJECT_DATA_KEY);
#endif
            
            if (!PlayerPrefs.HasKey(Constant.Names.PlayerPrefs.PROJECT_DATA_KEY))
            {
                ProjectData = CreateNewProjectData();
                SaveProjectData();
            }
            else
            {
                var json = PlayerPrefs.GetString(Constant.Names.PlayerPrefs.PROJECT_DATA_KEY);
                _initialProjectData = JsonUtility.FromJson<ProjectData>(json);
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
            _initialProjectData = new ProjectData();
            var projectDataProxy = new Proxy.ProjectDataProxy(_initialProjectData);

            foreach (var initData in _settingsProvider.MenuSettings.initialVocabulariesDataSettings.initialVocabulariesData)
            {
                var initialVocabularyEntries = new List<VocabularyEntryData>();
                foreach (var initEntry in initData.entries)
                {
                    var initialVocabularyEntry = new VocabularyEntryData
                    {
                        entityID = projectDataProxy.GetGlobalEntityId,
                        isDone = false,
                        word = initEntry.word,
                        translation = initEntry.translation,
                        isCurrent = initEntry.isInitial
                    };
                    initialVocabularyEntries.Add(initialVocabularyEntry);
                }
                
                var initialVocabularyData = new VocabularyData
                {
                    entityID = projectDataProxy.GetGlobalEntityId,
                    key = initData.key,
                    title = initData.title,
                    isCompleted = false,
                    vocabularyEntries = initialVocabularyEntries
                };
                
                projectDataProxy.Vocabularies.Add(new Proxy.VocabularyDataProxy(initialVocabularyData));
            }
            
            return projectDataProxy;
        }
    }
}