using System.Collections.Generic;

namespace HanSocket.Data
{
   public class GameData : Singleton<GameData>
   {
      private List<User> _users;

      public GameData()
      {
         _users = new List<User>();
      }


      public void AddUser(User user)
      {
         _users.Add(user);
      }

      public void DeleteUser(int id)
      {
         User usr = _users.Find(x => x.id == id);

         if (usr != null)
            _users.Remove(usr);
      }

      public int GetUserCount()
         => _users.Count;
   }
}