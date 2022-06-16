using UnityEngine;
using System;
using System.Collections.Generic;

namespace UI
{   
   public class UIManager : MonoSingleton<UIManager>
   {
      [SerializeField]
      private List<UIPanel> uiDictionary = new List<UIPanel>();

      public void ActivePanel(string key)
      {
         UIPanel uiPanel = uiDictionary.Find(e => e.name == key);

         if (uiPanel != null)
         {
            uiDictionary.ForEach(e => {
               e.panel.SetActive(e.name.CompareTo(key) == 0);
            });
            
            uiPanel.panel.SetActive(true);
         }


         Debug.Log(uiPanel.name);
      }
      
   }


   [Serializable]
   public class UIPanel
   {
      public string name;
      public GameObject panel;
   }
}