// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExitApplicationBrowserViewModel.cs" company="Tenaris">
//   Tenaris.
// </copyright>
// <summary>
//   Defines the ExitApplicationBrowserViewModel.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tamsa.View.ProductionReport.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Configuration;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using Tenaris.Library.UI.WPF.Internationalization;
    using Tamsa.View.ProductionReport.Model;
    
    /// <summary>
    /// ExitApplicationBrowserViewModel  class.
    /// </summary>
    public class BrowserViewModel : DependencyObject
    {
        #region Fields

        /// <summary>
        /// Default view dependency property
        /// </summary>
        public static readonly DependencyProperty DefaultViewProperty = DependencyProperty.Register("DefaultView", typeof(string), typeof(KeysViewModel), new PropertyMetadata(string.Empty));

        /// <summary>
        /// The activate exit management dependency property.
        /// </summary>
        public static readonly DependencyProperty ActiveExitManagementProperty = DependencyProperty.Register("ActiveExitManagement", typeof(string), typeof(BrowserViewModel), new PropertyMetadata(string.Empty));

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ExitApplicationBrowserViewModel"/> class.
        /// </summary>
        public BrowserViewModel()
        {
            try
            {
                this.ExitManagement = new KeysViewModel(string.Empty);
                this.SetApplicationMode();
                this.DefaultView = this.GetDefaultView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in the configuration client. Code:" + ex.Message);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the exit management property.
        /// </summary>
        public KeysViewModel ExitManagement { get; set; }

        /// <summary>
        /// Gets or sets default view property
        /// </summary>
        public string DefaultView
        {
            get { return (string)GetValue(DefaultViewProperty); }
            set { SetValue(DefaultViewProperty, value); }
        }

        /// <summary>
        /// Gets or sets the activate exit management property.
        /// </summary>
        public string ActiveExitManagement
        {
            get { return (string)GetValue(ActiveExitManagementProperty); }
            set { SetValue(ActiveExitManagementProperty, value); }
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// Set application mode
        /// </summary>
        /// <param name="applicationmode">
        /// The application mode.
        /// </param>
        internal void SetApplicationMode()
        {
            try
            {
                this.ActiveExitManagement = "Visible";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get zone index
        /// </summary>
        /// <param name="applicationmode">
        /// The application mode.
        /// </param>
        /// <param name="defaultview">
        /// The default view.
        /// </param>
        /// <returns>
        /// The default value.
        /// </returns>
        internal string GetDefaultView()
        {
            string index;
            try
            {
                index = "0";
                return index;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion 
    }
}

