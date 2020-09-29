using log4net;
using Ninject;
using Ninject.Modules;
using WindowsGitService.CustomConfig;
using WindowsGitService.DAL;
using WindowsGitService.DAL.FileManagment;
using WindowsGitService.DAL.Interfaces;
using WindowsGitService.Logging;

namespace WinService.DI
{
    public class DependensyInjection : NinjectModule
    {
        public override void Load()
        {
            Bind<FileTrackerTimer>().ToSelf();

            // facade
            Bind<IFileChangesFacade>().To<FileChangesFacade>();

            // file access
            Bind<IFileValidator>().To<FileValidator>();
            Bind<IFileAccessor>().To<FileReader>().InSingletonScope();
            Bind<IChangedFileSaver>().To<FileWriter>().InSingletonScope();

            // fileInfo converte
            Bind<IFileViewConverter>().To<FileViewModelBuilder>();

            // compare changes in files
            Bind<IFileChangesTracker>().To<FileChangesTracker>();

            // config access
            Bind<IMonitoringFolders>().To<FolderElementsAccessor>();

            // logging
            Bind<ILog>().ToMethod(context => CustomLogger.GetLogger());
        }

        public IKernel GetKernel()
        {
            return new StandardKernel(this);
        }
    }
}
