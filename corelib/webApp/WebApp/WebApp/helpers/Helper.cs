using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TarsierEyes;
using WebApp.Models;
using WebApp.helpers;
using System.Net.Mail;
using System.Text;

namespace WebApp.helpers
{
    public class Helper : IDisposable
    {
        dbContext db;

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public string EncryptString(string stringData = null)
        {
            string encrypted = Cryptography.Encryption.Encrypt(stringData, Constant.HASHKEY);
            return encrypted;
        }

        public string DecryptString(string stringData = null, string hKey = null)
        {
            string pattern = (!string.IsNullOrEmpty(hKey) ? hKey : Constant.HASHKEY);
            string decrypted = Cryptography.Decryption.Decrypt(stringData, pattern);
            return decrypted;
        }

        public user_models UserData(string session = null)
        {
            user_models m = new user_models();

            if (!string.IsNullOrEmpty(session))
                m = JsonConvert.DeserializeObject<user_models>(session);

            return m;
        }

        public Boolean IsNumber(String value)
        {
            return value.All(Char.IsDigit);
        }

        public string LogException(Exception e)
        {
            string errMessage = string.Empty;

            Exception realerror = e;
            while (realerror.InnerException != null)
                realerror = realerror.InnerException;

            if (realerror.ToString().ToString().IndexOf("duplicate key") > 0)
                
                errMessage = "System Error: Duplicate value found!";

            else
                errMessage = (e.InnerException.ToString() == null) ? e.Message.ToString() : e.InnerException.ToString();

            return errMessage.ToString();
        }

        public bool logEmail(string session = null, string emailBody = null, string reference_code = null, string reference_table = null, string subject = null)
        {

            user_models userInfo = this.UserData(this.DecryptString(session));
            var _user = userInfo.users;

            tbl_emails email = new tbl_emails();

            email.reference_code    = reference_code;
            email.reference_table   = reference_table;
            email.datetime          = DateTime.Now;
            email.body              = emailBody;
            email.subject           = subject;

            email.include_this_email = _user.email;

            using (db = new dbContext())
            {
                db.email_logs.Add(email);
                db.SaveChanges();
            }

            return true;
        }

        public void logEvent(string session)
        {
            user_models userInfo = this.UserData(this.DecryptString(session));
        }

        public bool sendAnEmail(string session = null, string subject = null, string emailHTML = null)
        {
            SmtpClient client;
            user_models userInfo = this.UserData(this.DecryptString(session));

            var _config = userInfo.system_config;
            var _user  = userInfo.users;
            try
            {
                // Command line argument must the the SMTP host.
                using (client = new SmtpClient())
                {
                    var mail_smtp_host = _config["mail_smtp_host"];
                    var mail_smtp_user = _config["mail_smtp_user"];
                    var mail_smtp_pass = _config["mail_smtp_pass"];
                    var mail_smtp_port = Int32.Parse(_config["mail_smtp_port"]);

                    client.Port         = mail_smtp_port;
                    client.Host         = mail_smtp_host;
                    client.EnableSsl    = true;
                    client.Timeout      = 10000;

                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new System.Net.NetworkCredential(mail_smtp_user, mail_smtp_pass);

                    MailMessage mm = new MailMessage("donot@reply.com" ,_user.email, subject, emailHTML);

                    mm.IsBodyHtml = true;
                    mm.BodyEncoding = UTF8Encoding.UTF8;
                    mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                    client.Send(mm);    

                    return true;
                }

            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}