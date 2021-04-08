using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Quant.Spice.Test.UI.Common
{
    public class Email
    {
        public void SendEmail(string mailSubject, string mailBody, string[] toAddresses)
        {
            MailMessage mailMessage = new MailMessage();
            try
            {
                foreach (string toAddress in toAddresses)
                {
                    mailMessage.To.Add(toAddress);
                }
                mailMessage.Subject = mailSubject;
                mailMessage.Body = mailBody;

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Send(mailMessage);
            }
            catch (Exception)
            {

            }
            
        }
    }
}
