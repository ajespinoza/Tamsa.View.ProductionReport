// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Field.cs" company="Tenaris">
//   Tenaris.
// </copyright>
// <summary>
//   Defines the Field.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tamsa.View.ProductionReport.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// The fields class.
    /// </summary>
    public static class Field
    {
        #region Enums

        /// <summary>
        /// The attributes data.
        /// </summary>
        public enum Attributes
        {
            /// <summary>
            /// The attribute.
            /// </summary>
            Attribute,

            /// <summary>
            /// The object value.
            /// </summary>
            Value
        }

        /// <summary>
        /// Product data.
        /// </summary>
        public enum Product
        {
            /// <summary>
            /// The id batch on product.
            /// </summary>
            idBatch,

            /// <summary>
            /// The machine on product.
            /// </summary>
            Machine,

            /// <summary>
            /// The Diameter.
            /// </summary>
            Diameter,

            /// <summary>
            /// The product maximum length.
            /// </summary>
            MaximumLength,

            /// <summary>
            /// The product KgMWeight.
            /// </summary>
            KgMWeight,

            /// <summary>
            /// The programmed pieces.
            /// </summary>
            ProgrammedPieces,

            /// <summary>
            /// The loaded pieces.
            /// </summary>
            LoadedPieces,

            /// <summary>
            /// The produced pieces.
            /// </summary>
            ProducedPieces,

            /// <summary>
            /// The available pieces.
            /// </summary>
            AvailablePieces,

            /// <summary>
            /// The status.
            /// </summary>
            Status,

            /// <summary>
            /// The IT status.
            /// </summary>
            ITStatus
        }

        /// <summary>
        /// Location data.
        /// </summary>
        public enum Location
        {
            /// <summary>
            /// id location
            /// </summary>
            idLocation,

            /// <summary>
            /// The location code
            /// </summary>
            Code
        }

        /// <summary>
        /// Elaboration state data.
        /// </summary>
        public enum Elaboration
        {
            /// <summary>
            /// id elaboration
            /// </summary>
            idElaboration,

            /// <summary>
            /// The elaboration state code
            /// </summary>
            Code,

            /// <summary>
            /// The elaboration state name
            /// </summary>
            Name
        }

        /// <summary>
        /// Item status data.
        /// </summary>
        public enum ItemStatus
        {
            /// <summary>
            /// id item status
            /// </summary>
            idItemStatus,

            /// <summary>
            /// the item status code
            /// </summary>
            Code,

            /// <summary>
            /// The item status name
            /// </summary>
            Name
        }

        /// <summary>
        /// Rejection code data.
        /// </summary>
        public enum RejectionCode
        {
            /// <summary>
            /// id rejection code
            /// </summary>
            idRejectionCode,

            /// <summary>
            /// The rejection code 
            /// </summary>
            Code,

            /// <summary>
            /// The rejection code description
            /// </summary>
            Description
        }

        public enum Group
        {
            /// <summary>
            ///  The id group.
            /// </summary>
            idGroup,

            /// <summary>
            /// The id user.
            /// </summary>
            idUser,

            /// <summary>
            /// The transaction status.
            /// </summary>
            Status,

            /// <summary>
            /// The sending string.
            /// </summary>
            SendingString,

            /// <summary>
            /// The response string.
            /// </summary>
            ResponseString
        }

        #endregion
    }
}

