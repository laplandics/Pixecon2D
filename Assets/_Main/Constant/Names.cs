namespace Constant
{
    public static class Names
    {
        public static class Scenes
        {
            public const string BOOT = "Boot";
            public const string MENU = "Menu";
            public const string GAME = "Game";
        }

        public static class UI
        {
            public const string UI_ROOT = "UI/[UI]";
            public const string LOADING_SCREEN = "UI/LoadingScreen";
            public const string MENU_UI = "UI/MenuUI";
            public const string GAME_UI = "UI/GameUI";

            public const string VOCABULARY_INFO_PANEL = "UI/Vocabulary/VocabularyInfoPanel";
            public const string PREFERENCES_INFO_PANEL = "UI/Preferences/PreferencesInfoPanel";
            public const string PLAY_INFO_PANEL = "UI/Play/PlayInfoPanel";
            public const string ABOUT_INFO_PANEL = "UI/About/AboutInfoPanel";
        }

        public static class PlayerPrefs
        {
            public const string PROJECT_DATA_KEY = nameof(PROJECT_DATA_KEY);
        }

        public static class EntityData
        {
            public const string VOCABULARY_BASE_KEY = "Vocab";
            public const string VOCABULARY_ENTRY_BASE_KEY = "Entry";
        }
    }
}