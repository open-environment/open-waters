using OpenEnvironment.App_Logic.BusinessLogicLayer;
using OpenEnvironment.App_Logic.DataAccessLayer;
using System;
using System.ServiceProcess;
using System.Timers;

namespace OpenWatersSvc
{
    public partial class OpenWatersService : ServiceBase
    {
        private Timer timer = new Timer();

        public OpenWatersService()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Startup code - this method runs when the service starts up for the first time.
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            Utils.WriteToFile("Open Waters Task Service started");

            try
            {
                //Reset the service status so it will run again (in case it failed previously)
                int SuccID = db_Ref.UpdateT_OE_APP_TASKS("WQXSubmit", "STOPPED", null, "TASK");
                if (SuccID > 0)
                {
                    // Set up a timer that triggers every minute.
                    timer.Interval = 10000; //providing the time in miliseconds 
                    timer.Elapsed += new ElapsedEventHandler(OnTimer);
                    timer.AutoReset = true;
                    timer.Enabled = true;
                    timer.Start();
                    Utils.WriteToFile("*************************************************");
                    Utils.WriteToFile("Open Waters Task Service timer successfully initialized");
                    Utils.WriteToFile("*************************************************");
                    Utils.WriteToFile("Open Waters Task Service timer set to run every " + timer.Interval + " ms");
                }
                else
                {
                    Utils.WriteToFile("Unable to reset service");
                }

            }
            catch (Exception ex)
            {
                Utils.WriteToFile("Failed to start Open Waters Service - Unspecified error. " + ex.Message);
            }
        }

        protected override void OnStop()
        {
            Utils.WriteToFile("Open Waters Task has stopped");
        }


        public void OnTimer(object sender, ElapsedEventArgs args)
        {
            try
            {
                //Utils.WriteToFile("Open Waters Submission Task Started");
                //submitting any pending data to EPA
                WQXSubmit.WQX_MasterAllOrgs();


                //importing activity data from EPA
                WQXSubmit.ImportActivityMaster();
                //Utils.WriteToFile("Open Waters Submission Task Ended");
            }
            catch
            {
                Utils.WriteToFile("ERROR getting execution time information from database.");
            }
        }
    }
}
