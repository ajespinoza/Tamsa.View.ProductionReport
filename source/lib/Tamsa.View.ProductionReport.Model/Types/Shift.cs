namespace Tamsa.View.ProductionReport.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Shift
    {
        #region Constructor

        public Shift()
        { }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="number"></param>
       /// <param name="date"></param>
       /// <param name="initial"></param>
       /// <param name="final"></param>
        public Shift(int number, string date, string initial, string final)
        {
            this.Number = number;
            this.Date = date;
            this.Initial = initial;
            this.Final = final;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        public Shift(Shift dt)
        {
            this.Number = dt.Number;
            this.Date = dt.Date;
            this.Initial = dt.Initial;
            this.Final = dt.Final;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public int Number { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public string Date { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public string Initial{ get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Final { get; private set; }


        #endregion
    }
}
