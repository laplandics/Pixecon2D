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
            public const string UI_ROOT = "Prefabs/UI/[UI]";
            public const string LOADING_SCREEN = "Prefabs/UI/LoadingScreen";
            public const string MENU_UI = "Prefabs/UI/Menu/MenuUI";
            public const string GAME_UI = "Prefabs/UI/Game/GameUI";

            public const string VOCABULARY_INFO_PANEL = "Prefabs/UI/Menu/Vocabulary/VocabularyInfoPanel";
            public const string PREFERENCES_INFO_PANEL = "Prefabs/UI/Menu/Preferences/PreferencesInfoPanel";
            public const string PLAY_INFO_PANEL = "Prefabs/UI/Menu/Play/PlayInfoPanel";
            public const string ABOUT_INFO_PANEL = "Prefabs/UI/Menu/About/AboutInfoPanel";

            public const string GAME_NO_VOCABULARIES_POPUP = "Prefabs/UI/Game/Popups/NoVocabularies";
        }

        public static class World
        {
            public const string CAM = "Prefabs/World/Cam";
            public const string GAME_WORLD = "Prefabs/World/GameWorld";
            
            public const string CELL_PREFAB = "Prefabs/World/Cell";
            public const string LETTERS_ATLAS = "Textures/LettersAtlas";
        }
        
        public static class PlayerPrefs
        {
            public const string PROJECT_DATA_KEY = nameof(PROJECT_DATA_KEY);
        }

        public static class EntityData
        {
            public const string VOCABULARY_BASE_KEY = "Vocab";

            public const string CELL_EMPTY_KEY = "Cell";
        }

        public static class Configs
        {
            public const string APPLICATION_SETTINGS = "Configs/ProjectSettings/ApplicationSettings";
            public const string MENU_SETTINGS = "Configs/ProjectSettings/MenuSettings";
            public const string GAME_SETTINGS = "Configs/ProjectSettings/GameSettings";
        }
    }
}