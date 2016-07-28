using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TarsierEyes;
using WebApp.helpers;

namespace WebApp.Models
{
    public class user_models : Helper, IDisposable{

        public Users users { get; set; }

        public IEnumerable<wp_pages> pages { get; set; }

        public IEnumerable<user_pages_available> user_pages { get; set; }

        public IEnumerable<tbl_projects> project_involve { get; set; }

        public Dictionary<string, string> system_config { get; set; }

        public string[] module { get; set; }

        public string ip { get; set; }

        public string host { get; set; }

        public bool new_user { get; set; }

    }

    public class UserModule : Helper, IDisposable
    {

        private dbContext _db  = new dbContext();

        public Users ValidateUser(string username, string password)
        {
            
            string newPassword = Cryptography.Encryption.Encrypt(password, Constant.HASHKEY);

            var user = _db.Users.Where(u => u.username == username && u.password == newPassword);

            if (user.Any()) return user.SingleOrDefault();

            return null;

        }

        public Dictionary<string, string> SystemConfig()
        {

            Dictionary<string, string> configList = new Dictionary<string, string>();

            string sql = "SELECT * FROM [extras].[tbl_options];";

            var config = _db.Database.SqlQuery<tbl_config>(sql);

            if (config.Any())
            {
                
                foreach (var item in config)
                {
                    configList.Add(item.name, item.value);
                }
            }
            return configList;
        }

        public IEnumerable<Users> getUser(int user_id = 0, string email = null){

            string where = string.Empty;

            if (user_id > 0) where = "WHERE user_id = " + user_id;
            if (!string.IsNullOrEmpty(email)) where = "WHERE email = '" + email + "'";

            string sql = "SELECT * FROM [dbo].[gsa_users] " + where;

            var users = _db.Database.SqlQuery<Users>(sql);

            return users;

        }

        public IEnumerable<wp_pages> getWPPages(int page_id = 0)
        {
            string where = string.Empty;

            if (page_id > 0) where = "WHERE PageID = " + page_id;

            string sql = "SELECT * FROM [dbo].[wp_pages] " + where;

            var pages = _db.Database.SqlQuery<wp_pages>(sql);

            return pages;
        }

        public IEnumerable<user_pages_available> getUserPages(int user_id = 0)
        {
            string where = string.Empty;

            if (user_id > 0) where = "WHERE t1.user_id = " + user_id;

            string sql = "SELECT t1.user_id, t2.PageID, t2.Page, t2.SubPage, t1.[View], t1.Download, t1.Upload, t2.page_type FROM [dbo].[gsa_user_pages] t1 INNER JOIN [dbo].[wp_pages] t2 ON (t1.PageID = t2.PageID) " + where;

            var user_pages = _db.Database.SqlQuery<user_pages_available>(sql);

            return user_pages;
        }

        public IEnumerable<tbl_projects> getProjectList(int project_id = 0)
        {

            string where = string.Empty;

            if (project_id > 0) where = "WHERE id = " + project_id;

            string sql = "SELECT * FROM [extras].[tbl_projects] " + where;

            var projects = _db.Database.SqlQuery<tbl_projects>(sql);

            return projects;

        }

        public IEnumerable<tbl_tokens> validateToken(string token = null)
        {

            string where = string.Empty;

            if (!string.IsNullOrEmpty(token)) where = "WHERE token = '" + token + "'";

            string sql = "SELECT * FROM [extras].[tbl_tokens] " + where;

            var tokens = _db.Database.SqlQuery<tbl_tokens>(sql);

            return tokens;

        }

        public bool changePassword(string npassword, int user_id) {

            bool success = true;

            if (string.IsNullOrEmpty(npassword))
                return false;

            string sql = "UPDATE [dbo].[gsa_users] SET password = '" + this.EncryptString(npassword) + "' WHERE user_id = " + user_id;

            if (_db.Database.ExecuteSqlCommand(sql) <= 0)
                success = false;

            return success;

        }

        public bool updateToken(string token)
        {

            bool success = true;

            if (string.IsNullOrEmpty(token))
                return false;

            string sql = "UPDATE [extras].[tbl_tokens] SET used = 1 WHERE token ='" + token + "'";

            if (_db.Database.ExecuteSqlCommand(sql) <= 0)
                success = false;

            return success;

        }

    }

}