// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Elaboration.cs" company="Tenaris">
//   Tenaris.
// </copyright>
// <summary>
//   Defines the Elaboration.
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
    /// Elaboration state class
    /// </summary>
    public class Elaboration : DependencyObject
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Elaboration"/> class.
        /// </summary>
        public Elaboration()
        { 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Elaboration"/> class.
        /// </summary>
        /// <param name="idelaboration">
        /// The id elaborations state.
        /// </param>
        /// <param name="code">
        /// The elaboration state code.
        /// </param>
        /// <param name="name">
        /// The name elaboration state.
        /// </param>
        public Elaboration(int idelaboration, string code, string name)
        {
            this.IdElaboration = idelaboration;
            this.Code = code;
            this.Name = name;
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets id elaboration.
        /// </summary>
        public int IdElaboration { get; private set; }

        /// <summary>
        /// Gets elaboration code.
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// Gets elaboration name.
        /// </summary>
        public string Name { get; private set; }

        #endregion
    }
}
