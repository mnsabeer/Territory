using TerritoryLocator.EmailManager.model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace TerritoryLocator.EmailManager
{
    public class Mailer
    {
        #region PRIVATE FIELDS

        /// <summary>
        /// SMTP client to send message
        /// </summary>
        private SmtpClient _mailClient;

        #endregion

        #region PUBLIC PROPERTIES

        /// <summary>
        /// Gets or sets the email configuration that applies to current Mailer instance. Normally autobuilt from application configuration.
        /// </summary>
        public EmailConfig MailConfiguration
        {
            get;
            set;
        }


        #endregion

        #region Constructor

        /// <summary>
        /// Initializes an instance of Mailer object.
        /// </summary>
        public Mailer()
        {
            this.MailConfiguration = new EmailConfig();
        }

        /// <summary>
        /// Initializes an instance of Mailer object with an EmailConfig parameter.
        /// </summary>
        /// <param name="configuration">The EmailConfig parameter that needs to be used with the new instance.</param>
        public Mailer(EmailConfig configuration)
        {
            this.MailConfiguration = configuration;

        }
        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Sends an email represented by the EmailPayload parameter.
        /// </summary>
        /// <param name="payload">The content representation of the Email like subject, body and recipients.</param>
        /// <returns>True if the send operation was successful, false if not.</returns>
        public bool SendMail(EMailPayload payload)
        {
            var message = (MailMessage)payload;

            var smtpSection = GetSmtpSectionByParameter("mailSettings/smtp_Settings_Welcome");

            _mailClient = new SmtpClient(smtpSection.Network.Host, smtpSection.Network.Port);
            _mailClient.UseDefaultCredentials = false;
            _mailClient.Credentials = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);

            if (payload.From != null)
            {
                message.From = string.IsNullOrEmpty(payload.From.DisplayName) ? new MailAddress(payload.From.EMailAddress) : new MailAddress(payload.From.EMailAddress, payload.From.DisplayName);
            }
            else
            {
                message.From = new MailAddress(this.MailConfiguration.DefaultFromAddress, this.MailConfiguration.DefaultSenderDisplayName);
            }
            try 
            {
                _mailClient.Send(message);
            }
            catch(Exception e)
            {

            }
          

            return true;
        }


    


        /// <summary>
        /// Sends the mail.
        /// </summary>
        /// <param name="payload">The payload.</param>
        /// <param name="smtpSection">The SMTP section.</param>
        /// <returns></returns>
        public bool SendMail(EMailPayload payload, SmtpSection smtpSection)
        {
            var message = (MailMessage)payload;

            _mailClient = new SmtpClient(smtpSection.Network.Host, smtpSection.Network.Port);
            _mailClient.UseDefaultCredentials = false;
            _mailClient.Credentials = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);

            if (payload.From != null)
            {
                message.From = string.IsNullOrEmpty(payload.From.DisplayName) ? new MailAddress(payload.From.EMailAddress) : new MailAddress(payload.From.EMailAddress, payload.From.DisplayName);
            }

            _mailClient.Send(message);

            return true;
        }



        #endregion

        #region PRIVATE SUPPORT

        /// <summary>
        /// Gets the SMTP section by parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        private SmtpSection GetSmtpSectionByParameter(string parameter)
        {
            try
            {
                // get section by parameter
                var smtpSection = (SmtpSection)ConfigurationManager.GetSection(parameter);

                return smtpSection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }


        /// <summary>
        /// Decodes a Base64 encoded string into plain text.
        /// </summary>
        /// <param name="data">The Base64 encoded input string.</param>
        /// <returns>The decoded string from the input.</returns>
        public string DecodeBase64(string data)
        {
            try
            {
                var todecode_byte = Convert.FromBase64String(data);
                var result = Encoding.UTF8.GetString(todecode_byte);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return null;
        }
        #endregion
    }
}