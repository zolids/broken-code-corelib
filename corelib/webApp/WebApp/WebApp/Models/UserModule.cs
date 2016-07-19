using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TarsierEyes;
using WebApp.helpers;

namespace WebApp.Models
{
    public class user_models : Helper, IDisposable{

        public IEnumerable<Users> users { get; set; }

        public IEnumerable<wp_pages> pages { get; set; }

        public IEnumerable<user_pages_available> user_pages { get; set; }

        public IEnumerable<tbl_projects> project_involve { get; set; }

        public bool user_type { get; set; }

    }

    public class UserModule : Helper, IDisposable
    {

        private const string key = "ams-tmp-web";
        private dbContext _user = new dbContext();

        public bool ValidateUser(string username, string password)
        {
            
            string newPassword = Cryptography.Encryption.Encrypt(password, key);

            var user = _user.Users.Where(u => u.username == username && u.password == newPassword);

            if (user.Any()) return true;

            return false;
        }

        public IEnumerable<Users> getUser(int user_id = 0){

            string where = string.Empty;

            if (user_id > 0) where = "WHERE user_id = " + user_id;

            string sql = "SELECT * FROM [dbo].[gsa_users] " + where;

            var users = _user.Database.SqlQuery<Users>(sql);

            return users;

        }

        public IEnumerable<wp_pages> getWPPages(int page_id = 0)
        {
            string where = string.Empty;

            if (page_id > 0) where = "WHERE PageID = " + page_id;

            string sql = "SELECT * FROM [dbo].[wp_pages] " + where;

            var pages = _user.Database.SqlQuery<wp_pages>(sql);

            return pages;
        }

        public IEnumerable<user_pages_available> getUserPages(int user_id = 0)
        {
            string where = string.Empty;

            if (user_id > 0) where = "WHERE t1.user_id = " + user_id;

            string sql = "SELECT t1.user_id, t2.PageID, t2.Page, t2.SubPage, t1.[View], t1.Download, t1.Upload, t2.page_type FROM [dbo].[gsa_user_pages] t1 INNER JOIN [dbo].[wp_pages] t2 ON (t1.PageID = t2.PageID) " + where;

            var user_pages = _user.Database.SqlQuery<user_pages_available>(sql);

            return user_pages;
        }

        public IEnumerable<tbl_projects> getProjectList(int project_id = 0)
        {

            string where = string.Empty;

            if (project_id > 0) where = "WHERE id = " + project_id;

            string sql = "SELECT * FROM [extras].[tbl_projects] " + where;

            var projects = _user.Database.SqlQuery<tbl_projects>(sql);

            return projects;

        }

    }

}