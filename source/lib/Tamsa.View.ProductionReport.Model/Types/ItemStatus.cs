// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ItemStatus.cs" company="Tenaris">
//   Tenaris.
// </copyright>
// <summary>
//   Defines the ItemStatus.
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
    /// Item status class.
    /// </summary>
    public class ItemStatus : DependencyObject
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemStatus"/> class.
        /// </summary>
        public ItemStatus()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemStatus"/> class.
        /// </summary>
        /// <param name="iditemstatus">
        /// The item status id.
        /// </param>
        /// <param name="code">
        /// The item status code.
        /// </param>
        /// <param name="name">
        /// The item status name.
        /// </param>
        public ItemStatus(int iditemstatus, string code, string name)
        {
            this.IdItemStatus = iditemstatus;
            this.Code = code;
            this.Name = name;
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets item status id.
        /// </summary>
        public int IdItemStatus { get; private set; }

        /// <summary>
        /// Gets item status code.
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// Gets item status name.
        /// </summary>
        public string Name { get; private set; }

        #endregion
    }
}
