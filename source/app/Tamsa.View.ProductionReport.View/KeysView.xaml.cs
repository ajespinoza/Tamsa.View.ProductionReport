// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExitManagementView.xaml.cs" company="Tenaris">
//   Tenaris.
// </copyright>
// <summary>
//   Defines the ExitManagementView.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tamsa.View.ProductionReport.View
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using Tamsa.View.ProductionReport.ViewModel;
    using Tamsa.View.ProductionReport.Model;
    /// <summary>
    /// Interaction logic for ExitManagementView.xaml
    /// </summary>
    public partial class KeysView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExitManagementView"/> class.
        /// </summary>
        public KeysView()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbxCommentsOnInsert.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            tbxCommentsOnInsert2.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            tbxCommentsOnInsert31.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            tbxCommentsOnInsert32.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void TrackingToBalance_SelectedCellsChanged(object sender, Microsoft.Windows.Controls.SelectedCellsChangedEventArgs e)
        {
            // Capturing the selected items from the grid and passing to ViewModel
            KeysViewModel keysViewModel = (KeysViewModel)base.DataContext;
            if (keysViewModel.SelectedTrackingsOnBalance != null)
            {
                keysViewModel.SelectedTrackingsOnBalance.Clear();
            }
            else
            {
                keysViewModel.SelectedTrackingsOnBalance = new List<Traceability>();
            }

            foreach (object item in TrackingToBalance.SelectedItems)
            {
                keysViewModel.SelectedTrackingsOnBalance.Add((Traceability)item);
            }

            keysViewModel.OnSelectionChangedTrackingToBalance();
        }


    }
}
