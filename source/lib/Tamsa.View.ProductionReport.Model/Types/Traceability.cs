// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Traceability.cs" company="Tamsa">
//   Tenaris.
// </copyright>
// <summary>
//   Traceability class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Tamsa.View.ProductionReport.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// The traceability class.
    /// </summary>
    public class Traceability
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the Traceability class.
        /// </summary>
        /// <param name="idtracking">
        /// The id tracking object.
        /// </param>
        /// <param name="ordernumber">
        /// The order number object.
        /// </param>
        /// <param name="heatnumber">
        /// The heat number object.
        /// </param>
        /// <param name="number">
        /// The pipe number object.
        /// </param>
        public Traceability(int idtracking, int ordernumber, int heatnumber, int progressive, int number, string stencil, string status, string bsk)
        {
            this.IdTracking = idtracking;
            this.OrderNumber = ordernumber;
            this.HeatNumber = heatnumber;
            this.Progressive = progressive;
            this.Number = number;
            this.TraceabilityMask = stencil;
            this.Status = status;
            this.Bsk = bsk;
        }

        /// <summary>
        /// Initializes a new instance of the Traceability class.
        /// </summary>
        /// <param name="detail">
        /// The traceability object.
        /// </param>
        public Traceability(Traceability detail)
        {
            this.IdTracking = detail.IdTracking;
            this.OrderNumber = detail.OrderNumber;
            this.HeatNumber = detail.HeatNumber;
            this.Progressive = detail.Progressive;
            this.Number = detail.Number;
            this.TraceabilityMask = detail.TraceabilityMask;
            this.Status = detail.Status;
            this.Bsk = detail.Bsk;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the id tracking.
        /// </summary>
        public int IdTracking { get; private set; }

        /// <summary>
        /// Gets the order number.
        /// </summary>
        public int OrderNumber { get; private set; }

        /// <summary>
        /// Gets the heat number.
        /// </summary>
        public int HeatNumber { get; private set; }

        /// <summary>
        /// Gets the heattreatment number.
        /// </summary>
        public int Number { get; private set; }

        /// <summary>
        /// Gets the progressive.
        /// </summary>
        public int Progressive { get; private set; }

        /// <summary>
        /// Gets or sets the traceability mask.
        /// </summary>
        public string TraceabilityMask { get; set; }

        /// <summary>
        /// Gets or sets status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets basket.
        /// </summary>
        public string Bsk { get; set; }


        #endregion
    }
}
