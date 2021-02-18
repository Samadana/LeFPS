using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus.MenuMain
{
    public class MainMenu_PlayPanel : Panel
    {
        public List<GameObject> LoadingObjects;

        public static MainMenu_PlayPanel instance { get; private set; }

        public Button queueButton;

        // Start is called before the first frame update
        void Start()
        {
            if (instance != null)
                Destroy(instance);
            instance = this;
            
            AddLoadingObjects();
            HideLoadingObjects();
            CustomStart(MainMenu.instance.PanelList);
        }

        public void AddLoadingObjects()
        {
            foreach (Transform child in transform)
            {
                if (child.tag == "SubPanel")
                    LoadingObjects.Add(child.gameObject);
            }
        }

        public void HideLoadingObjects()
        {
            foreach (GameObject go in LoadingObjects)
            {
                go.SetActive(false);
            }
        }

        public void ShowLoadingObjects()
        {
            foreach (GameObject go in LoadingObjects)
            {
                go.SetActive(true);
            }
        }
    }
}
