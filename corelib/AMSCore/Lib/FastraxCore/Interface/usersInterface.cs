using System.Data;

namespace AMSCore.Lib.FastraxCore.Interface
{
    interface usersInterface
    {
        DataTable getAllUser(Users users);

        void registerUser(Users users);

        void editUser(Users users);

        void forgotPassword(Users users);
    }
}
