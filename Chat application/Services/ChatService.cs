using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace Chat_application.Services
{
    public class ChatService
    {                                       //key : value EX: username: connectionID
        private static readonly Dictionary<string, string> Users = new Dictionary<string, string>();

        public bool AddUserToList(string userToAdd)
        {
            lock(Users)
            {
                foreach(var user in Users)
                {
                    if(user.Key.ToLower() == userToAdd.ToLower())
                    {
                        return false;
                    }
                }

                Users.Add(userToAdd, null);
                return true;
            }
        }

        public void AddUserConnectionId(string user, string connectionId)
        {
            lock(Users)
            { 
                if(Users.ContainsKey(user))
                    Users[user] = connectionId;  //add connection id to user in Users
            }
        }

        public string GetUserByConnectionId(string connectionId) 
        {
            lock (Users)
            {
                return Users.Where(x => x.Value ==connectionId).Select(x => x.Key).FirstOrDefault();
            }
        }

        public string GetConnectionIdByUser(string user)
        {
            lock (Users)
            {
                return Users.Where(x => x.Key == user).Select(x => x.Value).FirstOrDefault();
            }
        }

        public void RemoveUserFromList(string user)
        {
            lock(Users)
            {
                if(Users.ContainsKey(user))
                {
                    Users.Remove(user);
                }
            }
        }

        public string[] GetOnlineUsers()
        {
            lock(Users)
            {
                return Users.OrderBy(x => x.Key).Select(x => x.Key).ToArray();
            }
        }
    }
}
