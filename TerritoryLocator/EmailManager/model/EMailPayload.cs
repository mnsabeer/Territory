using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace TerritoryLocator.EmailManager.model
{
    public class EMailPayload
    {
        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets or sets the To address(es) of an email.
        /// </summary>
        public List<EmailAddress> To { get; set; }

        /// <summary>
        /// Gets or sets the CC address(es) of an email.
        /// </summary>
        public List<EmailAddress> Cc { get; set; }

        /// <summary>
        /// Gets or sets the BCC address(es) of an email.
        /// </summary>
        public List<EmailAddress> Bcc { get; set; }

        /// <summary>
        /// Gets or sets the sender From mail address.
        /// </summary>
        public EmailAddress From { get; set; }

        /// <summary>
        /// Gets or sets the email subject.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the mail body of an email.
        /// </summary>
        public string MessageBody { get; set; }

        /// <summary>
        /// Gets or sets if an email is html format.
        /// </summary>
        public bool IsHtml { get; set; }

        /// <summary>
        /// Gets or sets the paths to the attachment files. Invalid file paths will not be attached.
        /// </summary>
        public List<string> Attachments { get; set; }

        /// <summary>
        /// Gets or sets the alternate views of an email.
        /// </summary>
        public List<AlternateView> AlternateViews { get; set; }

        /// <summary>
        /// To attach objects of type Attachment
        /// </summary>
        public List<Attachment> AttachmentObjects { get; set; }
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Initializes an instance of MailPayload.
        /// </summary>
        public EMailPayload()
        {
            To = new List<EmailAddress>();
            Cc = new List<EmailAddress>();
            Bcc = new List<EmailAddress>();
            Attachments = new List<string>();
            AlternateViews = new List<AlternateView>();
            AttachmentObjects = new List<Attachment>();
        }
        #endregion

        #region OPERATOR OVERLOADS
        /// <summary>
        /// Implicitly casts an EmailPayload instance to a System.Net.Mail.MailMessage object to send mail using SmtpClient object.
        /// </summary>
        /// <param name="payload">The EmailPayload object to cast to System.Net.Mail.MailMessage type.</param>
        /// <returns>The MailMessage object that is built using the operand.</returns>
        public static implicit operator MailMessage(EMailPayload payload)
        {
            try
            {
                if (payload == null || payload.To.Count == 0)
                    return null;

                var message = new MailMessage();

                IEnumerable<MailAddress> toAddresses = ParseMailAddresses(payload.To);
                foreach (var address in toAddresses)
                    message.To.Add(address);

                if (payload.Cc.Count > 0)
                    foreach (var address in ParseMailAddresses(payload.Cc))
                        message.CC.Add(address);

                if (payload.Bcc.Count > 0)
                    foreach (var address in ParseMailAddresses(payload.Bcc))
                        message.Bcc.Add(address);

                message.IsBodyHtml = payload.IsHtml;
                message.Body = payload.MessageBody;
                message.Subject = payload.Subject;

                if (payload.Attachments != null && payload.Attachments.Count > 0)
                {
                    foreach (string path in payload.Attachments)
                    {
                        if (File.Exists(path))
                            message.Attachments.Add(new Attachment(path));
                    }
                }

                //To add attachments from attachment objects
                if (payload.AttachmentObjects.Count > 0)
                {
                    foreach (var item in payload.AttachmentObjects)
                    {
                        message.Attachments.Add(item);
                    }
                }

                return message;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        /// <summary>
        /// Remove extra line breaks
        /// </summary>
        /// <param name="data">html string</param>
        /// <returns>cleared string</returns>
        public string RemoveNewlineChars(string data)
        {
            return data.Replace("\r", "").Replace("\n", "").Trim();
        }

        /// <summary>
        /// Reads the text document.
        /// </summary>
        /// <param name="strDocumentName">Name of the STR document.</param>
        /// <returns></returns>
        /// <exception cref="System.ApplicationException">Error loading App Document.</exception>
        public string ReadTextDocument(string strDocumentName)
        {
            TextReader TextFileReader = null;
            string strFileContent;
            try
            {
                TextFileReader = File.OpenText(strDocumentName);
                strFileContent = TextFileReader.ReadToEnd();
            }
            catch (Exception Exp)
            {
                throw new ApplicationException("Error loading App Document.", Exp);
            }
            finally
            {
                if (TextFileReader != null)
                    TextFileReader.Close();
            }
            return strFileContent;
        }

        #endregion

        #region PRIVATE SUPPORT
        /// <summary>
        /// Safely build a list of the System.Net.Mail.MailAddress objects from a list of EmailAddress objects.
        /// </summary>
        /// <param name="addresses">The list of EmailAddress objects to process.</param>
        /// <returns>The list of parsed System.Net.Mail.MailAddress objects.</returns>
        private static IEnumerable<MailAddress> ParseMailAddresses(List<EmailAddress> addresses)
        {
            if (addresses == null || addresses.Count == 0)
                return null;

            var mailAddresses = new List<MailAddress>();
            addresses.ForEach(addr =>
            {
                if (string.IsNullOrEmpty(addr.EMailAddress))
                    return;

                if (string.IsNullOrEmpty(addr.DisplayName))
                    mailAddresses.Add(new MailAddress(addr.EMailAddress));
                else
                    mailAddresses.Add(new MailAddress(addr.EMailAddress, addr.DisplayName));
            });

            return mailAddresses.AsEnumerable();
        }


        /// <summary>
        /// Creates a safe List of EmailAddress. Only to safely manage the null lists.
        /// </summary>
        /// <param name="list">The input list to process.</param>
        /// <returns>The List itself if the list is not null, an empty List if input is null.</returns>
        private static List<EmailAddress> GetSafeList(List<EmailAddress> list)
        {
            if (list != null)
                return list;
            else
                return new List<EmailAddress>();
        }
        #endregion
    }
}