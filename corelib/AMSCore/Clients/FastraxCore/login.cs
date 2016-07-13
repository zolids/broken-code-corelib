using System.Data;
using AMSCore.Lib.FastraxCore.Strategies;

namespace AMSCore.Clients.FastraxCore
{
    public class login
    {
        private Users _storage;

        /// <summary>
        /// Initilized login client open new db connections
        /// </summary>
        /// <param name="conn">as connection string = Server=serverName;Database=dbname;User Id=userId</param>
        public login(string conn = null)
        {

            _storage = new Users();
           
            if (!string.IsNullOrEmpty(conn))
                _storage.connectionString = conn;

            _storage.createSQLDB();
        }

        /// <summary>
        /// Get all or specific system users
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>gsa_user datatable</returns>
        public DataTable getUsers(string username = null, string password = null){

            _storage.username = username;
            _storage.password = password;

            _storage.setStrategy(new usersStrategy());

            return _storage.getUsers(); ;
        }
    }
}
