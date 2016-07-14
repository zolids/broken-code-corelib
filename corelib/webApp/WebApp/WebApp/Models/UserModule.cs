using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TarsierEyes;

namespace WebApp.Models
{
    public class UserModule
    {

        private const string key = "ams-tmp-web";
        private dbContext _user;

        public bool ValidateUser(string username, string password)
        {
            _user = new dbContext();
     
            string newPassword = Cryptography.Encryption.Encrypt(password, key);

            var user = _user.Users.Where(u => u.username == username && u.password == newPassword);

            if (user.Any()) return true;

            return false;
        }
    }
}