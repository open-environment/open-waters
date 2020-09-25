using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;
using OpenEnvironment.App_Logic.BusinessLogicLayer;
using OpenEnvironment.App_Logic.DataAccessLayer;

namespace OpenWatersSvc
{
    class OpenWatersSvc : ServiceBase
    {
        private Timer timer = new Timer();

        /// <summary>
        /// Public Constructor for WindowsService.
        /// - Put all Initialization code here.
        /// </summary>
        public OpenWatersSvc()
        {
            this.ServiceName = "Open Waters Service";
            this.EventLog.Log = "Application";

            //conduct database connectivity and assembly reference test
            //string _dbtest = db_Ref.GetT_OE_APP_SETTING_test("Log Level");
            //EventLog.WriteEntry(_dbtest);
            //WQXSubmit.WQX_MasterAllOrgs();


            // These Flags set whether or not to handle that specific
            //  type of event. Set to true if you need it, false otherwise.
            this.CanHandlePowerEvent = true;
            this.CanHandleSessionChangeEvent = true;
            this.CanPauseAndContinue = true;
            this.CanShutdown = true;
            this.CanStop = true;
        }


        /// <summary>
        /// The Main Thread where service is run.
        /// </summary>
        static void Main()
        {
            ServiceBase.Run(new OpenWatersSvc());
        }


        /// <summary>
        /// Dispose of objects that need it.
        /// </summary>
        /// <param name="disposing">Whether or not disposing is going on.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        /// <summary>
        /// Startup code - this method runs when the service starts up for the first time.
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            EventLog.WriteEntry("Open Waters Service starting.");

            try
            {
                //for db connectivity testing only
                //string _dbtest = db_Ref.GetT_OE_APP_SETTING_test("Log Level");
                //EventLog.WriteEntry(_dbtest);


                //Reset the Open Waters service status so it will run again (in case it failed previously)
                int SuccID = db_Ref.UpdateT_OE_APP_TASKS("WQXSubmit", "STOPPED", null, "TASK");
                if (SuccID > 0)
                {
                    //Start the service timer
                    timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
                    timer.Interval = 10000;  //providing the time in miliseconds 
                    timer.AutoReset = true;
                    timer.Enabled = true;
                    timer.Start();
                }
                else
                {
                    EventLog.WriteEntry("Unable to reset service");
                }
            }
            catch 
            {
                EventLog.WriteEntry("Failed - Unspecified error.");
            }
        }

        void timer_Elapsed(object sender, EventArgs e)
        {
            try
            {

                //submitting any pending data to EPA
                WQXSubmit.WQX_MasterAllOrgs();

                EventLog.WriteEntry("Submission attempt completed");

                //importing activity data from EPA
                WQXSubmit.ImportActivityMaster();
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("Open Waters Task has failed." + ex.Message);
            }
        }


        /// <summary>
        /// OnStop(): Stop code 
        /// - Stop threads, set final data, etc.
        /// </summary>
        protected override void OnStop()
        {
            timer.Stop();
        }

        /// <summary>
        /// OnPause: Pause code 
        /// - Pause working threads, etc.
        /// </summary>
        protected override void OnPause()
        {
            base.OnPause();
            timer.Stop();
        }

        /// <summary>
        /// OnContinue(): Continue code 
        /// - Un-pause working threads, etc.
        /// </summary>
        protected override void OnContinue()
        {
            base.OnContinue();
            timer.Start();
        }

        /// <summary>
        /// OnShutdown(): Called when the System is shutting down
        /// - Put code here when you need special handling
        ///   of code that deals with a system shutdown, such
        ///   as saving special data before shutdown.
        /// </summary>
        protected override void OnShutdown()
        {
            base.OnShutdown();
            timer.Stop();
        }

        /// <summary>
        /// OnCustomCommand(): If you need to send a command to your
        ///   service without the need for Remoting or Sockets, use
        ///   this method to do custom methods.
        /// </summary>
        /// <param name="command">Arbitrary Integer between 128 & 256</param>
        protected override void OnCustomCommand(int command)
        {
            base.OnCustomCommand(command);
        }

        /// <summary>
        /// OnPowerEvent(): Useful for detecting power status changes,
        ///   such as going into Suspend mode or Low Battery for laptops.
        /// </summary>
        /// <param name="powerStatus">The Power Broadcast Status
        /// (BatteryLow, Suspend, etc.)</param>
        protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus)
        {
            return base.OnPowerEvent(powerStatus);
        }

        /// <summary>
        /// OnSessionChange(): To handle a change event
        ///   from a Terminal Server session.
        ///   Useful if you need to determine
        ///   when a user logs in remotely or logs off,
        ///   or when someone logs into the console.
        /// </summary>
        /// <param name="changeDescription">The Session Change
        /// Event that occured.</param>
        protected override void OnSessionChange(
                  SessionChangeDescription changeDescription)
        {
            base.OnSessionChange(changeDescription);
        }


    }
}
