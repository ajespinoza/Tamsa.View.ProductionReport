// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RejectionCode.cs" company="Tenaris">
//   Tenaris.
// </copyright>
// <summary>
//   Defines the RejectionCode.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tamsa.View.ProductionReport.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;

    /// <summary>
    /// Reejction code class.
    /// </summary>
    public class RejectionCode : DependencyObject
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="RejectionCode"/> class.
        /// </summary>
        public RejectionCode()
        { 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RejectionCode"/> class.
        /// </summary>
        /// <param name="idrejectioncode">
        /// The id rejection code.
        /// </param>
        /// <param name="code">
        /// the code value.
        /// </param>
        /// <param name="description">
        /// The description value.
        /// </param>
        public RejectionCode(int idrejectioncode, string code, string description)
        {
            this.IdRejectionCode = idrejectioncode;
            this.Code = code;
            this.Description = description;
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets id rejection code.
        /// </summary>
        public int IdRejectionCode { get; private set; }

        /// <summary>
        /// Gets the code.
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// Gets description.
        /// </summary>
        public string Description { get; private set; }

        #endregion
    }
}
