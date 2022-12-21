// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Detail.cs" company="Tenaris">
//   Tenaris.
// </copyright>
// <summary>
//   Defines the detail.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tamsa.View.ProductionReport.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Detail
    {
        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idproductionkey"></param>
        /// <param name="idprocess"></param>
        /// <param name="insdatetime"></param>
        /// <param name="initial"></param>
        /// <param name="final"></param>
        /// <param name="reportpieces"></param>
        /// <param name="status"></param>
        /// <param name="itresponse"></param>
        /// <param name="ismanual"></param>
        public Detail(int idgroup,string dateIns, string machine, string type, string user, int ordernumber, int heatnumber, string groupnumber, int pieces, string printer,string location,string status, string comments, string response, string transactionID)
        {
            this.idGroup = idgroup;
            this.InsDateTime = dateIns;
            this.Machine = machine;
            this.Type = type;
            this.User = user;
            this.OrderNumber = ordernumber;
            this.HeatNumber = heatnumber;
            this.GroupNumber = groupnumber;
            this.Pieces = pieces;
            this.Printer = printer;
            this.Location = location;
            this.Status = status;
            this.Comments = comments;
            this.ResponseStringIT = response;
            this.TransactionID = transactionID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="detail"></param>
        public Detail(Detail detail)
        {
            this.idGroup = detail.idGroup;
            this.InsDateTime = detail.InsDateTime;
            this.Machine = detail.Machine;
            this.Type= detail.Type;
            this.User = detail.User;
            this.OrderNumber = detail.OrderNumber;
            this.HeatNumber = detail.HeatNumber;
            this.GroupNumber = detail.GroupNumber;
            this.Pieces = detail.Pieces;
            this.Printer = detail.Printer;
            this.Location = detail.Location;
            this.Status = detail.Status;
            this.Comments = detail.Comments;
            this.ResponseStringIT = detail.ResponseStringIT;
            this.TransactionID = detail.TransactionID;
        }

        #endregion

        #region Properties

        public int idGroup { get; private set; }

        public string InsDateTime { get; private set; }

        public string Machine{ get; private set; }

        public string Type { get; private set; }

        public string User { get; private set; }

        public int OrderNumber { get; private set; }

        public int HeatNumber { get; private set; }

        public string GroupNumber { get; private set; }

        public int Pieces { get; private set; }

        public string Printer { get; private set; }

        public string Location { get; private set; }

        public string Status { get; private set; }

        public string Comments { get; private set; }

        public string ResponseStringIT { get; private set; }

        public string TransactionID { get; private set; }

        #endregion
    }
}
