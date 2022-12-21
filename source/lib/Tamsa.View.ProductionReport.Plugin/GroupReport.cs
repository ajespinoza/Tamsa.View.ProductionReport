
namespace Tenaris.View.ExitApplication.Plugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Tenaris.Manager.ExitApplication.Common;
    using Tenaris.Manager.ExitApplication.Support;
    using Tenaris.View.ExitApplication.Model;

    /// <summary>
    /// 
    /// </summary>
    public class GroupReport
    {
        public Transaction SendGroupReport(Group group)
        {
            //Business logic
                int tagNumber = 88888;
                string status = "TRANS OK";
                string responseString = "The transaction is correct.";
                var report = new Transaction(tagNumber, status, responseString);
            //

            // Return transaction result.
            return report;
        }
    }
}
