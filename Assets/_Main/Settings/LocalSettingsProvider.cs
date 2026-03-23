using System.Threading.Tasks;
using UnityEngine;

namespace Settings
{
    public class LocalSettingsProvider : ISettingsProvider
    {
        public ApplicationSettings ApplicationSettings =>
            Resources.Load<ApplicationSettings>(Constant.Names.Configs.APPLICATION_SETTINGS);

        public MenuSettings MenuSettings { get; private set; }
        public GameSettings GameSettings { get; private set; }


        public Task<bool> LoadSettingsAsync()
        {
            MenuSettings = Resources.Load<MenuSettings>(Constant.Names.Configs.MENU_SETTINGS);
            GameSettings = Resources.Load<GameSettings>(Constant.Names.Configs.GAME_SETTINGS);
            
            return Task.FromResult(MenuSettings != null && GameSettings != null);
        }
    }
}