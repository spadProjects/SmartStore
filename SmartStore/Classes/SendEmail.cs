using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using SmartStore.DataAccess.Context;
using SmartStore.Model.Entities;

namespace SmartStore.Classes
{
    public class SendEmail
    {
        public static void Send(string To,string Subject,string Body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("emailservicestests@gmail.com", "Spad.co");
            mail.To.Add(To);
            mail.Subject = Subject;
            mail.Body = Body;
            mail.IsBodyHtml = true;

            //System.Net.Mail.Attachment attachment;
            // attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
            // mail.Attachments.Add(attachment);

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("emailservicestests@gmail.com", "@Amin@7731@");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

        }
    }
}