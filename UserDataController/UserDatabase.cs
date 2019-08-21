using System;
using System.Collections.Generic;
using System.Text;

namespace UserDataController
{
    class UserDatabase : IUserDataReader, IUserDataWriter
    {
        private Dictionary<int, UserData> _users = new Dictionary<int, UserData>();
        public void AddUser(UserData user)
        {
            _users[user.Id] = user;
        }

        public UserData GetSingleUserData(int id)
        {
            return _users[id];
        }

        public IEnumerable<UserData> GetUserData()
        {
            return _users as IEnumerable<UserData>;
        }

        public void RemoveUser(int id)
        {
            _users.Remove(id);
        }
    }
}
