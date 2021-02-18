using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus.MenuMain
{
    public class MainMenu : Panel
    {
        public List<GameObject> PanelList;

        public static MainMenu instance { get; private set; }

        public Button queueButton;

        // Start is called before the first frame update
        void Start()
        {
            if (instance != null)
                Destroy(instance);
            instance = this;            
        }
    }
}
