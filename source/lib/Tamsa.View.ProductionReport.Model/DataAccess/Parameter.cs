// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Parameter.cs" company="Tenaris">
//   Tenaris.
// </copyright>
// <summary>
//   Defines the Parameter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tamsa.View.ProductionReport.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// The parameter class.
    /// </summary>
    public static class Parameter
    {
        #region Enums


        public enum Summary
        {
            idBatch,

            idProcess
        }

        /// <summary>
        /// The attributes parameter.
        /// </summary>
        public enum Attributes
        {
            /// <summary>
            /// The id owner item.
            /// </summary>
            idOwnerItem,

            /// <summary>
            /// The id batch value.
            /// </summary>
            idBatch,

            /// <summary>
            /// The id group.
            /// </summary>
            idGroup
        }

        /// <summary>
        /// Products parameters.
        /// </summary>
        public enum Products
        {
            /// <summary>
            /// Shift number.
            /// </summary>
            Shift,

            /// <summary>
            /// machine code to search.
            /// </summary>
            MachineCodes,

            /// <summary>
            /// The id machine.
            /// </summary>
            idMachine
        }

        #endregion
    }
}
