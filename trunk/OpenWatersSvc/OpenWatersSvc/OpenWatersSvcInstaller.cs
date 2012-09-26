using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;


namespace OpenWatersSvc
{
    [RunInstaller(true)]
    public class OpenWatersSvcInstaller : Installer
    {
         /// <summary>
        /// Public Constructor for Windows Service Installer.
        /// </summary>
        public OpenWatersSvcInstaller()
        {
            ServiceProcessInstaller serviceProcessInstaller = new ServiceProcessInstaller();
            ServiceInstaller serviceInstaller = new ServiceInstaller();

            //# Service Account Information
            serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
            serviceProcessInstaller.Username = null;
            serviceProcessInstaller.Password = null;

            //# Service Information
            serviceInstaller.ServiceName = "Open Waters Service"; //must be identical to OpenWaterSvc's ServiceName
            serviceInstaller.DisplayName = "Open Waters Service";
            serviceInstaller.Description = "Handles background WQX submission of data to EPA";
            serviceInstaller.StartType = ServiceStartMode.Automatic;

            this.Installers.Add(serviceProcessInstaller);
            this.Installers.Add(serviceInstaller);
        }

    }
}
