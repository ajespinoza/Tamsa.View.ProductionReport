// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KeysViewModel.cs" company="Tenaris">
//   Tenaris.
// </copyright>
// <summary>
//   Defines the SamplesViewModel.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tamsa.View.ProductionReport.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Configuration;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Resources;
    using System.Text;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Threading;
    using Tamsa.View.ProductionReport.Model;
    using Tenaris.Library.UI.WPF.Input;
    using Tenaris.Library.UI.WPF.Internationalization;
    using Tenaris.Manager.ExitApplication.Common;
    using Tenaris.Manager.ExitApplication.Support;
    using VerhoeffCheckDigitLibrary;
    using Tenaris.Library.ITServices;
    using ITManagement.BusinessLayer;
    using ITManagement.Models;
    using ITManagement.Common;
    using ITManagement.Common.ITServices;
    using Tamsa.View.ProductionReport.ViewModel;
    using Tenaris.Library.UI.Framework.ViewModel;
    using System.Xml.Serialization;
    using System.IO;
    using System.Xml;
    using Tamsa.View.ProductionReport.Model.Types;
    using Tenaris.Library.Log;


    /// <summary>
    /// SamplesViewModel class.
    /// </summary>
    public class KeysViewModel : BaseViewModel
    {
        #region Fields

        /// <summary>
        /// dispatcher timer.
        /// </summary>
        public DispatcherTimer dispatcherTimer;

        /// <summary>
        /// Process dependency property
        /// </summary>
        public static readonly DependencyProperty ProcessProperty = DependencyProperty.Register("Process", typeof(ObservableCollection<ComboBoxItem>), typeof(KeysViewModel), new PropertyMetadata(new ObservableCollection<ComboBoxItem>()));

        /// <summary>
        /// Event selection process.
        /// </summary>
        public static readonly DependencyProperty SelectedProcessProperty = DependencyProperty.Register("SelectedProcess", typeof(ComboBoxItem), typeof(KeysViewModel), new PropertyMetadata(null, SelectedProcessPropertyChanged));

        public static readonly DependencyProperty SelectedGroupElaborationStateProperty = DependencyProperty.Register("SelectedGroupElaborationState", typeof(GroupElaborationState), typeof(KeysViewModel), new PropertyMetadata(null, SelectedGroupElaborationStatePropertyChanged));
        public static readonly DependencyProperty SelectedElaborationStateProperty = DependencyProperty.Register("SelectedElaborationState", typeof(ElaborationState), typeof(KeysViewModel), new PropertyMetadata(null, SelectedElaborationStatePropertyChanged));
        
        /// <summary>
        /// Gets Summary
        /// </summary>
        public static readonly DependencyProperty SummaryForProductionReportProperty = DependencyProperty.Register("SummaryForProductionReport", typeof(BindingList<Summary>), typeof(KeysViewModel), new PropertyMetadata(new BindingList<Summary>()));

        public static readonly DependencyProperty ElaborationStateProperty = DependencyProperty.Register("ElaborationStates", typeof(ObservableCollection<ElaborationState>), typeof(KeysViewModel), new PropertyMetadata(new ObservableCollection<ElaborationState>()));

        /// <summary>
        /// Event for selection a summary.
        /// </summary>
        public static readonly DependencyProperty SelectedSummaryProperty = DependencyProperty.Register("SelectedSummary", typeof(Summary), typeof(KeysViewModel), new PropertyMetadata(null, SelectedSummaryPropertyChanged));

        /// <summary>
        /// Event for selection a detail.
        /// </summary>
        public static readonly DependencyProperty SelectedDetailProperty = DependencyProperty.Register("SelectedDetail", typeof(Detail), typeof(KeysViewModel), new PropertyMetadata(null, SelectedDetailPropertyChanged));

        /// <summary>
        /// Gets Detail
        /// </summary>
        public static readonly DependencyProperty DetailForProductionReportProperty = DependencyProperty.Register("DetailForProductionReport", typeof(BindingList<Detail>), typeof(KeysViewModel), new PropertyMetadata(new BindingList<Detail>()));

        /// <summary>
        /// Gets Detail
        /// </summary>
        public static readonly DependencyProperty DetailForGroupProperty = DependencyProperty.Register("DetailForGroup", typeof(BindingList<Traceability>), typeof(KeysViewModel), new PropertyMetadata(new BindingList<Traceability>()));

        /// <summary>
        /// Is enabled button balance dependency property
        /// </summary>
        public static readonly DependencyProperty IsEnabledButtonBalanceProperty = DependencyProperty.Register("IsEnabledButtonBalance", typeof(string), typeof(KeysViewModel), new PropertyMetadata("True"));

        /// <summary>
        /// Is enabled button asign dependency property
        /// </summary>
        public static readonly DependencyProperty IsEnabledButtonAsignProperty = DependencyProperty.Register("IsEnabledButtonAsign", typeof(string), typeof(KeysViewModel), new PropertyMetadata("True"));

        /// <summary>
        /// Is enabled button user dependency property
        /// </summary>
        public static readonly DependencyProperty IsEnabledButtonUserProperty = DependencyProperty.Register("IsEnabledButtonUser", typeof(string), typeof(KeysViewModel), new PropertyMetadata("True"));

        /// <summary>
        /// Difference dependency property
        /// </summary>
        public static readonly DependencyProperty DifferencePiecesProperty = DependencyProperty.Register("DifferencePieces", typeof(int), typeof(KeysViewModel), new PropertyMetadata(0));

        /// <summary>
        /// Balance Pieces dependency property
        /// </summary>
        public static readonly DependencyProperty BalancePiecesProperty = DependencyProperty.Register("BalancePieces", typeof(int), typeof(KeysViewModel), new PropertyMetadata(0));

        /// <summary>
        /// Comments dependency property
        /// </summary>
        public static readonly DependencyProperty CommentsProperty = DependencyProperty.Register("Comments", typeof(string), typeof(KeysViewModel), new PropertyMetadata(string.Empty));

        /// <summary>
        /// CommentsAsign dependency property
        /// </summary>
        public static readonly DependencyProperty CommentsAsignProperty = DependencyProperty.Register("CommentsAsign", typeof(string), typeof(KeysViewModel), new PropertyMetadata(string.Empty));

        /// <summary>
        /// Current user dependency property
        /// </summary>
        public static readonly DependencyProperty CurrentUserProperty = DependencyProperty.Register("CurrentUser", typeof(string), typeof(KeysViewModel), new PropertyMetadata(string.Empty));

        /// <summary>
        /// New user dependency property
        /// </summary>
        public static readonly DependencyProperty NewUserProperty = DependencyProperty.Register("NewUser", typeof(string), typeof(KeysViewModel), new PropertyMetadata(string.Empty));

        /// <summary>
        /// New user confirm dependency property
        /// </summary>
        public static readonly DependencyProperty NewUserConfirmProperty = DependencyProperty.Register("NewUserConfirm", typeof(string), typeof(KeysViewModel), new PropertyMetadata(string.Empty));

        /// <summary>
        /// ShowWarningBalanceComments dependency properties
        /// </summary>
        public static readonly DependencyProperty ShowWarningBalanceCommentsProperty = DependencyProperty.Register("ShowWarningBalanceComments", typeof(bool), typeof(KeysViewModel), new UIPropertyMetadata(false));

        /// <summary>
        /// ShowWarningBalanceDifference dependency properties
        /// </summary>
        public static readonly DependencyProperty ShowWarningBalanceDifferenceProperty = DependencyProperty.Register("ShowWarningBalanceDifference", typeof(bool), typeof(KeysViewModel), new UIPropertyMetadata(false));
        public static readonly DependencyProperty ShowWarningDataSelectedProperty = DependencyProperty.Register("ShowWarningDataSelected", typeof(bool), typeof(KeysViewModel), new UIPropertyMetadata(false));
        /// <summary>
        /// ShowWarningBalancePending dependency properties
        /// </summary>
        public static readonly DependencyProperty ShowWarningBalancePendingProperty = DependencyProperty.Register("ShowWarningBalancePending", typeof(bool), typeof(KeysViewModel), new UIPropertyMetadata(false));

        /// <summary>
        /// ShowWarningAsignPreview dependency properties
        /// </summary>
        public static readonly DependencyProperty ShowWarningAsignPreviewProperty = DependencyProperty.Register("ShowWarningAsignPreview", typeof(bool), typeof(KeysViewModel), new UIPropertyMetadata(false));

        /// <summary>
        /// ShowWarningAsignAccept dependency properties
        /// </summary>
        public static readonly DependencyProperty ShowWarningAsignAcceptProperty = DependencyProperty.Register("ShowWarningAsignAccept", typeof(bool), typeof(KeysViewModel), new UIPropertyMetadata(false));

        /// <summary>
        /// Show dialog print group
        /// </summary>
        public static readonly DependencyProperty IsOpenPrintTrackingGroupProperty = DependencyProperty.Register("IsOpenPrintTrackingGroup", typeof(bool), typeof(KeysViewModel), new UIPropertyMetadata(false));

        /// <summary>
        /// Show dialog send group
        /// </summary>
        public static readonly DependencyProperty IsOpenSendTrackingGroupProperty = DependencyProperty.Register("IsOpenSendTrackingGroup", typeof(bool), typeof(KeysViewModel), new UIPropertyMetadata(false));

        /// <summary>
        /// Show dialog user change
        /// </summary>
        public static readonly DependencyProperty IsOpenUserChangeProperty = DependencyProperty.Register("IsOpenUserChange", typeof(bool), typeof(KeysViewModel), new UIPropertyMetadata(false));

        /// <summary>
        ///  ReejectionCodes dependency property
        /// </summary>                            
        public static readonly DependencyProperty RejectionCodesProperty = DependencyProperty.Register("RejectionCodes", typeof(ObservableCollection<ComboBoxItem>), typeof(KeysViewModel), new PropertyMetadata(new ObservableCollection<ComboBoxItem>()));

        /// <summary>
        /// Captura de evento de seleccion rejection code en edit y metodo asociado desde combobox
        /// </summary>
        public static readonly DependencyProperty SelectedRejectionCodeProperty = DependencyProperty.Register("SelectedRejectionCode", typeof(ComboBoxItem), typeof(KeysViewModel), new PropertyMetadata(null, SelectedRejectionCodePropertyChanged));

        /// <summary>
        /// Gets TrackingToBalance
        /// </summary>
        public static readonly DependencyProperty TrackingToBalanceProperty = DependencyProperty.Register("TrackingToBalance", typeof(BindingList<Traceability>), typeof(KeysViewModel), new PropertyMetadata(new BindingList<Traceability>()));

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty PrintersProperty = DependencyProperty.Register("Printers", typeof(ObservableCollection<ComboBoxItem>), typeof(KeysViewModel), new PropertyMetadata(new ObservableCollection<ComboBoxItem>()));

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty LocationsProperty = DependencyProperty.Register("Locations", typeof(ObservableCollection<ComboBoxItem>), typeof(KeysViewModel), new PropertyMetadata(new ObservableCollection<ComboBoxItem>()));

        public static readonly DependencyProperty RejectionsProperty = DependencyProperty.Register("Rejections", typeof(ObservableCollection<ComboBoxItem>), typeof(KeysViewModel), new PropertyMetadata(new ObservableCollection<ComboBoxItem>()));

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty SelectedPrinterProperty = DependencyProperty.Register("SelectedPrinter", typeof(ComboBoxItem), typeof(KeysViewModel), new PropertyMetadata(null, SelectedPrinterPropertyChanged));

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty SelectedLocationProperty = DependencyProperty.Register("SelectedLocation", typeof(ComboBoxItem), typeof(KeysViewModel), new PropertyMetadata(null, SelectedLocationPropertyChanged));

        public static readonly DependencyProperty SelectedLocationToSendProperty = DependencyProperty.Register("SelectedLocationToSend", typeof(ComboBoxItem), typeof(KeysViewModel), new PropertyMetadata(null, SelectedLocationToSendPropertyChanged));
        public static readonly DependencyProperty SelectedRejectionToSendProperty = DependencyProperty.Register("SelectedRejectionToSend", typeof(ComboBoxItem), typeof(KeysViewModel), new PropertyMetadata(null, SelectedRejectionToSendPropertyChanged));

        public static readonly DependencyProperty IsEnableLocationToSendProperty = DependencyProperty.Register("IsEnableLocationToSend", typeof(bool), typeof(KeysViewModel), new UIPropertyMetadata(false));
        public static readonly DependencyProperty IsEnableRejectionToSendProperty = DependencyProperty.Register("IsEnableRejectionToSend", typeof(bool), typeof(KeysViewModel), new UIPropertyMetadata(false));

        // <summary>
        /// Show dialog send bundle
        /// </summary>
        public static readonly DependencyProperty IsOpenSendBundleProperty = DependencyProperty.Register("IsOpenSendBundle", typeof(bool), typeof(KeysViewModel), new UIPropertyMetadata(false));
        /// <summary>
        /// Gets or sets a value indicating whether Show dialog send bundle
        /// </summary>
        public bool IsOpenSendBundle
        {
            get { return (bool)GetValue(IsOpenSendBundleProperty); }
            set { SetValue(IsOpenSendBundleProperty, value); }
        }

        /// <summary>
        /// Gets or sets accept
        /// </summary>
        public DelegateCommand CmdAcceptSendBundle { get; set; }


        /// <summary>
        /// Gets or sets close
        /// </summary>
        public DelegateCommand CmdCloseSendBundle { get; set; }

        /// <summary>
        /// The accept send bundle
        /// </summary>
 
        private bool CanDoCmdAcceptSendBundle(object parameter)
        {
            return true;
        }

        private bool IsValidToSendIt()
        {
            if (!SelectedGroupElaborationState.Code.Equals("BUE"))
            {
                if (SelectedLocationToSend == null)
                    return false;

                if (SelectedRejectionToSend == null)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// The accept send bundle
        /// </summary>
        private void DoCmdAcceptSendBundle(object parameter)
        {
            try
            {
                if (SelectedGroupElaborationState != null && SelectedElaborationState != null)
                {
                    if (this.SelectedDetail != null)
                    {
                        if (this.SelectedDetail.Status != "Ok")
                        {
                            if (IsValidToSendIt())
                            {
                                Trace.Message("Start Send To Service");
                                Transaction tn = new Transaction(false, string.Empty, string.Empty, string.Empty, string.Empty);

                                var objListProcess = this.dataAccess.GetMachines();

                                // Obtengo maquinas.
                                var objmachine = objListProcess.FirstOrDefault(t => t.idProcess == Convert.ToInt32(this.SelectedProcess.Tag.ToString()));

                                // Ejecuto transaccion.
                                string xmlToBuild = string.Empty;
                                tn = this.SendGroupReport(objmachine.Code, out xmlToBuild);
                                Trace.Message("Finish Send To Service");

                                // Verifico estado de regreso.
                                int idstatus = 0;  // 3 ok, 4 error, 5 suspended
                                string tagNumber = string.Empty;

                                int liIdGroup = this.SelectedDetail.idGroup;
                                int liIdProcess = Convert.ToInt32(this.SelectedProcess.Tag.ToString());
                                int liIdBatch = this.SelectedSummary.idBatch;


                                if (tn.Status)
                                {
                                    idstatus = 3;
                                    tagNumber = tn.TagNumber;

                                    if (tagNumber != "0")
                                    {
                                        MessageBox.Show("Fue generado el atado: " + tagNumber);
                                        Trace.Message("The Bundle was generated");
                                    }
                                    else
                                    {
                                        idstatus = 4;
                                        MessageBox.Show("Envio parcial, no se generó etiqueta. Favor de reenviar el atado!");
                                        Trace.Message("Partial shipment, no label generated, please resend the bundle.");
                                    }
                                }
                                else
                                {
                                    idstatus = 4;
                                    tagNumber = "-";
                                    MessageBox.Show("Error en la generación del atado: " + tn.ResponseString);
                                    Trace.Message("Error on the bundle generated.");
                                }

                                // Actualizo grupo.
                                if (this.dataAccess.UpdGroup(liIdGroup, idstatus, tagNumber, tn.ResponseString, xmlToBuild))
                                {
                                    // Actualizo productos
                                    var a = this.dataAccess.GetProductsToReport(liIdProcess);
                                    this.SummaryForProductionReport = new BindingList<Summary>(a.Select(z => new Summary(z)).ToList());

                                    if (this.SummaryForProductionReport.Count > 0)
                                    {
                                        this.SelectedSummary = this.SummaryForProductionReport[0];

                                        // Actualizo grupos
                                        var b = this.dataAccess.GetGroupsForProduct(liIdProcess, liIdBatch);
                                        this.DetailForProductionReport = new BindingList<Detail>(b.Select(z => new Detail(z)).ToList());
                                    }
                                    else
                                    {
                                        this.DetailForProductionReport.Clear();
                                    }
                                }
                            }
                            else
                            {
                                this.ShowWarningDataSelected = true;
                            }
                        }
                        else
                        {
                            this.ShowWarningAsignPreview = true;
                        }
                    }
                    else
                    {
                        this.ShowWarningBalanceDifference = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       
        /// <summary>
        /// The close send bundle
        /// </summary>

        private bool CanDoCmdCloseSendBundle(object parameter)
        {
            return true;
        }

        /// <summary>
        /// The send bundle
        /// </summary>
        private void DoCmdCloseSendBundle(object parameter)
        {
            try
            {
                this.IsOpenSendBundle = false;
            }
            catch
            {
            }
        }





        /// <summary>
        /// The boolean object.
        /// </summary>
        public bool ban;

        /// <summary>
        /// Is notify change user.
        /// </summary>
        public bool IsNotifyChangeUser;

        /// <summary>
        /// Field for access to dataAccess.
        /// </summary>
        private DataAccess dataAccess;

        /// <summary>
        /// The log object.
        /// </summary>
        private Log log;

        /// <summary>
        /// selectedTrackingsOnBalance field
        /// </summary>
        private List<Traceability> selectedTrackingsOnBalance;

        private ObservableCollection<GroupElaborationState> groupElaborationStates = new ObservableCollection<GroupElaborationState>();
        private ObservableCollection<ElaborationState> elaborationStates = new ObservableCollection<ElaborationState>();
        private GroupElaborationState selectedGroupElaborationState = new GroupElaborationState();
        private ElaborationState selectedElaborationState = new ElaborationState();

        #endregion

        /// <summary>
        /// 
        /// </summary>
        private static BundleReportModel bundleReportSelected;

        private bool IsForceGranel;
        private bool IsFullMode;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="KeysViewModel"/> class.
        /// </summary>
        /// <param name="header">
        /// The header value.
        /// </param>
        public KeysViewModel(string header)
            : base(header)
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            try
            {
                //// Common commands.
                this.CmdValidateSelectedCommonTrackingGroup = new DelegateCommand(this.DoCmdValidateSelectedCommonTrackingGroup, this.CanDoCmdValidateSelectedCommonTrackingGroup);
                this.CmdSetDifferencePieces = new DelegateCommand(this.DoCmdSetDifferencePieces, this.CanDoCmdSetDifferencePieces);
                this.CmdSetComments = new DelegateCommand(this.DoCmdSetComments, this.CanDoCmdSetComments);
                this.CmdInsertBalance = new DelegateCommand(this.DoCmdInsertBalance, this.CanDoCmdInsertBalance);
                this.CmdCloseBalance = new DelegateCommand(this.DoCmdCloseBalance, this.CanDoCmdCloseBalance);

                this.CmdAcceptSendTrackingGroup = new DelegateCommand(this.DoCmdAcceptSendTrackingGroup, this.CanDoCmdAcceptSendTrackingGroup);
                this.CmdCloseSendTrackingGroup = new DelegateCommand(this.DoCmdCloseSendTrackingGroup, this.CanDoCmdCloseSendTrackingGroup);

                this.CmdAcceptUserChange = new DelegateCommand(this.DoCmdAcceptUserChange, this.CanDoCmdAcceptUserChange);
                this.CmdCloseUserChange = new DelegateCommand(this.DoCmdCloseUserChange, this.CanDoCmdCloseUserChange);

                this.CmdSetNewUser = new DelegateCommand(this.DoCmdSetNewUser, this.CanDoCmdSetNewUser);
                this.CmdSetNewUserConfirm = new DelegateCommand(this.DoCmdSetNewUserConfirm, this.CanDoCmdSetNewUserConfirm);

                this.CmdAcceptSendBundle = new DelegateCommand(this.DoCmdAcceptSendBundle, this.CanDoCmdAcceptSendBundle);
                
                this.CmdCloseSendBundle = new DelegateCommand(this.DoCmdCloseSendBundle, this.CanDoCmdCloseSendBundle);

                this.dispatcherTimer = new DispatcherTimer();
                this.dispatcherTimer.Tick += new EventHandler(this.DispatcherTimer_Tick);
                this.dispatcherTimer.Interval = new TimeSpan(0, 0, Convert.ToInt32(ClientConfiguration.Settings.ApplicationIntervalTimer.ToString()));
                this.dispatcherTimer.Start();

                this.dataAccess = new DataAccess();
                this.dataAccess.Activate();

                this.SetMachines();

                this.IsEnabledButtonUser = ClientConfiguration.Settings.IsEnableChangeUser;
                this.IsNotifyChangeUser = Convert.ToBoolean(ClientConfiguration.Settings.IsEnableNotifyChangeUser);
                this.IsForceGranel = Convert.ToBoolean(ClientConfiguration.Settings.IsForceGranel);
                this.IsFullMode= Convert.ToBoolean(ClientConfiguration.Settings.IsFullMode);
                this.ban = false;

                #region Carga de impresoras disponibles.

                ObservableCollection<ComboBoxItem> printers = new ObservableCollection<ComboBoxItem>();

                ComboBoxItem ps1 = new ComboBoxItem();
                ps1.Content = "MTEF";
                ps1.Tag = "MTEF";

                ComboBoxItem ps2 = new ComboBoxItem();
                ps2.Content = "MTE6";
                ps2.Tag = "MTE6";

                ComboBoxItem ps3 = new ComboBoxItem();
                ps3.Content = "MTF9";
                ps3.Tag = "MTF9";

                ComboBoxItem ps4 = new ComboBoxItem();
                ps4.Content = "MTFB";
                ps4.Tag = "MTFB";

                ComboBoxItem ps5 = new ComboBoxItem();
                ps5.Content = "MTE5";
                ps5.Tag = "MTE5";

                printers.Add(ps1);
                printers.Add(ps2);
                printers.Add(ps3);
                printers.Add(ps4);
                printers.Add(ps5);

                this.Printers = printers;

                #endregion

                #region Cargar ubicaciones disponibles.

                ObservableCollection<ComboBoxItem> locations = new ObservableCollection<ComboBoxItem>();

                ComboBoxItem ls0 = new ComboBoxItem();
                ls0.Content = string.Empty;
                ls0.Tag = string.Empty;

                //ComboBoxItem ls1 = new ComboBoxItem();
                //ls1.Content = "2063";
                //ls1.Tag = "2063";

                //ComboBoxItem ls2 = new ComboBoxItem();
                //ls2.Content = "2070";
                //ls2.Tag = "2070";

                //ComboBoxItem ls3 = new ComboBoxItem();
                //ls3.Content = "2075";
                //ls3.Tag = "2075";

                //ComboBoxItem ls4 = new ComboBoxItem();
                //ls4.Content = "4048";
                //ls4.Tag = "4048";

                //ComboBoxItem ls5 = new ComboBoxItem();
                //ls5.Content = "4035";
                //ls5.Tag = "4035";

                //ComboBoxItem ls6 = new ComboBoxItem();
                //ls6.Content = "4036";
                //ls6.Tag = "4036";

                //ComboBoxItem ls7 = new ComboBoxItem();
                //ls7.Content = "4037";
                //ls7.Tag = "4037";

                //ComboBoxItem ls8 = new ComboBoxItem();
                //ls8.Content = "4027";
                //ls8.Tag = "4027";

                ComboBoxItem ls1 = new ComboBoxItem();
                ls1.Content = "DESP";
                ls1.Tag = "DESP";

                ComboBoxItem ls2 = new ComboBoxItem();
                ls2.Content = "CSAL";
                ls2.Tag = "CSAL";

                ComboBoxItem ls3 = new ComboBoxItem();
                ls3.Content = "CSAL2";
                ls3.Tag = "CSAL2";

                locations.Add(ls0);
                locations.Add(ls1);
                locations.Add(ls2);
                locations.Add(ls3);
                //locations.Add(ls4);
                //locations.Add(ls5);
                //locations.Add(ls6);
                //locations.Add(ls7);
                //locations.Add(ls8);
                //locations.Add(ls9);
                //locations.Add(ls10);

                this.Locations = locations;

                //GroupElaborationStates = this.dataAccess.GetGroupElaborationState();

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in exit management view build:" + ex.Message);
            }
        }

        #endregion

        #region Properties

        public ObservableCollection<GroupElaborationState> GroupElaborationStates
        {
            get
            {
                return this.groupElaborationStates;
            }
            set
            {
                this.groupElaborationStates = value;
            }
        }
        public GroupElaborationState SelectedGroupElaborationState
        {
            get { return (GroupElaborationState)GetValue(SelectedGroupElaborationStateProperty); }
            set { SetValue(SelectedGroupElaborationStateProperty, value); }

        }

        public ObservableCollection<ElaborationState> ElaborationStates
        {
            get { return (ObservableCollection<ElaborationState>)GetValue(ElaborationStateProperty); }
            set { SetValue(ElaborationStateProperty, value); }
        }

        public ElaborationState SelectedElaborationState
        {
            get { return (ElaborationState)GetValue(SelectedElaborationStateProperty); }
            set { SetValue(SelectedElaborationStateProperty, value); }

        }

        /// <summary>
        /// Gets or sets process property
        /// </summary>
        public ObservableCollection<ComboBoxItem> Process
        {
            get { return (ObservableCollection<ComboBoxItem>)GetValue(ProcessProperty); }
            set { SetValue(ProcessProperty, value); }
        }

        /// <summary>
        /// Gets or sets selected process.
        /// </summary>
        public ComboBoxItem SelectedProcess
        {
            get { return (ComboBoxItem)GetValue(SelectedProcessProperty); }
            set { SetValue(SelectedProcessProperty, value); }
        }

        /// <summary>
        /// Gets or sets summary
        /// </summary>
        public BindingList<Summary> SummaryForProductionReport
        {
            get { return (BindingList<Summary>)GetValue(SummaryForProductionReportProperty); }
            set { SetValue(SummaryForProductionReportProperty, value); }
        }

        /// <summary>
        /// Gets or sets selected summary property.
        /// </summary>
        public Summary SelectedSummary
        {
            get { return (Summary)GetValue(SelectedSummaryProperty); }
            set { SetValue(SelectedSummaryProperty, value); }
        }

        /// <summary>
        /// Gets or sets selected detail property.
        /// </summary>
        public Detail SelectedDetail
        {
            get { return (Detail)GetValue(SelectedDetailProperty); }
            set { SetValue(SelectedDetailProperty, value); }
        }

        /// <summary>
        /// Gets or sets detail
        /// </summary>
        public BindingList<Detail> DetailForProductionReport
        {
            get { return (BindingList<Detail>)GetValue(DetailForProductionReportProperty); }
            set { SetValue(DetailForProductionReportProperty, value); }
        }

        /// <summary>
        /// Gets or sets detail
        /// </summary>
        public BindingList<Traceability> DetailForGroup
        {
            get { return (BindingList<Traceability>)GetValue(DetailForGroupProperty); }
            set { SetValue(DetailForGroupProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Show dialog print group
        /// </summary>
        public bool IsOpenPrintTrackingGroup
        {
            get { return (bool)GetValue(IsOpenPrintTrackingGroupProperty); }
            set { SetValue(IsOpenPrintTrackingGroupProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Show dialog send group
        /// </summary>
        public bool IsOpenSendTrackingGroup
        {
            get { return (bool)GetValue(IsOpenSendTrackingGroupProperty); }
            set { SetValue(IsOpenSendTrackingGroupProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Show dialog user change
        /// </summary>
        public bool IsOpenUserChange
        {
            get { return (bool)GetValue(IsOpenUserChangeProperty); }
            set { SetValue(IsOpenUserChangeProperty, value); }
        }

        /// <summary>
        /// Gets or sets enabled button banlance  property
        /// </summary>
        public string IsEnabledButtonBalance
        {
            get { return (string)GetValue(IsEnabledButtonBalanceProperty); }
            set { SetValue(IsEnabledButtonBalanceProperty, value); }
        }

        /// <summary>
        /// Gets or sets enabled button asign  property
        /// </summary>
        public string IsEnabledButtonAsign
        {
            get { return (string)GetValue(IsEnabledButtonAsignProperty); }
            set { SetValue(IsEnabledButtonAsignProperty, value); }
        }

        /// <summary>
        /// Gets or sets enabled button user property
        /// </summary>
        public string IsEnabledButtonUser
        {
            get { return (string)GetValue(IsEnabledButtonUserProperty); }
            set { SetValue(IsEnabledButtonUserProperty, value); }
        }

        /// <summary>
        /// Gets or sets difference pieces  property
        /// </summary>
        public int DifferencePieces
        {
            get { return (int)GetValue(DifferencePiecesProperty); }
            set { SetValue(DifferencePiecesProperty, value); }
        }

        /// <summary>
        /// Gets or sets balance pieces  property
        /// </summary>
        public int BalancePieces
        {
            get { return (int)GetValue(BalancePiecesProperty); }
            set { SetValue(BalancePiecesProperty, value); }
        }

        /// <summary>
        /// Gets or sets comments  property
        /// </summary>
        public string Comments
        {
            get { return (string)GetValue(CommentsProperty); }
            set { SetValue(CommentsProperty, value); }
        }

        /// <summary>
        /// Gets or sets comments asign  property
        /// </summary>
        public string CommentsAsign
        {
            get { return (string)GetValue(CommentsAsignProperty); }
            set { SetValue(CommentsAsignProperty, value); }
        }

        /// <summary>
        /// Gets or sets current user  property
        /// </summary>
        public string CurrentUser
        {
            get { return (string)GetValue(CurrentUserProperty); }
            set { SetValue(CurrentUserProperty, value); }
        }

        /// <summary>
        /// Gets or sets new user  property
        /// </summary>
        public string NewUser
        {
            get { return (string)GetValue(NewUserProperty); }
            set { SetValue(NewUserProperty, value); }
        }

        /// <summary>
        /// Gets or sets new user confirm  property
        /// </summary>
        public string NewUserConfirm
        {
            get { return (string)GetValue(NewUserConfirmProperty); }
            set { SetValue(NewUserConfirmProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether ShowWarningBalanceComments properties
        /// </summary>
        public bool ShowWarningBalanceComments
        {
            get { return (bool)GetValue(ShowWarningBalanceCommentsProperty); }
            set { SetValue(ShowWarningBalanceCommentsProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether ShowWarningBalanceDifference properties
        /// </summary>
        public bool ShowWarningBalanceDifference
        {
            get { return (bool)GetValue(ShowWarningBalanceDifferenceProperty); }
            set { SetValue(ShowWarningBalanceDifferenceProperty, value); }
        }

        public bool ShowWarningDataSelected
        {
            get { return (bool)GetValue(ShowWarningDataSelectedProperty); }
            set { SetValue(ShowWarningDataSelectedProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether ShowWarningBalancePending properties
        /// </summary>
        public bool ShowWarningBalancePending
        {
            get { return (bool)GetValue(ShowWarningBalancePendingProperty); }
            set { SetValue(ShowWarningBalancePendingProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether ShowWarningAsignPreview properties
        /// </summary>
        public bool ShowWarningAsignPreview
        {
            get { return (bool)GetValue(ShowWarningAsignPreviewProperty); }
            set { SetValue(ShowWarningAsignPreviewProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether ShowWarningAsignAccept properties
        /// </summary>
        public bool ShowWarningAsignAccept
        {
            get { return (bool)GetValue(ShowWarningAsignAcceptProperty); }
            set { SetValue(ShowWarningAsignAcceptProperty, value); }
        }

        /// <summary>
        /// Gets or sets rejection code property
        /// </summary>
        public ObservableCollection<ComboBoxItem> RejectionCodes
        {
            get { return (ObservableCollection<ComboBoxItem>)GetValue(RejectionCodesProperty); }
            set { SetValue(RejectionCodesProperty, value); }
        }

        /// <summary>
        /// Gets or sets selected rejection code on edit property.
        /// </summary>
        public ComboBoxItem SelectedRejectionCode
        {
            get { return (ComboBoxItem)GetValue(SelectedRejectionCodeProperty); }
            set { SetValue(SelectedRejectionCodeProperty, value); }
        }

        /// <summary>
        /// Gets or sets tracking to balance.
        /// </summary>
        public BindingList<Traceability> TrackingToBalance
        {
            get { return (BindingList<Traceability>)GetValue(TrackingToBalanceProperty); }
            set { SetValue(TrackingToBalanceProperty, value); }
        }

        /// <summary>
        /// Gets or sets Selected tracking to balance list
        /// </summary>
        public List<Traceability> SelectedTrackingsOnBalance
        {
            get { return this.selectedTrackingsOnBalance; }
            set { this.selectedTrackingsOnBalance = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<ComboBoxItem> Printers
        {
            get { return (ObservableCollection<ComboBoxItem>)GetValue(PrintersProperty); }
            set { SetValue(PrintersProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<ComboBoxItem> Locations
        {
            get { return (ObservableCollection<ComboBoxItem>)GetValue(LocationsProperty); }
            set { SetValue(LocationsProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<ComboBoxItem> Rejections
        {
            get { return (ObservableCollection<ComboBoxItem>)GetValue(RejectionsProperty); }
            set { SetValue(RejectionsProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public ComboBoxItem SelectedPrinter
        {
            get { return (ComboBoxItem)GetValue(SelectedPrinterProperty); }
            set { SetValue(SelectedPrinterProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public ComboBoxItem SelectedLocation
        {
            get { return (ComboBoxItem)GetValue(SelectedLocationProperty); }
            set { SetValue(SelectedLocationProperty, value); }
        }

        public ComboBoxItem SelectedLocationToSend
        {
            get { return (ComboBoxItem)GetValue(SelectedLocationToSendProperty); }
            set { SetValue(SelectedLocationToSendProperty, value); }
        }

        public ComboBoxItem SelectedRejectionToSend
        {
            get { return (ComboBoxItem)GetValue(SelectedRejectionToSendProperty); }
            set { SetValue(SelectedRejectionToSendProperty, value); }
        }

        

        public bool IsEnableLocationToSend
        {
            get { return (bool)GetValue(IsEnableLocationToSendProperty); }
            set { SetValue(IsEnableLocationToSendProperty, value); }
        }
        public bool IsEnableRejectionToSend
        {
            get { return (bool)GetValue(IsEnableRejectionToSendProperty); }
            set { SetValue(IsEnableRejectionToSendProperty, value); }
        }

        #region Delegates 

        /// <summary>
        /// Delegate machine changed.
        /// </summary>
        public delegate void DelegateMachineChanged();

        /// <summary>
        /// Gets or sets commands for to set Difference pieces
        /// </summary>
        public DelegateCommand CmdSetDifferencePieces { get; set; }

        /// <summary>
        /// Gets or sets commands for to set Comments
        /// </summary>
        public DelegateCommand CmdSetComments { get; set; }

        /// <summary>
        /// Gets or sets the insert balance
        /// </summary>
        public DelegateCommand CmdInsertBalance { get; set; }

        /// <summary>
        /// Gets or sets the close balance
        /// </summary>
        public DelegateCommand CmdCloseBalance { get; set; }

        /// <summary>
        /// Gets or sets the validate.
        /// </summary>
        public DelegateCommand CmdValidateSelectedCommonTrackingGroup { get; set; }

        /// <summary>
        /// Gets or sets the accept.
        /// </summary>
        public DelegateCommand CmdAcceptSendTrackingGroup { get; set; }

        /// <summary>
        /// Gets or sets the close send.
        /// </summary>
        public DelegateCommand CmdCloseSendTrackingGroup { get; set; }

        /// <summary>
        /// Gets or sets accept
        /// </summary>
        public DelegateCommand CmdAcceptUserChange { get; set; }

        /// <summary>
        /// Gets or sets close
        /// </summary>
        public DelegateCommand CmdCloseUserChange { get; set; }

        /// <summary>
        /// Gets or sets commands for to set New user
        /// </summary>
        public DelegateCommand CmdSetNewUser { get; set; }

        /// <summary>
        /// Gets or sets commands for to set New user confirm
        /// </summary>
        public DelegateCommand CmdSetNewUserConfirm { get; set; }

        /// <summary>
        /// Delegate for event change user
        /// </summary>
        public delegate void DelegateUserChanged();

        #endregion

        #endregion

        #region Internal methods

        /// <summary>
        /// The dispatcher timer
        /// </summary>
        /// <param name="sender">
        /// The sender object.
        /// </param>
        /// <param name="e">
        /// The event args.
        /// </param>
        internal void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                var usr = this.dataAccess.GetCurrentUser(1);

                this.CurrentUser = usr.CurrentUser;
                string date = usr.LastDateUpdate;

                var a = this.dataAccess.GetProductsToReport(Convert.ToInt32(this.SelectedProcess.Tag.ToString()));
                this.SummaryForProductionReport = new BindingList<Summary>(a.Select(z => new Summary(z)).ToList());

                if (this.SummaryForProductionReport.Count > 0)
                {
                    this.SelectedSummary = this.SummaryForProductionReport[0];
                }
                else
                {
                    this.DetailForProductionReport.Clear();
                }

                if (this.IsNotifyChangeUser)
                {
                    var currentShift = this.dataAccess.GetShift(DateTime.Now.ToString());

                    var initialTime = Convert.ToDateTime(currentShift.Initial);
                    var lastUpdTime = Convert.ToDateTime(date);

                    if (lastUpdTime < initialTime && this.ban != true)
                    {
                        System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(this.RunsOnWorkerThread));
                        t.Start();
                        this.ban = true;
                    }
                }
            }
            catch (Exception ex)
            {
                this.log.Exception(ex, "Exception on KeysViewModel:" + ex.Message, null);
            }
        }

        /// <summary>
        /// Set machines to visualize.
        /// </summary>
        internal void SetMachines()
        {
            try
            {
                var processCode = new ObservableCollection<ComboBoxItem>();
                var stringProcess = ClientConfiguration.Settings.ApplicationProcess.Trim().Split(new char[] { ',' }).ToList();

                var process = string.Empty;
                if (stringProcess.Count > 0)
                {
                    process = stringProcess[0];
                }
                else
                {
                    this.log.Exception(null, "Error: Not exist a specific process!", null);
                }

                this.Process.Clear();

                var objListProcess = this.dataAccess.GetMachines();

                foreach (var p in objListProcess)
                {
                    if (stringProcess.Contains(p.Code))
                    {
                        var objpro = new ComboBoxItem();

                        objpro.Content = p.Name;
                        objpro.Tag = p.idProcess;
                        processCode.Add(objpro);
                    }
                }

                this.Process = processCode;

                this.SelectedProcess = this.GetProcess(processCode, process);
            }
            catch (Exception ex)
            {
                this.log.Exception(ex, "Exception on KeysViewModel:" + ex.Message, null);
            }
        }

        /// <summary>
        /// Get default process to visualize.
        /// </summary>
        /// <param name="process">
        /// The process list.
        /// </param>
        /// <param name="p">
        /// The defualt process.
        /// </param>
        /// <returns>
        /// The combo box object.
        /// </returns>
        internal ComboBoxItem GetProcess(ObservableCollection<ComboBoxItem> process, string p)
        {
            var defaultProcess = new ComboBoxItem();
            try
            {
                defaultProcess = process[0];
                var ienum = process.Select(x => x as ComboBoxItem);
                foreach (var x in ienum)
                {
                    if (x.Tag.ToString().Trim() == p.Trim())
                    {
                        defaultProcess = x;
                        break;
                    }
                }

                return defaultProcess;
            }
            catch (Exception ex)
            {
                this.log.Exception(ex, "Exception on KeysViewModel:" + ex.Message, null);
                return defaultProcess;
            }
        }

        /// <summary>
        /// Update detail for key.
        /// </summary>
        /// <param name="idBatch">
        /// The idbatch value.
        /// </param>
        /// <param name="idProcess">
        /// The id process value.
        /// </param>
        internal void UpdateDetail(int idBatch, int idMachine)
        {
            try
            {
                this.DetailForProductionReport.Clear();

                var b = this.dataAccess.GetGroupsForProduct(idMachine, idBatch);
                this.DetailForProductionReport = new BindingList<Detail>(b.Select(z => new Detail(z)).ToList());

                if (this.DetailForProductionReport.Count > 0)
                {
                    this.SelectedDetail = this.DetailForProductionReport[0];
                }
            }
            catch (Exception ex)
            {
                this.log.Exception(ex, "Exception on KeysViewModel:" + ex.Message, null);
            }
        }

        internal void UpdatePipes(int idGroup)
        {
            try
            {
                this.DetailForGroup.Clear();

                var b = this.dataAccess.GetTrackingsByGroup(idGroup);
                this.DetailForGroup = new BindingList<Traceability>(b.Select(z => new Traceability(z)).ToList());
            }
            catch (Exception ex)
            {
                this.log.Exception(ex, "Exception on KeysViewModel:" + ex.Message, null);
            }
        }

        /// <summary>
        /// Set validations with options.
        /// </summary>
        /// <param name="command">
        /// The command value.
        /// </param>
        private ObservableCollection<ComboBoxItem> GetRejectionList()
        {
            ObservableCollection<ComboBoxItem> rejections = new ObservableCollection<ComboBoxItem>();
            ComboBoxItem cb0 = new ComboBoxItem();
            cb0.Content = string.Empty;
            cb0.Tag = string.Empty;
            rejections.Add(cb0);
            if(SelectedElaborationState != null)
                rejections.AddRange(this.dataAccess.GetRejections(SelectedElaborationState.IdElaborationState));

            return rejections;
        }
        internal void SetValidations(string command)
        {
            try
            {
                switch (command)
                {
                    case "Generar":
                        {
                            // Obtengo diferencia
                            if (this.SelectedSummary != null)
                            {
                                this.DifferencePieces = this.SelectedSummary.LoadedCount - this.SelectedSummary.ProducedCount;
                                this.BalancePieces = 0;
                                this.Comments = string.Empty;

                                this.ShowWarningBalanceComments = false;
                                this.ShowWarningBalanceDifference = false;
                                this.ShowWarningBalancePending = false;

                                var trks = this.dataAccess.GetAvailableTrackings(this.SelectedSummary.idBatch, Convert.ToInt32(this.SelectedProcess.Tag.ToString()), Convert.ToInt32(this.IsFullMode));
                                this.TrackingToBalance = new BindingList<Traceability>(trks.Select(z => new Traceability(z)).ToList());

                                this.dispatcherTimer.Stop();

                                // Muestro pantalla
                                this.IsOpenPrintTrackingGroup = true;
                            }
                            break;
                        }

                    case "Enviar":
                        {
                            GroupElaborationStates.Clear();
                            GroupElaborationStates.AddRange(this.dataAccess.GetGroupElaborationState());
                            SelectedGroupElaborationState = GroupElaborationStates.FirstOrDefault(x => x.Code.Equals(ClientConfiguration.Settings.DefaultGroupEECode));
                            
                            ElaborationStates = GetListElaborationsStates();
                            SelectedElaborationState = ElaborationStates.FirstOrDefault(x => x.Code.Equals(ClientConfiguration.Settings.DefaultEECode));
                            SelectedLocationToSend = this.SelectedLocation;
                            Rejections = GetRejectionList();

                            this.IsOpenSendBundle = true;

                            break;
                        }

                    case "Borrar":
                        {
                            if (this.SelectedDetail != null)
                            {
                                if (this.SelectedDetail.Status != "Ok" && this.SelectedDetail.GroupNumber!="0")
                                {
                                    // borro grupo.
                                    if (this.dataAccess.DelGroup(this.SelectedDetail.idGroup))
                                    {
                                        // Actualizo registros en grid
                                        var b = this.dataAccess.GetGroupsForProduct(Convert.ToInt32(this.SelectedProcess.Tag.ToString()), this.SelectedSummary.idBatch);
                                        this.DetailForProductionReport = new BindingList<Detail>(b.Select(z => new Detail(z)).ToList());

                                        MessageBox.Show("El Atado ha sido borrado satisfactoriamente!");
                                    }
                                }
                                else
                                {
                                    this.ShowWarningAsignPreview = true;
                                }
                            }
                            else
                            {
                                this.ShowWarningBalanceDifference = true;
                            }

                            break;
                        }

                    case "Reload":
                        {
                            // Actualizo productos
                            var a = this.dataAccess.GetProductsToReport(Convert.ToInt32(this.SelectedProcess.Tag.ToString()));
                            this.SummaryForProductionReport = new BindingList<Summary>(a.Select(z => new Summary(z)).ToList());

                            if (this.SummaryForProductionReport.Count > 0)
                            {
                                this.SelectedSummary = this.SummaryForProductionReport[0];

                                // Actualizo grupos
                                var b = this.dataAccess.GetGroupsForProduct(Convert.ToInt32(this.SelectedProcess.Tag.ToString()), this.SelectedSummary.idBatch);
                                this.DetailForProductionReport = new BindingList<Detail>(b.Select(z => new Detail(z)).ToList());
                            }
                            else
                            {
                                this.DetailForProductionReport.Clear();
                            } 

                            break;
                        }
                }

                this.ShowWarningAsignPreview = false;
                this.ShowWarningBalanceDifference = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Set run worker
        /// </summary>
        internal void RunsOnWorkerThread()
        {
            DelegateMachineChanged machineDelegateChange = delegate
            {
                this.NotifyUser();
            };
            this.Dispatcher.BeginInvoke(machineDelegateChange, System.Windows.Threading.DispatcherPriority.Normal);
        }

        /// <summary>
        /// Set notify user.
        /// </summary>
        internal void NotifyUser()
        {
            MessageBox.Show("El usuario fue cambiado en un turno diferente al actual, favor de reasignar!", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
            this.ban = false;
        }

        #endregion

        #region Private methods
        
        #region Dependency methods

        /// <summary>
        ///  selected process property.
        /// </summary>
        /// <param name="source">
        /// The source value.
        /// </param>
        /// <param name="e">
        /// The event args.
        /// </param>
        private static void SelectedProcessPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            KeysViewModel viewModel = source as KeysViewModel;
            viewModel.DoSelectedProcess();
        }

        private static void SelectedGroupElaborationStatePropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            KeysViewModel viewModel = source as KeysViewModel;
            viewModel.DoSelectedGroupElaborationState();
        }
        private static void SelectedElaborationStatePropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            KeysViewModel viewModel = source as KeysViewModel;
            viewModel.DoSelectedElaborationState();
        }

        /// <summary>
        /// Selected summary property changed.
        /// </summary>
        /// <param name="source">
        /// Dependency object.
        /// </param>
        /// <param name="e">
        /// Dependency property event.
        /// </param>
        private static void SelectedSummaryPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            KeysViewModel viewModel = source as KeysViewModel;
            viewModel.DoSelectedSummary();
        }

        /// <summary>
        /// Selected detail property changed.
        /// </summary>
        /// <param name="source">
        /// Dependency object.
        /// </param>
        /// <param name="e">
        /// Dependency property event.
        /// </param>
        private static void SelectedDetailPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            KeysViewModel viewModel = source as KeysViewModel;
            viewModel.DoSelectedDetail();
        }

        /// <summary>
        /// Selected rejection code on edit property changed.
        /// </summary>
        /// <param name="source">
        /// Dependency object.
        /// </param>
        /// <param name="e">
        /// Dependency proeprty event.
        /// </param>
        private static void SelectedRejectionCodePropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            KeysViewModel viewModel = source as KeysViewModel;
            viewModel.DoSelectedRejectionCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private static void SelectedPrinterPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            KeysViewModel viewModel = source as KeysViewModel;
            viewModel.DoSelectedPrinter();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private static void SelectedLocationPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            KeysViewModel viewModel = source as KeysViewModel;
            viewModel.DoSelectedLocation();
        }
        private static void SelectedLocationToSendPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            KeysViewModel viewModel = source as KeysViewModel;
            viewModel.DoSelectedLocationToSend();
        }

        private static void SelectedRejectionToSendPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            KeysViewModel viewModel = source as KeysViewModel;
            viewModel.DoSelectedRejectionToSend();
        }
        

        /// <summary>
        /// Selected process method.
        /// </summary>
        private void DoSelectedProcess()
        {
            try
            {
                var selected = this.SelectedProcess;

                var usr = this.dataAccess.GetCurrentUser(1);
                this.CurrentUser = usr.CurrentUser;

                var a = this.dataAccess.GetProductsToReport(Convert.ToInt32(selected.Tag.ToString()));
                this.SummaryForProductionReport = new BindingList<Summary>(a.Select(z => new Summary(z)).ToList());

                if (this.SummaryForProductionReport.Count > 0)
                {
                    this.SelectedSummary = this.SummaryForProductionReport[0];
                }
                else
                {
                    this.DetailForProductionReport.Clear();
                }
            }
            catch (Exception ex)
            {
                this.log.Exception(ex, "Exception on KeysViewModel:" + ex.Message, null);
            }
        }

        /// <summary>
        /// Selected group method.
        /// </summary>
        private void DoSelectedSummary()
        {
            try
            {
                if (this.SelectedSummary != null)
                {
                    Summary selected = this.SelectedSummary;

                    // Valido opcion de balance.
                    if (this.SelectedSummary != null)
                    {
                        // Validacion para balance.
                        if (this.SelectedSummary.LoadedCount == this.SelectedSummary.EnteredCount && this.SelectedSummary.LoadedCount == this.SelectedSummary.ProducedCount)
                        {
                            this.IsEnabledButtonBalance = "True";// "False";
                        }
                        else 
                        {
                            this.IsEnabledButtonBalance = "True";
                        }

                        this.IsEnabledButtonAsign = "True";
                    }

                    // obtener idprocess de combo
                    int idprocess = Convert.ToInt32(this.SelectedProcess.Tag.ToString());
                    this.UpdateDetail(selected.idBatch, idprocess);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Selected detail method.
        /// </summary>
        private void DoSelectedDetail()
        {
            try
            {
                this.DetailForGroup.Clear();

                if (this.SelectedDetail != null)
                {
                    this.UpdatePipes(this.SelectedDetail.idGroup);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void DoSelectedPrinter()
        {
            try
            {


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void DoSelectedLocation()
        {
            try
            {


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DoSelectedLocationToSend()
        {
            try
            {


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DoSelectedRejectionToSend()
        {
            try
            {


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DoIsEnableLocationToSend()
        {
            try
            {


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private ObservableCollection<ElaborationState> GetListElaborationsStates()
        {
            ObservableCollection<ElaborationState> list;
            if (SelectedGroupElaborationState != null)
            {
                list = new ObservableCollection<ElaborationState>();
                list.Add(new ElaborationState(0, String.Empty, String.Empty, String.Empty));
                list.AddRange(this.dataAccess.GetElaborationStateByGroup(SelectedGroupElaborationState.IdGroupElaborationState));
            }
            else
            {
                list = new ObservableCollection<ElaborationState>();
            }
            return list;
        }
        
        private void DoSelectedGroupElaborationState()
        {
            try
            {
                if(SelectedGroupElaborationState != null)
                {
                    ElaborationStates = GetListElaborationsStates();
                    if (SelectedGroupElaborationState.Code.Equals("BUE"))
                    {
                        IsEnableLocationToSend = false;
                        SelectedLocationToSend = null;
                        IsEnableRejectionToSend = false;
                        SelectedRejectionToSend = null;
                    }
                    else
                    {
                        IsEnableLocationToSend = true;
                        IsEnableRejectionToSend = true;
                    }
                }
                
            }
            catch (Exception ex)
            {
                this.log.Exception(ex, "Exception on KeysViewModel:" + ex.Message, null);
            }
        }

        private void DoSelectedElaborationState()
        {
            try
            {
                Rejections = GetRejectionList();
            }
            catch(Exception ex)
            {
                this.log.Exception(ex, "Exception on KeysViewModel: " + ex.Message, null);
            }
        }

        /// <summary>
        /// The set difference pieces.
        /// </summary>
        /// <param name="parameter">
        /// The parameter value.
        /// </param>
        /// <returns>
        /// The result value.
        /// </returns>
        private bool CanDoCmdSetDifferencePieces(object parameter)
        {
            return true;
        }

        /// <summary>
        /// The set difference pieces
        /// </summary>
        /// <param name="parameter">
        /// The parameter object.
        /// </param>
        private void DoCmdSetDifferencePieces(object parameter)
        {
        }

        /// <summary>
        /// The set comments.
        /// </summary>
        /// <param name="parameter">
        /// The parameter value.
        /// </param>
        /// <returns>
        /// The result value.
        /// </returns>
        private bool CanDoCmdSetComments(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Set comments.
        /// </summary>
        /// <param name="parameter">
        /// The parameter object.
        /// </param>
        private void DoCmdSetComments(object parameter)
        {
            var s = "";
        }

        /// <summary>
        /// The insert balance.
        /// </summary>
        /// <param name="parameter">
        /// The paremeter value.
        /// </param>
        /// <returns>
        /// The result value.
        /// </returns>
        private bool CanDoCmdInsertBalance(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Insert balance
        /// </summary>
        /// <param name="parameter">
        /// The parameter value.
        /// </param>
        private void DoCmdInsertBalance(object parameter)
        {
            try
            {
                // Valido impresora.
                if (this.SelectedPrinter == null)
                {
                    this.ShowWarningBalancePending = true;
                }
                else
                {
                    // Valido piezas a insertar.
                    if (this.BalancePieces > 0)
                    {
                        ProductionReport.ViewModel.ITServices.TServiceClient serviceObject = new ProductionReport.ViewModel.ITServices.TServiceClient();

                        // Get new GUIDAscii85
                        string transaction = serviceObject.GetNewAscii85();

                        // Obtengo tabla de trackings.
                        var dt = this.GetDataTableFromItemList(this.SelectedTrackingsOnBalance);

                        string loc = string.Empty;

                        // Valido ubicación.
                        if (this.SelectedLocation == null)
                        {
                            loc = string.Empty;
                        }
                        else
                        {
                            loc = this.SelectedLocation.Tag.ToString();
                        }

                        // Inserto grupo y sus trackings
                        if (this.dataAccess.InsGroup(this.SelectedSummary.idBatch, Convert.ToInt32(this.SelectedProcess.Tag.ToString()),this.CurrentUser, "P", loc, this.SelectedPrinter.Tag.ToString(), this.Comments, transaction, dt))
                        {
                             // Actualizo registros en grid
                            var b = this.dataAccess.GetGroupsForProduct(Convert.ToInt32(this.SelectedProcess.Tag.ToString()),this.SelectedSummary.idBatch);
                            this.DetailForProductionReport = new BindingList<Detail>(b.Select(z => new Detail(z)).ToList());
                            serviceObject = null;
                        }
                    }
                    else
                    {
                        this.ShowWarningBalanceComments = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// The close command value.
        /// </summary>
        /// <param name="parameter">
        /// The parameter value.
        /// </param>
        /// <returns>
        /// The result value.
        /// </returns>
        private bool CanDoCmdCloseBalance(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Close balance
        /// </summary>
        /// <param name="parameter">
        /// The parameter value.
        /// </param>
        private void DoCmdCloseBalance(object parameter)
        {
             this.dispatcherTimer.Start();
             this.IsOpenPrintTrackingGroup = false;
        }

        /// <summary>
        /// Gets a value indicating whether is validate selected group.
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <returns>
        /// Validation status true.
        /// </returns>
        private bool CanDoCmdValidateSelectedCommonTrackingGroup(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Selected group method.
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        private void DoCmdValidateSelectedCommonTrackingGroup(object parameter)
        {
            try
            {
                // valida que comandos fueron seleccionados y en que datagrid
                this.SetValidations(parameter.ToString().Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Send trackig group validate.
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <returns>
        /// Validation status true.
        /// </returns>
        private bool CanDoCmdAcceptSendTrackingGroup(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Send group command.
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        private void DoCmdAcceptSendTrackingGroup(object parameter)
        {
            try
            {
            //    if (this.SelectedRejectionCode != null && this.CommentsAsign != string.Empty)
            //    {
            //        int idprocess = Convert.ToInt32(this.SelectedProcess.Tag.ToString());
            //        int idbatch = this.SelectedSummary.idBatch;
            //        int iduser = 1;

            //        int idproductionkey = this.SelectedDetail.idProductionKey;
            //        int idrejectioncode = Convert.ToInt32(this.SelectedRejectionCode.Tag.ToString().Trim());
            //        string comments = this.CommentsAsign;

            //        if (this.dataAccess.UpdKeyForRejectionCode(idproductionkey, idrejectioncode, iduser, comments))
            //        {
            //            var b = this.dataAccess.GetDetailKeyHistory(idbatch, idprocess);
            //            this.DetailForProductionReport = new BindingList<Detail>(b.Select(z => new Detail(z)).ToList());
            //        }
            //    }
            //    else
            //    {
            //        this.ShowWarningAsignAccept = true;
            //    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Close send group validate.
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <returns>
        /// Validation status true.
        /// </returns>
        private bool CanDoCmdCloseSendTrackingGroup(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Close send group command.
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        private void DoCmdCloseSendTrackingGroup(object parameter)
        {
            try
            {
                this.ShowWarningAsignAccept = false;
                this.IsOpenSendTrackingGroup = false;
                this.dispatcherTimer.Start();
            }
            catch 
            {
            }
        }

        /// <summary>
        /// Selected rejection code on edit method.
        /// </summary>
        private void DoSelectedRejectionCode()
        {
        }

        /// <summary>
        /// The accept user change.
        /// </summary>
        /// <param name="parameter">
        /// The parameter value.
        /// </param>
        /// <returns>
        /// The result value.
        /// </returns>
        private bool CanDoCmdAcceptUserChange(object parameter)
        {
            return true;
        }

        /// <summary>
        /// The accept user change.
        /// </summary>
        /// <param name="parameter">
        /// The parameter value.
        /// </param>
        private void DoCmdAcceptUserChange(object parameter)
        {
            try
            {
                if (this.NewUser != string.Empty)
                {
                    if (this.NewUser == this.NewUserConfirm)
                    {
                        if (this.dataAccess.UpdLocalUser(1, this.NewUser))
                        {
                            MessageBox.Show("El usuario fue cambiado con éxito!", "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.CurrentUser = this.NewUser;
                        }
                    }
                    else
                    {
                        MessageBox.Show("El usuario indicado no coincide con la confirmación!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else 
                {
                    MessageBox.Show("El usuario debe ser válido!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// The close user change.
        /// </summary>
        /// <param name="parameter">
        /// The parameter value.
        /// </param>
        /// <returns>
        /// The result value.
        /// </returns>
        private bool CanDoCmdCloseUserChange(object parameter)
        {
            return true;
        }

        /// <summary>
        /// The user change.
        /// </summary>
        /// <param name="parameter">
        /// The parameter value.
        /// </param>
        private void DoCmdCloseUserChange(object parameter)
        {
            try
            {
                this.IsOpenUserChange = false;
            }
            catch 
            {
            }
        }

        /// <summary>
        /// The set new user.
        /// </summary>
        /// <param name="parameter">
        /// The parameter value.
        /// </param>
        /// <returns>
        /// The result value.
        /// </returns>
        private bool CanDoCmdSetNewUser(object parameter)
        {
            return true;
        }

        /// <summary>
        /// The set new user.
        /// </summary>
        /// <param name="parameter">
        /// The parameter value.
        /// </param>
        private void DoCmdSetNewUser(object parameter)
        {
        }

        /// <summary>
        /// The set new user.
        /// </summary>
        /// <param name="parameter">
        /// The parameter value.
        /// </param>
        /// <returns>
        /// The result value.
        /// </returns>
        private bool CanDoCmdSetNewUserConfirm(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Set new user.
        /// </summary>
        /// <param name="parameter">
        /// The parameter value.
        /// </param>
        private void DoCmdSetNewUserConfirm(object parameter)
        {
        }

      
        #endregion

        #endregion 

        /// <summary>
        /// On selection changed in grid method
        /// </summary>
        public void OnSelectionChangedTrackingToBalance()
        {
            try
            {
                // Selected pieces.
                this.BalancePieces = this.selectedTrackingsOnBalance.Count;
                this.OnPropertyChanged("BalancePieces");
            }
            catch (Exception ex)
            {
                Tenaris.Library.Log.Trace.Message("Error on key view:" + ex.StackTrace);
            }
        }

        internal System.Data.DataTable GetDataTableFromItemList(List<Traceability> list)
        {
            System.Data.DataTable dtItems = new System.Data.DataTable();
            dtItems.Columns.Add("idTracking", typeof(Int32));

            foreach (var lt in list)
            {
                // Add to table.
                dtItems.Rows.Add(lt.IdTracking);
            }
            return dtItems;
        }

        private  Transaction SendGroupReport(string machine, out string xmlString)
        {
            bundleReportSelected = new BundleReportModel();
            Transaction transaction = new Transaction(false, string.Empty, "Level2", "N2:Internal Error On Build", string.Empty);
            ProductionReport.ViewModel.ITServices.TServiceClient serviceObject = new ProductionReport.ViewModel.ITServices.TServiceClient();
            string xmlToBuilder = string.Empty;
            try
            {
                string user = this.CurrentUser;
                string edoElab = this.SelectedSummary.ElaborationState;
                int idgroup = SelectedDetail.idGroup;

                bundleReportSelected.TransactionNumber = this.SelectedDetail.TransactionID;
                bundleReportSelected.idBundleReport = this.SelectedDetail.idGroup;
                bundleReportSelected.OrderNumber = this.SelectedDetail.OrderNumber;
                bundleReportSelected.HeatNumber = this.SelectedDetail.HeatNumber;
                bundleReportSelected.FactoryLot = string.Empty;
                bundleReportSelected.Location = this.SelectedDetail.Location;
                //JAA 20190909: Se envia EE 
                //bundleReportSelected.ManufacturingStatusIn = string.Empty;
                bundleReportSelected.ManufacturingStatusIn = edoElab;
                //Se agrega EE de salida
                bundleReportSelected.ManufacturingStatusOut = SelectedElaborationState.Code;

                // Verifico si biene de SEA o TT31.
                if (edoElab != "33")
                {
                    bundleReportSelected.HandlingType = this.SelectedSummary.ReportType;
                }
                else
                {
                    bundleReportSelected.HandlingType = "G";
                }

                if (this.IsForceGranel)
                {
                    bundleReportSelected.HandlingType = "G";
                }
                
                bundleReportSelected.ReportType = this.SelectedDetail.Type;
                bundleReportSelected.Comments = this.SelectedDetail.Comments;
                bundleReportSelected.GenerateDetail = false;
                bundleReportSelected.RenumberPipes = false;
                bundleReportSelected.Printer = this.SelectedDetail.Printer;
                bundleReportSelected.NextMachine = string.Empty;

                // Obtengo tubos de atado.
                List<Traceability> pipes = this.dataAccess.GetTrackingsByGroup(idgroup);

                // Si son salidas en SEAS  provenientes de TT31 o SEA..
                if (machine == "BA301SE02" || machine == "BS301SE03")
                {
                    Trace.Message("Machine {0}", machine);
                    string GroupNumber = string.Empty;
                    string SourceComments = string.Empty;

                    // Respaldo comentarios originales.
                    SourceComments = bundleReportSelected.Comments;

                    // Se asocia detalle de ES301, SE303, SE304
                    bundleReportSelected.Details = SetPiecesDetailTotal(machine, idgroup, edoElab, user, "T1", pipes);
                    bundleReportSelected.TransactionNumber = serviceObject.GetNewAscii85();
                    //bundleReportSelected.Comments = "OK SEA. " + SourceComments;
                    bundleReportSelected.Comments = SourceComments;

                    //Envio.
                    transaction = SendBundleReportTotal(SelectedGroupElaborationState, SelectedLocationToSend != null ? SelectedLocationToSend.Tag.ToString() : "", SelectedRejectionToSend != null ? SelectedRejectionToSend.Tag.ToString() : "", out xmlToBuilder);

                    if ((transaction.TagNumber != "-1") && (transaction.TagNumber != "-2"))
                    {
                        GroupNumber = transaction.TagNumber; //Reporte OK con ruta SEA, entro desde TT31. 
                    }
                    else
                    {
                        if (transaction.TagNumber == "-1")
                        {
                            transaction = null;

                            // Se asocia detalle de SE303, SE304
                            bundleReportSelected.Details = SetPiecesDetailTotal(machine, idgroup, edoElab, user, "T2", pipes);
                            bundleReportSelected.TransactionNumber = serviceObject.GetNewAscii85();
                            //bundleReportSelected.Comments = "OK SEA. " + SourceComments;
                            bundleReportSelected.Comments = SourceComments;

                            //Envio.
                            transaction = SendBundleReportTotal(SelectedGroupElaborationState, SelectedLocationToSend != null ? SelectedLocationToSend.Tag.ToString() : "", SelectedRejectionToSend != null ? SelectedRejectionToSend.Tag.ToString() : "", out xmlToBuilder);

                            if ((transaction.TagNumber != "-1") && (transaction.TagNumber != "-2"))
                            {
                                GroupNumber = transaction.TagNumber; //Reporte OK con ruta SEA, entro desde SE33.
                            }
                            else
                            {
                                if (transaction.TagNumber == "-1")
                                {
                                    transaction = null;

                                    // Se asocia detalle de ES301
                                    bundleReportSelected.Details = SetPiecesDetailTotal(machine, idgroup, edoElab, user, "T3", pipes);
                                    bundleReportSelected.TransactionNumber = serviceObject.GetNewAscii85();
                                    //bundleReportSelected.Comments = "OK SEA, NO RUTA. " + SourceComments;
                                    bundleReportSelected.Comments = SourceComments;

                                    //Envio
                                    transaction = SendBundleReportTotal(SelectedGroupElaborationState, SelectedLocationToSend != null ? SelectedLocationToSend.Tag.ToString() : "", SelectedRejectionToSend != null ? SelectedRejectionToSend.Tag.ToString() : "", out xmlToBuilder);

                                    if ((transaction.TagNumber != "-1") && (transaction.TagNumber != "-2"))
                                    {
                                        GroupNumber = transaction.TagNumber; //Reporte OK SEA, NO RUTA en SEA.
                                    }
                                }
                            }

                        }
                    }
                }
                else
                {
                    Trace.Message("Machine {0}", machine);
                    //Si son salidas TT31
                    bundleReportSelected.Details = SetPiecesDetailAux(machine, idgroup, edoElab, user, "E1", pipes);
                    transaction = SendBundleReportTotal(SelectedGroupElaborationState, SelectedLocationToSend != null ? SelectedLocationToSend.Tag.ToString() : "", SelectedRejectionToSend != null ? SelectedRejectionToSend.Tag.ToString() : "", out xmlToBuilder);

                    if (transaction.TagNumber == "0")
                    {
                        int attempts = 0;
                        transaction.TagNumber = "-1";

                        while ((transaction.TagNumber == "-1" || transaction.TagNumber == "-2") && attempts < 5)
                        {
                            bundleReportSelected.ReportType = "B";
                            //bundleReportSelected.Comments = "Desbalanceo de linea";
                            bundleReportSelected.Comments = "";
                            bundleReportSelected.Details = SetPiecesDetailAux(machine, idgroup, edoElab, user, "E2", pipes);
                            bundleReportSelected.Location = "CSAL2";

                            // nueva transacción
                            string s2 = serviceObject.GetNewAscii85();
                            bundleReportSelected.TransactionNumber = s2;

                            // En caso de que regrese 0, entonces la ruta es hacias seas y se tiene que hacer una bajada.
                            transaction = SendBundleReportTotal(SelectedGroupElaborationState, SelectedLocationToSend != null ? SelectedLocationToSend.Tag.ToString() : "", SelectedRejectionToSend != null ? SelectedRejectionToSend.Tag.ToString() : "", out xmlToBuilder);

                            attempts++;
                        }
                    }
                }

            }
            catch(Exception ex)
            {
                Trace.Exception(ex);
                throw;
                xmlToBuilder = string.Empty;
            }

            xmlString = xmlToBuilder;

            return transaction;
        }

        private static  List<BundleReportDetailModel> SetPiecesDetailTotal(string machine, int idgroup, string edoElabProduct, string user, string option, List<Traceability> pipes)
        {
            List<BundleReportDetailModel> lPieces = new List<BundleReportDetailModel>();

            try
            {
                MachineProductionModel m = new MachineProductionModel();
                MachineProductionModel n = new MachineProductionModel();
                MachineProductionModel o = new MachineProductionModel();
                int c = 1;

                foreach(var p in pipes)
                {
                    var detail = new BundleReportDetailModel();
                    detail.idBundleReport = idgroup;
                    detail.idBundleReportDetail = c;

                    // Asocio datos de rastreabilidad, tomando en cuanta si entro desde sea, ya que indica que entro granel o si entro desde TT donde ahi
                    // si se crea detalle dependiendo del tipo de rastreabilidad.
                    if (edoElabProduct != "33")
                    {
                        detail.PipeNumber = p.Number;
                        detail.FactoryId = p.TraceabilityMask;
                    }
                    else
                    {
                        detail.PipeNumber = c;
                        detail.FactoryId = string.Empty;
                    }

                    detail.ReprocessNumber = 0;

                    /*Si prod es301, sea303 y sea304, entrando por TT31*/
                    if (option == "T1")
                    {
                        o.idBundleReportDetail = c;
                        o.idMachineProduction = 1;
                        o.Machine = "ES301";
                        o.ProccesedSide = string.Empty;
                        o.ProductionType = "BUE";
                        o.RejectionCause = string.Empty;
                        o.UserId = user;
                        o.Date = DateTime.Now;

                        m.idBundleReportDetail = c;
                        m.idMachineProduction = 2;
                        m.Machine = "SE303";
                        m.ProccesedSide = string.Empty;
                        m.ProductionType = "BUE";
                        m.RejectionCause = string.Empty;
                        m.UserId = user;
                        m.Date = DateTime.Now;

                        n.idBundleReportDetail = c;
                        n.idMachineProduction = 3;
                        n.Machine = "SE304";
                        n.ProccesedSide = string.Empty;
                        n.ProductionType = "BUE";
                        n.RejectionCause = string.Empty;
                        n.UserId = user;
                        n.Date = DateTime.Now;

                        detail.Machines = new List<MachineProductionModel>();
                        detail.Machines.Add(o);
                        detail.Machines.Add(m);
                        detail.Machines.Add(n);
                    }

                    /*Si prod sea303 y sea304, entrando por SE33*/
                    if (option == "T2")
                    {
                        m.idBundleReportDetail = c;
                        m.idMachineProduction = 1;
                        m.Machine = "SE303";
                        m.ProccesedSide = string.Empty;
                        m.ProductionType = "BUE";
                        m.RejectionCause = string.Empty;
                        m.UserId = user;
                        m.Date = DateTime.Now;

                        n.idBundleReportDetail = c;
                        n.idMachineProduction = 2;
                        n.Machine = "SE304";
                        n.ProccesedSide = string.Empty;
                        n.ProductionType = "BUE";
                        n.RejectionCause = string.Empty;
                        n.UserId = user;
                        n.Date = DateTime.Now;

                        detail.Machines = new List<MachineProductionModel>();
                        detail.Machines.Add(m);
                        detail.Machines.Add(n);
                    }

                    /*Si prod ES301, entrando por TT31 y genera etiqueta.*/
                    if (option == "T3")
                    {
                        m.idBundleReportDetail = c;
                        m.idMachineProduction = 1;
                        m.Machine = "ES301";
                        m.ProccesedSide = string.Empty;
                        m.ProductionType = "BUE";
                        m.RejectionCause = string.Empty;
                        m.UserId = user;
                        m.Date = DateTime.Now;

                        detail.Machines = new List<MachineProductionModel>();
                        detail.Machines.Add(m);
                    }

                    lPieces.Add(detail);

                    c++;
                }

            }
            catch(Exception ex)
            {
                Trace.Exception(ex);
                throw;
            }

            return lPieces;
        }

        private static List<BundleReportDetailModel> SetPiecesDetailAux(string machine, int idgroup, string edoElabProduct, string user, string option, List<Traceability> pipes)
        {
            List<BundleReportDetailModel> lPieces = new List<BundleReportDetailModel>();

            try
            {
                MachineProductionModel m = new MachineProductionModel();
                int c = 1;

                foreach (var p in pipes)
                {
                    var detail = new BundleReportDetailModel();
                    detail.idBundleReport = idgroup;
                    detail.idBundleReportDetail = c;

                    // Asocio datos de rastreabilidad, tomando en cuanta si entro desde sea, ya que indica que entro granel o si entro desde TT donde ahi
                    // si se crea detalle dependiendo del tipo de rastreabilidad.
                    if (edoElabProduct != "33")
                    {
                        detail.PipeNumber = p.Number;
                        detail.FactoryId = p.TraceabilityMask;
                    }
                    else
                    {
                        detail.PipeNumber = c;
                        detail.FactoryId = string.Empty;
                    }

                    detail.ReprocessNumber = 0;

                    /*Si prod es301, sea303 y sea304, entrando por TT31*/
                    if (option == "E1")
                    {
                        m.idBundleReportDetail = c;
                        m.idMachineProduction = 1;
                        m.Machine = "ES301";
                        m.ProccesedSide = string.Empty;
                        m.ProductionType = "BUE";
                        m.RejectionCause = string.Empty;
                        m.UserId = user;
                        m.Date = DateTime.Now;

                        detail.Machines = new List<MachineProductionModel>();
                        detail.Machines.Add(m);
                    }

                    /*Si prod sea303 y sea304, entrando por SE33*/
                    if (option == "E2")
                    {
                        m.idBundleReportDetail = c;
                        m.idMachineProduction = 1;
                        m.Machine = "SE303";
                        m.ProccesedSide = string.Empty;
                        m.ProductionType ="BAJ";
                        m.RejectionCause = string.Empty;
                        m.UserId = user;
                        m.Date = DateTime.Now;

                        detail.Machines = new List<MachineProductionModel>();
                        detail.Machines.Add(m);
                    }

                    lPieces.Add(detail);

                    c++;
                }

            }
            catch(Exception ex)
            {
                Trace.Exception(ex);
                throw;
            }

            return lPieces;
        }

        private static Transaction SendBundleReportTotal(GroupElaborationState groupElaborationState, string location, string rejectionCode, out string xmlString)
        {

            Dictionary<string, ITManagement.Common.ITServices.ErrorReport> res = null;
            ITManagement.Common.ITServices.TServiceClient client = null;
            Dictionary<string, ITManagement.Common.ITServices.TransactionReport> trans = null;

            Transaction transaction = new Transaction(false, string.Empty, "Level2", "N2:Internal Error On Send", string.Empty);

            string xmlToBuild = String.Empty;
            try
            {
                client = new ITManagement.Common.ITServices.TServiceClient();
                ITManagement.Common.ITServices.Bundle atado = null;

                
                atado = FillBundlesProducedFromBD(bundleReportSelected.OrderNumber.ToString(), bundleReportSelected.HeatNumber.ToString(), groupElaborationState, location, rejectionCode, out xmlToBuild);
                Trace.Message("Formed XML Bundle Sent to Service: {0}", xmlToBuild);
                trans = client.SendProducedItemsGroup(out res, bundleReportSelected.TransactionNumber.ToString(), new ITManagement.Common.ITServices.ITMachineId() { MachineId = "DUMMY" }, atado);

                //Respuesta de envio
                if (res.Count.Equals(0))
                {
                    transaction = new Transaction(true, trans["Bundle"].Element, "Level2", "OK", string.Empty);
                }
                else
                {

                    ITManagement.Common.ITServices.ErrorReport r = new ITManagement.Common.ITServices.ErrorReport();
                    string message = string.Empty;

                    if (res.TryGetValue("ErroresPIP", out r))
                    {
                        message = res["ErroresPIP"].ErrorCode + " : " + res["ErroresPIP"].Description;
                        transaction = new Transaction(false, "-1", "ERRORPIP", message, string.Empty);
                    }

                    if (res.TryGetValue("ErroresNAT", out r))
                    {
                        message = res["ErroresNAT"].ErrorCode + " : " + res["ErroresNAT"].Description;
                        transaction = new Transaction(false, "-2", "ERRORNAT", message, string.Empty);
                    }

                }
                Trace.Message("Response Service transaction: estatus {0}, tagNumber {1}, sendingString {2}, responseString {3}, ", transaction.Status, transaction.TagNumber, transaction.SendingString, transaction.ResponseString);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("El extremo remoto finalizó la secuencia."))
                {
                    transaction = new Transaction(false, "-2", "Level2TimeOut", "OK :" + ex.Message, string.Empty);
                }
                else
                {
                    transaction = new Transaction(false, "-2", "Level2NotContain", ex.Message, string.Empty);
                }
                Trace.Exception(ex);
                xmlToBuild = string.Empty;
                client.Abort();
            }

            xmlString = xmlToBuild;
            return transaction;
        }

        private static ITManagement.Common.ITServices.Bundle FillBundlesProducedFromBD(string order, string heat, GroupElaborationState groupElaborationState, string location, string rejectionCode, out string xmlString)
        {
            ITManagement.Common.ITServices.Bundle atado = null;
            ITManagement.Common.ITServices.Parameter par;

            BundleLocal bundleLocal = new BundleLocal();
            atado = new ITManagement.Common.ITServices.Bundle();
            atado.ProductKey = new ITManagement.Common.ITServices.ProductKey() { Order = order, Heat = heat };
            bundleLocal.ProductKey = new Model.Types.ProductKey() { Order = atado.ProductKey.Order, Heat = atado.ProductKey.Heat };
            atado.LotID = bundleReportSelected.FactoryLot;    // Lote de fabricacion 
            bundleLocal.LotID = atado.LotID;

            if (groupElaborationState.Code.Equals("BUE"))
            {
                atado.Location = bundleReportSelected.Location;   // Ubicación de salida del material (se utiliza principalmente para Bajadas; puede ir en blanco)
            }else
            {
                atado.Location = location;   // Ubicación de salida del material Seleccionada en el modal
            }


            bundleLocal.Location = atado.Location;
            atado.ElabStatus = new ITManagement.Common.ITServices.ElabStatus() { Status = bundleReportSelected.ManufacturingStatusIn, SubStatus = bundleReportSelected.ManufacturingStatusOut }; // Estados de elaboración (origen/destino) del material (puede ir en blanco)
            bundleLocal.ElabStatus = new Model.Types.ElabStatus() { Status = atado.ElabStatus.Status, SubStatus = atado.ElabStatus.SubStatus };
            atado.Parameters = new ITManagement.Common.ITServices.Parameters() { List = new Dictionary<string, ITManagement.Common.ITServices.Parameter>() };
            par = new ITManagement.Common.ITServices.Parameter() { Name = "HandlingType", Value = bundleReportSelected.HandlingType }; // Es el tipo de material a manejar. Valores posibles: G - Granel, D - Detalle
            atado.Parameters.List.Add(par.Name, par);
            par = new ITManagement.Common.ITServices.Parameter() { Name = "ReportType", Value = bundleReportSelected.ReportType }; // Tipo de Reporte Valores posibles: P - Producción, N - Descartes No Recuperables, etc.
            atado.Parameters.List.Add(par.Name, par);
            par = new ITManagement.Common.ITServices.Parameter() { Name = "Comments", Value = bundleReportSelected.Comments };
            atado.Parameters.List.Add(par.Name, par);
            par = new ITManagement.Common.ITServices.Parameter() { Name = "GenerateDetail", Value = bundleReportSelected.GenerateDetail ? "S" : "N" }; // Se vuelve detalle?  "S" o "N"
            atado.Parameters.List.Add(par.Name, par);
            par = new ITManagement.Common.ITServices.Parameter() { Name = "ReNumberPipes", Value = bundleReportSelected.RenumberPipes ? "S" : "N" }; // Para el paso por tratamiento térmico para renumerar. Valores posibles: S - Sí, N - No
            atado.Parameters.List.Add(par.Name, par);
            par = new ITManagement.Common.ITServices.Parameter() { Name = "Printer", Value = bundleReportSelected.Printer }; // Impresora seleccionada, Valor vacio no imprime etiqueta
            atado.Parameters.List.Add(par.Name, par);
            bundleLocal.Parameters = new Model.Types.Parameters()
            {
                HandlingType = atado.Parameters.List["HandlingType"].Value.ToString(),
                ReportType = atado.Parameters.List["ReportType"].Value.ToString(),
                Comments = atado.Parameters.List["Comments"].Value.ToString(),
                GenerateDetail = atado.Parameters.List["GenerateDetail"].Value.ToString(),
                ReNumberPipes = atado.Parameters.List["ReNumberPipes"].Value.ToString(),
                Printer = atado.Parameters.List["Printer"].Value.ToString()
            };
            atado.ITMachineId = new ITManagement.Common.ITServices.ITMachineId() { MachineId = bundleReportSelected.NextMachine }; // Siguiente máquina (Para cuando finaliza producción en una línea y se va a otra; puede ir en blanco)
            bundleLocal.Machine = new Machine() { MachineId = atado.ITMachineId.MachineId };

            atado.Items = new ITManagement.Common.ITServices.StockItemsCollection() { List = new Dictionary<string, ITManagement.Common.ITServices.StockItem>() };
            bundleLocal.Items = new Items();
            try
            {
                bundleLocal.Items.Pipes = new List<Model.Types.Pipe>();
                foreach (var detail in bundleReportSelected.Details)
                {
                    ITManagement.Common.ITServices.Pipe pipe = new ITManagement.Common.ITServices.Pipe();
                    Model.Types.Pipe pipeModel = new Model.Types.Pipe();
                    pipe.Name = detail.PipeNumber.ToString();
                    pipeModel.Name = detail.PipeNumber.ToString();
                    pipe.LotID = detail.FactoryId; // Id de fabricacion
                    pipeModel.LotID = detail.FactoryId;
                    pipe.ManufacturigNumber = detail.ReprocessNumber.ToString(); // ReProcessNumber
                    pipeModel.ManufacturingNumber = detail.ReprocessNumber.ToString();
                    pipe.ProcessRuns = new ITManagement.Common.ITServices.ProcessRuns() { List = new Dictionary<string, ITManagement.Common.ITServices.ProcessRun>() };
                    List<Model.Types.ProcessRuns> processRuns = new List<Model.Types.ProcessRuns>();

                    // Procesamiento en Maquina
                    foreach (var machine in detail.Machines)
                    {
                        ITManagement.Common.ITServices.ProcessRun process = new ITManagement.Common.ITServices.ProcessRun();
                        Model.Types.Process processModel = new Model.Types.Process();
                        process.Name = machine.Machine; // nombre de la maquina
                        processModel.Name = machine.Machine;
                        process.TimeStamp = machine.Date; // Fecha de produccion
                        processModel.TimeStamp = machine.Date;
                        process.Parameters = new ITManagement.Common.ITServices.Parameters() { List = new Dictionary<string, ITManagement.Common.ITServices.Parameter>() };
                        List<Model.Types.ProcessParameters> processParameters = new List<Model.Types.ProcessParameters>();
                        par = new ITManagement.Common.ITServices.Parameter() { Name = "ProccesedSide", Value = machine.ProccesedSide }; // Extremo procesado (Principalmente para CASING y TUBING; puede ir en blanco)
                        process.Parameters.List.Add(par.Name, par);
                        processParameters.Add(new ProcessParameters() { Name = "ProccesedSide", Value = machine.ProccesedSide });
                        par = new ITManagement.Common.ITServices.Parameter() { Name = "ProductionType", Value = groupElaborationState.Code }; // Tipo de producción (del tubo en determinada máquina. Valores posibles: BUE - Buena, NAP - No aplica, etc.)                    
                        process.Parameters.List.Add(par.Name, par);
                        processParameters.Add(new ProcessParameters() { Name = "ProductionType", Value = groupElaborationState.Code });
                        par = new ITManagement.Common.ITServices.Parameter() { Name = "RejectionCause", Value = rejectionCode }; // Motivo de descarte (relevante si el tipo de producción fue un descarte. Valor depende la línea reportada)
                        process.Parameters.List.Add(par.Name, par);
                        processParameters.Add(new ProcessParameters() { Name = "RejectionCause", Value = rejectionCode });
                        par = new ITManagement.Common.ITServices.Parameter() { Name = "UserId", Value = machine.UserId }; // Matricula Operador
                        process.Parameters.List.Add(par.Name, par);
                        processParameters.Add(new ProcessParameters() { Name = "UserId", Value = machine.UserId });

                        processModel.ProcessParameters = processParameters;
                        pipe.ProcessRuns.List.Add(process.Name, process);
                        processRuns.Add(new Model.Types.ProcessRuns() { Name = process.Name, Process = processModel });
                    }
                    pipeModel.ProcessRuns = processRuns;
                    // Añadir el tubo a StockItemsCollection
                    atado.Items.List.Add(pipe.Name, pipe);
                    bundleLocal.Items.Pipes.Add(pipeModel);
                }
            }
            catch
            {
                throw;
            }

            atado.PiecesCount = atado.Items.List.Count;
            bundleLocal.PiecesCount = atado.Items.List.Count;

            string xml = "";
            XmlSerializer serializer = new XmlSerializer(typeof(BundleLocal));
            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    serializer.Serialize(writer, bundleLocal);
                    xml = sww.ToString(); // Your XML
                }
            }

            xmlString = xml;

            return atado;
        }

    }
}