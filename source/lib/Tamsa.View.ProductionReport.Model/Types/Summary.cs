// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Summary.cs" company="Tenaris">
//   Tenaris.
// </copyright>
// <summary>
//   Defines the summmary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tamsa.View.ProductionReport.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Summary
    {
        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idbatch"></param>
        /// <param name="ordernumber"></param>
        /// <param name="heatnumber"></param>
        /// <param name="elabstate"></param>
        /// <param name="firstitemnumber"></param>
        /// <param name="loadedcount"></param>
        /// <param name="enteredcount"></param>
        /// <param name="producedcount"></param>
        /// <param name="reportedcount"></param>
        /// <param name="status"></param>
        public Summary(int idbatch, int ordernumber, int heatnumber, string elabstate,string reportType,int loadedcount, int enteredcount, int producedcount,int reportedcount, int pendingCount,int status)
        {
            this.idBatch = idbatch;
            this.OrderNumber = ordernumber;
            this.HeatNumber = heatnumber;
            this.ElaborationState = elabstate;
            this.ReportType = reportType;
            this.LoadedCount = loadedcount;
            this.EnteredCount = enteredcount;
            this.ProducedCount = producedcount;
            this.ReportedCount = reportedcount;
            this.PendingCount = pendingCount;
            this.Status = status;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="summary"></param>
        public Summary(Summary summary)
        {
            this.idBatch = summary.idBatch;
            this.OrderNumber = summary.OrderNumber;
            this.HeatNumber = summary.HeatNumber;
            this.ElaborationState = summary.ElaborationState;
            this.ReportType = summary.ReportType;
            this.LoadedCount = summary.LoadedCount;
            this.EnteredCount = summary.EnteredCount;
            this.ProducedCount = summary.ProducedCount;
            this.ReportedCount = summary.ReportedCount;
            this.PendingCount = summary.PendingCount;
            this.Status = summary.Status;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the idbatch.
        /// </summary>
        public int idBatch { get; private set; }

        /// <summary>
        /// Gets the order number.
        /// </summary>
        public int OrderNumber { get; private set; }

        /// <summary>
        /// Gets the heat number.
        /// </summary>
        public int HeatNumber { get; private set; }

        /// <summary>
        /// Gets the elab state.
        /// </summary>
        public string ElaborationState{ get; private set; }

        /// <summary>
        /// Gets the first item number.
        /// </summary>
        public string ReportType { get; private set; }

        /// <summary>
        /// Gets the loaded count.
        /// </summary>
        public int LoadedCount { get; private set; }

        /// <summary>
        /// Gets the entered count.
        /// </summary>
        public int EnteredCount { get; private set; }

        /// <summary>
        /// Gets the produced count.
        /// </summary>
        public int ProducedCount { get; private set; }

        /// <summary>
        /// Gets the report count.
        /// </summary>
        public int ReportedCount { get; private set; }

        /// <summary>
        /// Gets the pending count.
        /// </summary>
        public int PendingCount { get; private set; }

        /// <summary>
        /// Gets the  status.
        /// </summary>
        public int Status { get; private set; }

        #endregion
    }
}
