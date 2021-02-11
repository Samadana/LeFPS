using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus.MenuMain
{
    public class MainMenu_ProfilPanel : Panel
    {

        public static MainMenu_ProfilPanel instance { get; private set; }
        public InputField PseudoText;
        public Transform playerPanel;

        // Start is called before the first frame update
        void Start()
        {
            if (instance != null)
                Destroy(instance);
            instance = this;
            
            GetChildByTag();            
            if(DBmanager.instance.playerInfos != null) PseudoText.text = DBmanager.instance.playerInfos.pseudo;
            CustomStart(MainMenu.instance.PanelList);
            Debug.Log("MainMenu_ProfilPanel");
        }

        public void GetChildByTag()
        {
            foreach(Transform child in transform)
            {
                if(child.tag == "PlayerPanel")
                {
                    playerPanel = child;
                    foreach (Transform subChild in child)
                    {
                        if (subChild.tag == "Pseudo")
                        {
                            PseudoText = subChild.GetComponent<InputField>();
                        }
                    }
                }
            }
        }
    }
}
