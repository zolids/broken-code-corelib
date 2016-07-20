using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TarsierEyes;
using WebApp.Models;
using WebApp.helpers;

namespace WebApp.helpers
{
    public class Helper : IDisposable
    {
       
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
    }
}