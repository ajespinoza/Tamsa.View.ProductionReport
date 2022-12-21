// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ManagerAccess.cs" company="Tenaris">
//   Tenaris.
// </copyright>
// <summary>
//   Defines the ManagerAccess.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tamsa.View.ProductionReport.Model
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Configuration;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Remoting;
    using System.Text;
    using System.Threading;
    using System.Windows;
    using System.Windows.Threading;
    using Tenaris.Library.ConnectionMonitor;
    using Tenaris.Library.Proxy;
    using Tenaris.Library.Proxy.Exceptions;
    using Tenaris.Library.System.Factory;
    using Tenaris.Library.System.MultiClient;
    using Tenaris.Library.System.Remoting;
    using Tenaris.Manager.ExitApplication.Common;
    using Tenaris.Manager.ExitApplication.Support;

    /// <summary>
    /// The exit application manager access class.
    /// </summary>
    public class ManagerAccess
    {
        #region Fields

        /// <summary>
        /// IManager object
        /// </summary>
        private static MultipleManagerClient<IExitApplicationManager> manager;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes static members of the <see cref="ManagerAccess"/> class.
        /// </summary>
        static ManagerAccess()
        {
            try
            {
                SuscribeManager();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// ShiiftChangedEvent on manager.
        /// </summary>
        public static event EventHandler<Tenaris.Manager.ExitApplication.Common.ShiftChangedEventArgs> ShiftChanged;

        /// <summary>
        /// MachineChangedEvent on manager.
        /// </summary>
        public static event EventHandler<MachineChangedEventArgs> ManagerMachineChanged;

        /// <summary>
        /// Gets the production machine manager.
        /// </summary>
        public static Dictionary<string, IMachine> ManagerProductionMachines { get; private set; }

        /// <summary>
        /// Gets the reworked machine manager.
        /// </summary>
        public static Dictionary<string, IMachine> ManagerReworkedMachines { get; private set; }

        /// <summary>
        /// Gets the manager shift.
        /// </summary>
        public static Shift ManagerShift { get; private set; }

        #endregion

        #region Public methods

        /// <summary>
        /// Suscribe machines method.
        /// </summary>
        /// <param name="areacode">
        /// The area code.
        /// </param>
        public static void SuscribeMachines(string areacode)
        {
            try
            {
                // Get area configuration.
                var area = new AreaConfiguration();
                foreach (AreaConfiguration a in ClientConfiguration.Settings.Areas)
                {
                    if (a.AreaCode == areacode)
                    {
                        area = a;
                        break;
                    }
                }

                // Get production machines.
                var machinesProductionDictionary = manager.Managers[areacode].Instance.Machines.Where(p => area.ProductionMachines.Contains(p.Code)).ToDictionary(m => m.Code, m => m);
                ManagerProductionMachines = machinesProductionDictionary;

                // Suscribe events.
                foreach (var match in ManagerProductionMachines.Values)
                {
                    match.MachineChanged += Match_MachineChanged;
                }

                // Verify is required reworked mahcines.
                if ((bool)area.IsActiveReworkedMachines)
                {
                    // Get production machines.
                    var machinesReworkedDictionary = manager.Managers[areacode].Instance.Machines.Where(p => area.ReworkedMachines.Contains(p.Code)).ToDictionary(m => m.Code, m => m);
                    ManagerReworkedMachines = machinesReworkedDictionary;

                    // Suscribe events.
                    foreach (var match in ManagerReworkedMachines.Values)
                    {
                        match.MachineChanged += Match_MachineChanged;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Suscribe shift method.
        /// </summary>
        /// <param name="areacode">
        /// The area code.
        /// </param>
        /// <returns>
        /// The shift object.
        /// </returns>
        //public static Shift SuscribeShift(string areacode)
        //{
        //    try
        //    {
        //        throw;
        //        //manager.Managers[areacode].Instance.ShiftChanged += new EventHandler<ShiftChangedEventArgs>(Instance_ShiftChanged);
        //        //return manager.Managers[areacode].Instance.CurrentShift;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        #endregion

        #region Internal methods

        /// <summary>
        /// Suscription to managers.
        /// </summary>
        internal static void SuscribeManager()
        {
            try
            {
                // Subscribe connection monitor state changed event
                //ConnectionMonitor.Instance.StateChanged += new EventHandler<StateChangeEventArgs>(Instance_StateChanged);

                //System.Runtime.Remoting.RemotingConfiguration.Configure(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile, false);
                //manager = new MultipleManagerClient<IExitApplicationManager>("Tenaris.Manager.ExitApplication.ExitApplicationManager");
                //manager.Created += new EventHandler<ConnectionChangedEventArgs<IExitApplicationManager>>(Manager_Created);
                //manager.Connected += new EventHandler<ConnectionChangedEventArgs<IExitApplicationManager>>(Manager_Connected);
                //manager.Disconnected += new EventHandler<ConnectionChangedEventArgs<IExitApplicationManager>>(Manager_Disconnected);
                //manager.CreationFailed += new EventHandler<ConnectionFailedEventArgs>(Manager_CreationFailed);
                //manager.Start();

                Application.Current.Exit += delegate { manager.Stop(); };
            }
            catch 
            {
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Connection monitor state changed
        /// </summary>
        /// <param name="sender">
        /// The sender object.
        /// </param>
        /// <param name="e">
        /// The event args object.
        /// </param>
        private static void Instance_StateChanged(object sender, StateChangeEventArgs e)
        {
            try
            {
                //if (e.IsConnected)
                //{
                //    if (e.Connection is Tenaris.Manager.ExitApplication.Common.IExitApplicationManager)
                //    {
                //        var manager = (Tenaris.Manager.ExitApplication.Common.IExitApplicationManager)e.Connection;
                //        ManagerShift = manager.CurrentShift;
                //    }
                //}
                //else
                //{
                //     ManagerShift = new Shift(1, string.Empty, string.Empty);
                //}
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Method on creation managers.
        /// </summary>
        /// <param name="sender">
        /// The sender object.
        /// </param>
        /// <param name="e">
        /// The manager create event.
        /// </param>
        private static void Manager_Created(object sender, ConnectionChangedEventArgs<IExitApplicationManager> e)
        {
            try
            {
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Method on creation failed.
        /// </summary>
        /// <param name="sender">
        /// The sender object.
        /// </param>
        /// <param name="e">
        /// The manager event.
        /// </param>
        private static void Manager_CreationFailed(object sender, ConnectionFailedEventArgs e)
        {
            Console.WriteLine("ERROR! couldn't find an ExitApplicationManager for the area [{0}]", e.AreaCode);
        }

        /// <summary>
        /// Method on connected manager.
        /// </summary>
        /// <param name="sender">
        /// The sender object.
        /// </param>
        /// <param name="e">
        /// The manager connected event.
        /// </param>
        private static void Manager_Connected(object sender, ConnectionChangedEventArgs<IExitApplicationManager> e)
        {
            if (ManagerProductionMachines != null)
            {
                // Unsuscribe events.
                foreach (var match in ManagerProductionMachines.Values)
                {
                    match.MachineChanged -= Match_MachineChanged;
                }

                SuscribeMachines(e.AreaCode);
            }

            if (ManagerReworkedMachines != null)
            {
                foreach (var match in ManagerReworkedMachines.Values)
                {
                    match.MachineChanged -= Match_MachineChanged;
                }
            }
        }

        /// <summary>
        /// Method on disconnected manager.
        /// </summary>
        /// <param name="sender">
        /// The sender object.
        /// </param>
        /// <param name="e">
        /// The disconnected event.
        /// </param>
        private static void Manager_Disconnected(object sender, ConnectionChangedEventArgs<IExitApplicationManager> e)
        {
        }
        
        /// <summary>
        /// Instance shift changed method.
        /// </summary>
        /// <param name="sender"> 
        /// The sender object.
        /// </param>
        /// <param name="e">
        /// The shift event.
        /// </param>
        private static void Instance_ShiftChanged(object sender, ShiftChangedEventArgs e)
        {
            var shift = new Tenaris.Manager.ExitApplication.Common.Shift(e.Shift.CurrentShift, e.Shift.ProductionDate, e.Shift.CurrentCrew);

            if (ShiftChanged != null)
            {
                new Thread(delegate() { ShiftChanged.Invoke(sender, new Tenaris.Manager.ExitApplication.Common.ShiftChangedEventArgs(shift)); })
                { 
                    IsBackground = true 
                } .Start();
            }
        }

        /// <summary>
        /// Instance machine changed method.
        /// </summary>
        /// <param name="sender">
        /// The sender object.
        /// </param>
        /// <param name="e">
        /// The machine event.
        /// </param>
        private static void Match_MachineChanged(object sender, MachineChangedEventArgs e)
        {
            try
            {
                if (ManagerMachineChanged != null)
                {
                    new Thread(delegate() { ManagerMachineChanged.Invoke(sender, new MachineChangedEventArgs(e.Machine)); }) 
                    { 
                        IsBackground = true 
                    } .Start();
                }
            }
            catch (Exception ex)
            {
                //Trace.Exception(ex, "Exception on machine end operation ", ex.Message);
                throw ex;
            }
        }

        #endregion
    }
}
