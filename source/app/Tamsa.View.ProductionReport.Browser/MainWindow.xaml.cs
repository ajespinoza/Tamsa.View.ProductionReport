// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Tenaris">
//   Tenaris.
// </copyright>
// <summary>
//   Defines the MainWindow.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tamsa.View.ProductionReport.Browser
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;    
    using System.Reflection;
    using System.Text;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using System.Windows.Threading;
    using Tenaris.Library.ConnectionMonitor;
    using Tenaris.Library.Proxy.Exceptions;
    using Tenaris.Library.System.Factory;

    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Title = ConfigurationSettings.AppSettings["ApplicationTitle"].ToString();
        }
    }
}
