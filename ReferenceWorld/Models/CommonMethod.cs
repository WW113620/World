using ReferenceWorld.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace ReferenceWorld.Models
{
    public class CommonMethod
    {
        public static bool IsLogin()
        {
            if (!string.IsNullOrWhiteSpace(LoginHelper.UserGuid) && !string.IsNullOrWhiteSpace(LoginHelper.UserNameEmail))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region 发送邮件
        public static string SendMail(string from, string fromname, string to, List<string> ccList, string subject, string body, string username, string password, string server = "smtp.163.com", string fujian = "")
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from, fromname);
                mail.To.Add(to);
                if (ccList != null && ccList.Count > 0)
                {
                    foreach (var item in ccList)
                        mail.CC.Add(new MailAddress(item));
                }
                mail.Subject = subject;
                mail.BodyEncoding = Encoding.Default;
                mail.Priority = MailPriority.High;
                mail.Body = body; 
                mail.IsBodyHtml = true;
                if (fujian.Length > 0)
                {
                    mail.Attachments.Add(new Attachment(fujian));
                }
                SmtpClient smtp = new SmtpClient(server, 25);
                smtp.UseDefaultCredentials = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new System.Net.NetworkCredential(username, password);
                smtp.Timeout = 10000;
                smtp.Send(mail);
                return "ok";
            }
            catch (Exception exp)
            {
                return exp.Message;
            }
        }
        #endregion

    }
    enum EnumUserType
    {
        Person = 0,
        Company = 1
    }
}