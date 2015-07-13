using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TerritoryLocator.EmailManager
{
    public class EmailAddress
    {

        #region PRIVATE FIELDS
        /// <summary>
        /// Field to hold the user name value.
        /// </summary>
        private string displayName;
        #endregion

        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        public string EMailAddress { get; set; }

        /// <summary>
        /// Gets or sets the friendly display name.
        /// </summary>
        public string DisplayName
        {
            get
            {
                if (string.IsNullOrEmpty(displayName))
                    return this.EMailAddress;
                else
                    return displayName;
            }
            set
            {
                displayName = value;
            }
        }
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Initializes an instance of EmailAddress object.
        /// </summary>
        /// <param name="emailAddress"></param>
        public EmailAddress(string emailAddress)
        {
            this.EMailAddress = emailAddress;
        }

        /// <summary>
        /// Initializes an instance of EmailAddress object.
        /// </summary>
        /// <param name="emailAddress">The email address part.</param>
        /// <param name="displayName">The friendly display name part.</param>
        public EmailAddress(string emailAddress, string firstName, string LastName)
        {
            this.DisplayName = firstName + "" + LastName;
            this.EMailAddress = emailAddress;
        }
        #endregion

        #region OVERRIDES
        /// <summary>
        /// Override from System.Object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (string.IsNullOrEmpty(this.DisplayName))
                return this.EMailAddress;
            else
                return this.DisplayName + "<" + this.EMailAddress + ">";
        }
        #endregion
    }
}