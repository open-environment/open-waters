using System.ServiceProcess;

namespace OpenWatersSvc
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new OpenWatersService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
