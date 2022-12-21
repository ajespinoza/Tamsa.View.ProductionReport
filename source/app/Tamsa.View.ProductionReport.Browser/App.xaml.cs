// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="Tenaris">
//   Tenaris.
// </copyright>
// <summary>
//   Defines the App.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tamsa.View.ProductionReport.Browser
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Globalization;
    using System.Linq;
    using System.Resources;
    using System.Threading;
    using System.Windows;
    using System.Windows.Markup;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Application start up
        /// </summary>
        /// <param name="sender">
        /// The sender object.
        /// </param>
        /// <param name="e">
        /// The start up event.
        /// </param>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //var splashScreen = new SplashScreen("Resources/TenarisLoad.gif");

             //Run screen wait..
            //splashScreen.Show(false);

            CultureInfo culture = null;

            if (ConfigurationSettings.AppSettings["Culture"] != null)
            {
                culture = new CultureInfo(ConfigurationSettings.AppSettings["Culture"]);
            }

            if (culture == null)
            {
                culture = new CultureInfo(CultureInfo.CurrentCulture.IetfLanguageTag);
            }

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }
    }
}
