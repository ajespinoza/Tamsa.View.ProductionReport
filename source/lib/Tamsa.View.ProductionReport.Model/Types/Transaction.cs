// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Transaction.cs" company="Tenaris">
//   Tenaris.
// </copyright>
// <summary>
//   Defines the transaction to report group.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tamsa.View.ProductionReport.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// The transaction class.
    /// </summary>
    public class Transaction
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Transaction"/> class.
        /// </summary>
        /// <param name="status">
        /// The status.
        /// </param>
        /// <param name="tagNumber">
        /// The tag number in report.
        /// </param>
        /// <param name="sendingString">
        /// The sending string.
        /// </param>
        /// <param name="responseString">
        /// The response string.
        /// </param>
        public Transaction(bool status, string tagNumber, string sendingString, string responseString, string xmlStringIT)
        {
            this.TagNumber = tagNumber;
            this.Status = status;
            this.SendingString = sendingString;
            this.ResponseString = responseString;
            this.XmlStringIT = xmlStringIT; 
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether the status.
        /// </summary>
        public bool Status { get; private set; }

        /// <summary>
        /// Gets the tag number.
        /// </summary>
        public string TagNumber { get; set; }

        /// <summary>
        /// Gets the sending string.
        /// </summary>
        public string SendingString { get; private set; }

        /// <summary>
        /// Gets response string.
        /// </summary>
        public string ResponseString { get; private set; }

        /// <summary>
        /// Gets xml string.
        /// </summary>
        public string XmlStringIT { get; private set; }

        #endregion
    }
}
