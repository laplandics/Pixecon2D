using R3;

public interface IProjectDataProvider
{
    public Proxy.ProjectDataProxy ProjectData { get; }

    public Observable<Proxy.ProjectDataProxy> LoadProjectData();
    public Observable<bool> SaveProjectData();
    public Observable<bool> ResetProjectData();
}