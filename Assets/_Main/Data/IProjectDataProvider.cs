using R3;
using Settings;

public interface IProjectDataProvider
{
    public Proxy.ProjectDataProxy ProjectData { get; }

    public Observable<Proxy.ProjectDataProxy> LoadProjectData( ISettingsProvider settingsProvider );
    public Observable<bool> SaveProjectData();
    public Observable<bool> ResetProjectData();
}