// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Location.cs" company="Tenaris">
//   Tenaris.
// </copyright>
// <summary>
//   Defines the Location.
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
    /// Location class.
    /// </summary>
    public class Location : DependencyObject
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> class.
        /// </summary>
        public Location()
        { 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> class.
        /// </summary>
        /// <param name="idlocation">
        /// The location id.
        /// </param>
        /// <param name="code">
        /// The location code.
        /// </param>
        public Location(int idlocation, string code)
        {
            this.IdLocation = idlocation;
            this.Code = code;
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets location id.
        /// </summary>
        public int IdLocation { get; private set; }

        /// <summary>
        /// Gets location code.
        /// </summary>
        public string Code { get; private set; }

        #endregion
    }
}
