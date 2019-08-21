using System.Collections.Generic;

namespace UserDataController
{
    interface IUserDataReader
    {
        UserData GetSingleUserData(int id);
        IEnumerable<UserData> GetUserData();
    }
}
