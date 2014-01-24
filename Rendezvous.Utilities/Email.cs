using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rendezvous.Utilities
{
    public class Email
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }

        public string MessageFooter
        {
            get
            {
                return "<i>The information contained in this electronic mail message is confidential. It is intended solely for the use of the individual or entity to whom it is addressed and others authorised to receive it. If the reader of this message is not the intended recipient, you are hereby notified that any use, copying, dissemination or disclosure of this information is strictly prohibited.<br /><br/>Acute Sales Ltd, Registered in England and Wales Number 2529960, VAT Number GB536921139, Registered office Acute sales Ltd, 4 Century Road, High Carr Business Park, Newcastle-under-Lyme, Staffordshire, ST5 7UG</i>";
            }
        }

        /// <summary>
        /// Send Email
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool SendEmail(string name, string email, string phone, string text, int type, string subject = null)
        {
            try
            {
                Name = name;
                EmailAddress = email;
                Phone = phone;
                Message = text;
                Subject = subject;

                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                message.To.Add("sales@acutesales.co.uk");
                //for testing via smtp4dev
                message.To.Add("rob@rnwood.co.uk");

                switch (type)
                {
                    case 1:
                        message.Subject = "Acutest.net Website Enquiry";
                        break;
                    case 2:
                        message.Subject = "Confirmation";
                        break;
                    case 3:
                        message.Subject = "Additional";
                        break;
                    case 4:
                        message.Subject = "Book on Workshop" + (string.IsNullOrEmpty(subject) != true ? subject : "");
                        break;
                    case 5:
                        message.Subject = "Error" + (string.IsNullOrEmpty(subject) != true ? subject : "");
                        break;
                }

                message.IsBodyHtml = true;
                message.Body = GenerateMessageBody(type);

                System.Net.Configuration.SmtpSection smtpConfig = new System.Net.Configuration.SmtpSection();
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(smtpConfig.Network.Host);
                smtp.Send(message);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private string GenerateMessageBody(int type)
        {
            if (type == 1)
            {
                return BuildEnquiryEmail();
            }else if(type == 4){
                return BuildWorkshopEmail();
            }
            else if (type == 5)
            {
                return BuildErrorEmail();
            }

            return "";
        }

        private string BuildEnquiryEmail()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("From: " + Name + "<br />");
            sb.Append("Email: " + EmailAddress + "<br />");
            sb.Append("Phone: " + Phone + "<br />");
            sb.Append("Message: " + Message + "<br />");

            sb.Append("<br/><br/><br/>" + MessageFooter);
            return sb.ToString();
        }

        private string BuildWorkshopEmail()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Please book me on " + Subject.Replace(" - ", "") + "<br />");

            sb.Append("From: " + Name + "<br />");
            sb.Append("Email: " + EmailAddress + "<br />");
            sb.Append("Phone: " + Phone + "<br />");
            sb.Append("Message: " + Message + "<br />");

            sb.Append("<br/><br/><br/>" + MessageFooter);
            return sb.ToString();
        }

        private string BuildErrorEmail()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Error Message: " + Message + "<br />");

            sb.Append("<br/><br/><br/>" + MessageFooter);
            return sb.ToString();
        }
    }
}
