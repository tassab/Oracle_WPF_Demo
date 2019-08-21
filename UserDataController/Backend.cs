using System;
using System.Collections.Generic;
using System.Text;
using UIEvents;

namespace UserDataController
{
    public class Backend
    {
        private static UserDatabase db = new UserDatabase();
        public static void InitializeBackend()
        {
            UIEventHandler.Instance.AddUser += PrintUser;
            UIEventHandler.Instance.AddUser += AddUserToDatabase;
        }

        private static void PrintUser(object sender, UserData user)
        {
            Console.WriteLine($"Name: {user.FirstName} {user.LastName}, partner: {user.Partner}");
        }
        private static void AddUserToDatabase(object sender, UserData user)
        {
            db.AddUser(user);
        }
    }
}
