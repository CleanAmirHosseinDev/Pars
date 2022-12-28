using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Email {
    public static class EmailSend {
        public static Boolean SendMail(string fromMail, string fromName, string subject, string body, string toMail) {
            try {

                using(var mailMsg = new MailMessage()) {
                    mailMsg.BodyEncoding = Encoding.UTF8;
                    mailMsg.HeadersEncoding = Encoding.UTF8;
                    mailMsg.SubjectEncoding = Encoding.UTF8;
                    mailMsg.Priority = MailPriority.High;
                    mailMsg.Subject = subject;
                    mailMsg.Body = "<a style=\"background: #3e82ff;padding: 10px;color: white;font-size: 16px;text-decoration: none;font-family: 'system-ui';\" href=\"mailto:" + fromMail + "\">" +
                        "<b style=\"display:inline-block; direction: rtl; \">" +
                        "<span>برای پاسخ به</span>" +
                        "<span> (" + fromName + ") </span>" +
                        "<span>کلیک کنید</span>" +
                        "</b></a><br><br>" + body;
                    mailMsg.IsBodyHtml = true;
                    mailMsg.From = new MailAddress("parscrc@outlook.com", "parscrc_mailsender", Encoding.UTF8);
                    //mailMsg.Sender = new MailAddress(fromMail, fromName, Encoding.UTF8);                    

                    //if (attachments != null) foreach (var item in attachments) mailMsg.Attachments.Add(item);

                    //if (toMails != null) foreach (var mail in toMails) mailMsg.To.Add(new MailAddress(mail));
                    mailMsg.To.Add(new MailAddress(toMail));

                    SmtpClient smtpServer = new SmtpClient("smtp-mail.outlook.com") {
                        EnableSsl = true,
                        Port = 587,
                        Credentials = new NetworkCredential("parscrc@outlook.com", "1qaz@WSX10155")
                        // smtpClient.UseDefaultCredentials = true; // uncomment if you don't want to use the network credentials
                    };
                    //smtpServer.Send("mohammadjv7@gmail.com", "info@vam30.com", "subssss", "textttt");
                    smtpServer.Send(mailMsg);
                    return true;

                }

            } catch(Exception ex) {
                return false;
            }
        }
    }
}
