// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataAccess.cs" company="Tenaris">
//   Tenaris.
// </copyright>
// <summary>
//   Defines the DataAccess.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tamsa.View.ProductionReport.Model
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Configuration;
    using System.Data;
    using System.Linq;
    using System.Text;
    using Tenaris.Library.DbClient;
    using Tenaris.Manager.ExitApplication.Common;
    using Tenaris.Manager.ExitApplication.Support;
    using System.Data.SqlClient;
    using Tamsa.View.ProductionReport.Model.Types;
    using System.Windows.Controls;

    /// <summary>
    /// The Data access class.
    /// </summary>
    public class DataAccess
    {
        #region Fields

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DataAccess"/> class.
        /// Contructor
        /// </summary>
        public DataAccess()
        {
            DbClient = new DbClient(ConfigurationManager.ConnectionStrings["dbLevel2"].Name.ToString(), false, true);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets DbClient.
        /// </summary>
        internal static DbClient DbClient { get; set; }

        #endregion

        #region Base methods

        /// <summary>
        /// Activate necessary procedures.
        /// </summary>
        public void Activate()
        {
            try
            {
                DbClient.Activate();
                DbClient.AddCommand(ClientConfiguration.Settings.StoredProcedureGetProcessToReport.Trim());
                DbClient.AddCommand(ClientConfiguration.Settings.StoredProcedureGetKeysHistory.Trim());
                DbClient.AddCommand(ClientConfiguration.Settings.StoredProcedureGetDetailKeyHistory.Trim());
                DbClient.AddCommand(ClientConfiguration.Settings.StoredProcedureInsKey.Trim());
                DbClient.AddCommand(ClientConfiguration.Settings.StoredProcedureDelGroup.Trim());
                DbClient.AddCommand(ClientConfiguration.Settings.StoredProcedureUpdGroup.Trim());
                DbClient.AddCommand(ClientConfiguration.Settings.StoredProcedureGetLocalUser.Trim());
                DbClient.AddCommand(ClientConfiguration.Settings.StoredProcedureUpdLocalUser.Trim());
                DbClient.AddCommand(ClientConfiguration.Settings.StoredProcedureGetShiftInformation.Trim());
                DbClient.AddCommand(ClientConfiguration.Settings.StoredProcedureGetTrackingToBalance.Trim());
                DbClient.AddCommand(ClientConfiguration.Settings.StoredProcedureGetTrackings.Trim());
                DbClient.AddCommand(ClientConfiguration.Settings.StoredProcedureGetGroupElaborationState.Trim());
                DbClient.AddCommand(ClientConfiguration.Settings.StoredProcedureGetElaborationStateByGroup.Trim());
                DbClient.AddCommand(ClientConfiguration.Settings.StoredProcedureGetRejectionCodeByElaborationState.Trim());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Deactivate current elements in the class.
        /// </summary>
        public void DeActivate()
        {
            if (DbClient.Activated)
            {
                DbClient.Deactivate();
            }
        }

        /// <summary>
        /// Begin transaction method.
        /// </summary>
        public void BeginTransaction()
        {
            DbClient.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        /// <summary>
        /// Begin transaction method.
        /// </summary>
        /// <param name="isolationLevel">
        /// The isolation level.
        /// </param>
        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            DbClient.BeginTransaction(isolationLevel);
        }

        /// <summary>
        /// Commit transaction method.
        /// </summary>
        public void Commit()
        {
            DbClient.Commit();
        }

        /// <summary>
        /// Rollback transaction method.
        /// </summary>
        public void Rollback()
        {
            DbClient.Rollback();
        }

        #endregion

        #region Methods

        public List<Process> GetMachines()
        {
            var objList = new List<Process>();

            try
            {
                using (var reader = DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureGetProcessToReport.Trim()).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var objProcess = new Process(
                        (int)reader["idMachine"],
                        (string)reader["Code"],
                        (string)reader["Description"]);

                        objList.Add(objProcess);
                    }
                }

                return objList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Summary> GetProductsToReport(int idmachine)
        {
            try
            {
                var objSummary = new List<Summary>();
                DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureGetKeysHistory.Trim()).Parameters[String.Format("@{0}", "idMachine")].Value = idmachine;
                using (var reader = DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureGetKeysHistory.Trim()).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var summary = new Summary(
                            (int)reader["idBatch"],
                            (int)reader["OrderNumber"],
                            (int)reader["HeatNumber"],
                            (string)reader["ElaborationState"],
                            (string)reader["ReportType"],
                            (int)reader["LoadedCount"],
                            (int)reader["EnteredCount"],
                            (int)reader["ProducedCount"],
                            (int)reader["ReportedCount"],
                            (int)reader["PendingCount"],
                            (int)reader["Status"]);

                        objSummary.Add(summary);
                    }
                }

                return objSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Traceability> GetAvailableTrackings(int idbatch, int idmachine, int isfullmode)
        {
            try
            {
                var objTracking = new List<Traceability>();
                DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureGetTrackingToBalance.Trim()).Parameters[String.Format("@{0}", "idBatch")].Value = idbatch;
                DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureGetTrackingToBalance.Trim()).Parameters[String.Format("@{0}", "idMachine")].Value = idmachine;
                DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureGetTrackingToBalance.Trim()).Parameters[String.Format("@{0}", "isFullMode")].Value = isfullmode;

                using (var reader = DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureGetTrackingToBalance.Trim()).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tracking = new Traceability(
                        (int)reader["idTracking"],
                        (int)reader["OrderNumber"],
                        (int)reader["HeatNumber"],
                        (int)reader["ProgressiveNumber"],
                        (int)reader["PipeNumber"],
                        (string)reader["TraceabilityMask"],
                        (string)reader["Status"],
                        (string)reader["Bsk"]);
                        objTracking.Add(tracking);
                    }
                }

                return objTracking;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsGroup(int idbatch,int idmachine, string user, string type, string location, string printer, string comments, string transactionid, DataTable pipes)
        {
            try
            {
                var par = DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureInsKey.Trim()).Parameters[String.Format("@{0}", "Pipes")];
                DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureInsKey.Trim()).Parameters.Remove(par);
                DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureInsKey.Trim()).Parameters.Add(new SqlParameter("@Pipes", pipes) { SqlDbType = SqlDbType.Structured });

                DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureInsKey.Trim()).Parameters[String.Format("@{0}", "idBatch")].Value = idbatch;
                DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureInsKey.Trim()).Parameters[String.Format("@{0}", "idMachine")].Value = idmachine;
                DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureInsKey.Trim()).Parameters[String.Format("@{0}", "User")].Value = user;
                DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureInsKey.Trim()).Parameters[String.Format("@{0}", "Type")].Value = type;
                DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureInsKey.Trim()).Parameters[String.Format("@{0}", "Location")].Value = location;
                DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureInsKey.Trim()).Parameters[String.Format("@{0}", "Printer")].Value = printer;
                DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureInsKey.Trim()).Parameters[String.Format("@{0}", "Comments")].Value = comments;
                DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureInsKey.Trim()).Parameters[String.Format("@{0}", "TransactionID")].Value = transactionid;
                DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureInsKey.Trim()).Parameters[String.Format("@{0}", "Pipes")].Value = pipes;
                
                DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureInsKey.Trim()).ExecuteNonQuery();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Detail> GetGroupsForProduct(int idmachine, int idbatch)
        {
            try
            {
                var objDetail = new List<Detail>();
                DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureGetDetailKeyHistory.Trim()).Parameters[String.Format("@{0}", "idMachine")].Value = idmachine;
                DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureGetDetailKeyHistory.Trim()).Parameters[String.Format("@{0}", "idBatch")].Value = idbatch;
                using (var reader = DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureGetDetailKeyHistory.Trim()).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var detail = new Detail(
                            (int)reader["idGroup"],
                            (string)reader["DateIns"],
                            (string)reader["Machine"],
                            (string)reader["Type"],
                            (string)reader["User"],
                            (int)reader["OrderNumber"],
                            (int)reader["HeatNumber"],
                            (string)reader["GroupNumber"],
                            (int)reader["Pieces"],
                            (string)reader["Printer"],
                             (string)reader["Location"],
                            (string)reader["Status"],
                            (string)reader["Comments"],
                            (string)reader["ResponseStringIT"],
                            (string)reader["TransactionID"]);

                        objDetail.Add(detail);
                    }
                }

                return objDetail;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Traceability> GetTrackingsByGroup(int idgroup)
        {
            try
            {
                var objTracking = new List<Traceability>();
                DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureGetTrackings.Trim()).Parameters[String.Format("@{0}", "idGroup")].Value = idgroup;

                using (var reader = DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureGetTrackings.Trim()).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tracking = new Traceability(
                        (int)reader["idTracking"],
                        0,
                        0,
                        (int)reader["ProgressiveNumber"],
                        (int)reader["PipeNumber"],
                        (string)reader["TraceabilityMask"],
                        (string)reader["Status"],
                        (string)reader["Bsk"]);
                        objTracking.Add(tracking);
                    }
                }

                return objTracking;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DelGroup(int idgroup)
        {
            try
            {
                DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureDelGroup.Trim()).Parameters[String.Format("@{0}", "idGroup")].Value = idgroup;
                DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureDelGroup.Trim()).ExecuteNonQuery();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdGroup(int idgroup, int idstatus, string groupnumber,string response, string xmlStringIT)
        {
            try
            {
                DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureUpdGroup.Trim()).Parameters[String.Format("@{0}", "idGroup")].Value = idgroup;
                DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureUpdGroup.Trim()).Parameters[String.Format("@{0}", "idStatus")].Value = idstatus;
                DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureUpdGroup.Trim()).Parameters[String.Format("@{0}", "GroupNumber")].Value = groupnumber;
                DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureUpdGroup.Trim()).Parameters[String.Format("@{0}", "ResponseStringIT")].Value = response;
                DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureUpdGroup.Trim()).Parameters[String.Format("@{0}", "XmlStringIT")].Value = xmlStringIT;

                DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureUpdGroup.Trim()).ExecuteNonQuery();

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Get current user.
        /// </summary>
        /// <param name="option">
        /// The option value.
        /// </param>
        /// <returns>
        /// The user value.
        /// </returns>
        public User GetCurrentUser(int option)
        {
            try
            {
                var objUser = new User();
                DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureGetLocalUser.Trim()).Parameters[String.Format("@{0}", "option")].Value = option;
                using (var reader = DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureGetLocalUser.Trim()).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user = new User(
                            (int)reader["idUser"],
                            (string)reader["CurrentCode"],
                            (string)reader["DefaultCode"],
                            (string)reader["LastUpdate"]);

                        objUser = new User(user);
                    }
                }

                return objUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update local user.
        /// </summary>
        /// <param name="option">
        /// The option.
        /// </param>
        /// <param name="user">
        /// The user value.
        /// </param>
        /// <returns>
        /// The result status.
        /// </returns>
        public bool UpdLocalUser(int option, string user)
        {
            try
            {
                DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureUpdLocalUser.Trim()).Parameters[String.Format("@{0}", "option")].Value = option;
                DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureUpdLocalUser.Trim()).Parameters[String.Format("@{0}", "CurrentUser")].Value = user;

                DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureUpdLocalUser.Trim()).ExecuteNonQuery();

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets shift information.
        /// </summary>
        /// <param name="date1">
        /// The date value.
        /// </param>
        /// <returns>
        /// The shift object.
        /// </returns>
        public Shift GetShift(string date1)
        {
            try
            {
                var objDate = new Shift();
                DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureGetShiftInformation.Trim()).Parameters[String.Format("@{0}", "date")].Value = date1;
                using (var reader = DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureGetShiftInformation.Trim()).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var date = new Shift(
                            (int)reader["Number"],
                            (string)reader["Date"],
                            (string)reader["Initial"],
                            (string)reader["Final"]);

                        objDate = new Shift(date);
                    }
                }

                return objDate;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<GroupElaborationState> GetGroupElaborationState()
        {
            var objList = new ObservableCollection<GroupElaborationState>();

            try
            {
                using (var reader = DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureGetGroupElaborationState.Trim()).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var group = new GroupElaborationState(
                            (int)reader["idGroupElaborationState"],
                            (string)reader["Code"],
                            (string)reader["Name"],
                            (string)reader["Description"]);

                        objList.Add(group);
                    }
                }

                return objList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<ElaborationState> GetElaborationStateByGroup(int IdGroupElaborationState)
        {
            var objList = new ObservableCollection<ElaborationState>();

            try
            {
                DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureGetElaborationStateByGroup.Trim()).Parameters[String.Format("@{0}", "IdGroupElaborationState")].Value = IdGroupElaborationState;
                using (var reader = DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureGetElaborationStateByGroup.Trim()).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var elaborationState = new ElaborationState(
                            (int)reader["IdElaborationState"],
                            (string)reader["Code"],
                            (string)reader["Name"],
                            (string)reader["Description"]);

                        objList.Add(elaborationState);
                    }
                }

                return objList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<ComboBoxItem> GetRejections(int idElaborationState)
        {
            var objList = new ObservableCollection<ComboBoxItem>();

            try
            {
                DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureGetRejectionCodeByElaborationState.Trim()).Parameters[String.Format("@{0}", "IdElaborationState")].Value = idElaborationState;
                using (var reader = DbClient.GetCommand(ClientConfiguration.Settings.StoredProcedureGetRejectionCodeByElaborationState.Trim()).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var rejection = new ComboBoxItem();
                        rejection.Content = (string)reader["Name"];
                        rejection.Tag = (string)reader["Code"];

                        objList.Add(rejection);
                    }
                }

                return objList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
