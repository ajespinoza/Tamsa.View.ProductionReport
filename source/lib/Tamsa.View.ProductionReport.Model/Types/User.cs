namespace Tamsa.View.ProductionReport.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class User
    {

        #region Constructor

        public User()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iduser"></param>
        /// <param name="currentUser"></param>
        /// <param name="defaultUser"></param>
        /// <param name="lastDateUpdate"></param>
        public User(int iduser, string currentUser, string defaultUser, string lastDateUpdate)
        {
            this.idUser = iduser;
            this.CurrentUser = currentUser;
            this.DefaultUser = defaultUser;
            this.LastDateUpdate = lastDateUpdate;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        public User(User user)
        {
            this.idUser = user.idUser;
            this.CurrentUser = user.CurrentUser;
            this.DefaultUser = user.DefaultUser;
            this.LastDateUpdate = user.LastDateUpdate;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the idprocess.
        /// </summary>
        public int idUser { get; private set; }

        /// <summary>
        /// Gets the current
        /// </summary>
        public string CurrentUser { get; private set; }

        /// <summary>
        /// Gets the default.
        /// </summary>
        public string DefaultUser { get; private set; }

        /// <summary>
        /// Gets the last date update.
        /// </summary>
        public string LastDateUpdate { get; private set; }


        #endregion
    }
}
