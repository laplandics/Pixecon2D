using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "MenuSettings", menuName = "Settings/Menu")]
    public class MenuSettings : ScriptableObject
    {
        public VocabulariesSettings vocabulariesSettings;
        public InitialVocabulariesDataSettings initialVocabulariesDataSettings;
    }
}