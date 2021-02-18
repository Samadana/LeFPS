using UnityEngine;

namespace Assets.Scripts.Menus.SettingsPanel
{
    public class GroupChat
    {
        public string LoginPlayer;
        public string Title;
        public ButtonUIGroupChat ButtonGroupChat;
        public ScrollArea ScrollArea;

        public GroupChat(string login, string pseudo, ButtonUIGroupChat buttonGroupChat, ScrollArea scrollArea)
        {
            LoginPlayer = login;
            Title = pseudo;
            ButtonGroupChat = buttonGroupChat;
            ScrollArea = scrollArea;
        }
    }
}
