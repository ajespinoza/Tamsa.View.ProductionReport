using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamsa.View.ProductionReport.Model.Types
{
    public class Rejection
    {
        #region Constructor

        public Rejection()
        { }

        public Rejection(int IdRejection, string Code)
        {
            this.IdRejection = IdRejection;
            this.Code = Code;
        }

        #endregion

        #region Properties

        public int IdRejection { get; private set; }

        public string Code { get; private set; }

        #endregion
    }
}
