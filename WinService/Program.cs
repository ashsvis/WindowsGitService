using log4net;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using WindowsGitService.DAL;
using WinService.DI;

namespace WinService
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        static void Main()
        {
         //   IKernel kernel = new DependensyInjection().GetKernel();
         //
         //   var timeManager = kernel.Get<FileTrackerTimer>();

         //   if (!Environment.UserInteractive)
         //   {
                // running as service
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new GitService()
                };

                ServiceBase.Run(ServicesToRun);
          //  }
          //  else
          //  {
          //      // running as console app
          //      kernel.Get<ILog>().Warn("Старт работы консольной версии сервиса");
          //      timeManager.InitilizeTimer();
          //      Console.ReadKey(true);
          //  }
        }
    }
}
