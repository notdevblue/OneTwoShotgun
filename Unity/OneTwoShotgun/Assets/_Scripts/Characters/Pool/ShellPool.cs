using UnityEngine;
using System.Collections.Generic;

namespace Characters.Player.Pool
{   
   public class ShellPool : MonoSingleton<ShellPool>
   {
      private List<Shell> _pool = new List<Shell>();
      private GameObject _shellPrefab;
      public string shellPrefabName = "Shell";

      private void Start()
      {
         _shellPrefab = Resources.Load<GameObject>(shellPrefabName);

         for (int i = 0; i < 1000; ++i)
            Create();
      }

      private Shell Create()
      {
         Shell shell = Instantiate(_shellPrefab, this.transform)
                     .GetComponent<Shell>();

         shell.gameObject.SetActive(false);
         _pool.Add(shell);

         return shell;
      }

      public Shell Get()
      {
         Shell shell = _pool.Find(x => !x.gameObject.activeSelf);

         if (shell == null)
            shell = Create();

         shell.gameObject.SetActive(true);
         return shell;
      }

   }
}