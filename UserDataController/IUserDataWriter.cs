namespace UserDataController
{
    interface IUserDataWriter
    {
        void AddUser(UserData user);
        void RemoveUser(int id);
    }
}
