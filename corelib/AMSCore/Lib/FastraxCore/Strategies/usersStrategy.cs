﻿using System;
using System.Data;
using AMSCore.Lib.FastraxCore.Interface;

namespace AMSCore.Lib.FastraxCore.Strategies
{
    public class usersStrategy  : usersInterface
    {
        public DataTable getAllUser(Users users)
        {

            string where = string.Empty;

            if (!string.IsNullOrEmpty(users.username) 
                && !string.IsNullOrEmpty(users.password))

                where = " WHERE username = " + users.username + " AND password = " + users.password;

            string sql = "SELECT * FROM [dbo].[gsa_users]" + where;

            return users.executeSelectQuery(sql);

        }

        public void registerUser(Users users)
        {
            throw new NotImplementedException();
        }

        public void editUser(Users users)
        {
            throw new NotImplementedException();
        }

        public void forgotPassword(Users users)
        {
            throw new NotImplementedException();
        }
    
    }

}
