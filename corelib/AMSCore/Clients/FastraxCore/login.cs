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
        /// return all currect system users
        /// </summary>
        /// <param name="username">Query param to get specific user</param>
        /// <returns>gsa_user datatable</returns>
        public DataTable getAllUsers(string username = null){

            _storage.username = username;
            _storage.setStrategy(new usersStrategy());

            return _storage.getAllUsers(); ;
        }
    }
}
