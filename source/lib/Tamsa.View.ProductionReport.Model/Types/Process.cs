

namespace Tamsa.View.ProductionReport.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Process
    {
        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idprocess"></param>
        /// <param name="code"></param>
        public Process(int idprocess, string code, string name)
        {
            this.idProcess = idprocess;
            this.Code = code;
            this.Name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="process"></param>
        public Process(Process process)
        {
            this.idProcess= process.idProcess;
            this.Code = process.Code;
            this.Name = process.Name;
        }

        public Process()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the idprocess.
        /// </summary>
        public int idProcess { get; private set; }

        /// <summary>
        /// Gets the code
        /// </summary>
        public string Code{ get; private set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; private set; }

      

        #endregion

    }
}
