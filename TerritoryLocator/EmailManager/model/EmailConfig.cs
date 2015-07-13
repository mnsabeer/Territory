using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Configuration;
using System.Web;

namespace TerritoryLocator.EmailManager.model
{
    public class EmailConfig
    {
        #region PROPERTIES
        /// <summary>
        /// The default From mail address shown to the mail recipients.
        /// </summary>
        public string DefaultFromAddress { get; set; }

        /// <summary>
        /// The default friendly name of the sender shown to the mail recipients.
        /// </summary>
        public string DefaultSenderDisplayName { get; set; }

        /// <summary>
        /// Gets or sets the incoming email server host.
        /// </summary>
        public string IncomingServer { get; set; }

        /// <summary>
        /// Gets or sets the incoming email port number.
        /// </summary>
        public int IncomingPort { get; set; }

        /// <summary>
        /// Gets or sets the incoming email credential username component.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the incoming email credential password component.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets if the incoming server uses SSL.
        /// </summary>
        public bool IsIncomingSsl { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether [enable welcome email service].
        /// </summary>
        /// <value>
        /// <c>true</c> if [enable welcome email service]; otherwise, <c>false</c>.
        /// </value>
        public bool EnableWelcomeEmailService { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [enable appointment email].
        /// </summary>
        /// <value>
        /// <c>true</c> if [enable appointment email]; otherwise, <c>false</c>.
        /// </value>
        public bool EnableAppointmentEmail { get; set; }

        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Initializes an instance of EmailConfig object.
        /// </summary>
        public EmailConfig()
        {
            this.DefaultFromAddress = GetConfigValue("DefaultFromAddress");
            this.DefaultSenderDisplayName = GetConfigValue("DefaultSenderDisplayName");

            this.IncomingServer = GetConfigValue("IncomingEmailServer");
            this.IncomingPort = int.Parse(GetConfigValue("IncomingEmailPort"));
            this.IsIncomingSsl = bool.Parse(GetConfigValue("IncomingSsl"));


            var smtpSection = ConfigurationManager.GetSection("mailSettings/smtp_Settings_Welcome") as SmtpSection;
            if (smtpSection != null)
            {
                this.UserName = smtpSection.Network.UserName;
                this.Password = smtpSection.Network.Password;
            }
        }
        #endregion

        #region PRIVATE SUPPORT METHOD
        /// <summary>
        /// Gets the configuration value from the application configuration file.
        /// </summary>
        /// <param name="key">The key to identify the application setting to return.</param>
        /// <returns>The value of the application setting identified by key, or empty string if there is no such setting.</returns>
        private string GetConfigValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
        #endregion
    }
}