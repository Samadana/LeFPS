using UnityEngine;

namespace Assets.Scripts.Menus.SettingsPanel
{
    public class Friend
    {
        public bool isConnected;
        public string login;
        public string pseudo;

        public Friend(bool isConnected, string login, string pseudo)
        {
            this.isConnected = isConnected;
            this.login = login;
            this.pseudo = pseudo;
        }
    }
}
