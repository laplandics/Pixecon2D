using System.Threading.Tasks;

namespace Settings
{
    public interface ISettingsProvider
    {
        public ApplicationSettings ApplicationSettings { get; }
        public MenuSettings MenuSettings { get; }
        public GameSettings GameSettings { get; }

        public Task<bool> LoadSettingsAsync();
    }
}