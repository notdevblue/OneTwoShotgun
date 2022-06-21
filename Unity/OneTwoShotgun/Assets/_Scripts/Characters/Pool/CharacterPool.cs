using UnityEngine;
using System.Collections.Generic;
using HanSocket.Data;

namespace Characters.Pool
{   
   public class CharacterPool : MonoSingleton<CharacterPool>
   {
      private List<User> _pool = new List<User>();
      private GameObject _playerPrefab;
      public string playerPrefabName = "RemotePlayer";

      private void Start()
      {
         _playerPrefab = Resources.Load<GameObject>(playerPrefabName);

         for (int i = 0; i < 20; ++i)
            Create();
      }

      private User Create()
      {
         User user = Instantiate(_playerPrefab, this.transform)
                     .GetComponent<User>();

         user.gameObject.SetActive(false);
         _pool.Add(user);

         return user;
      }

      public User Get()
      {
         User usr = _pool.Find(x => !x.gameObject.activeSelf);

         if (usr == null)
            usr = Create();

         usr.gameObject.SetActive(true);
         return usr;
      }
   }
}