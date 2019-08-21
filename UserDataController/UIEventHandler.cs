using System;
using System.Collections.Generic;
using System.Text;
using UserDataController;

namespace UIEvents
{
    public class UIEventHandler
    {
        private static UIEventHandler _instance = new UIEventHandler();
        public static UIEventHandler Instance { get { return _instance; } private set { } }

        public event EventHandler<UserData> AddUser;

        public void OnAddUser(object sender, UserData user)
        {
            AddUser?.Invoke(sender, user);
        }
    }
}
