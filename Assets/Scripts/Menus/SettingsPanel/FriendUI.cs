using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Menus.SettingsPanel
{
    public class FriendUI : MonoBehaviour
    {
        public FriendPanel myPanel;
        public Text pseudoTextComponent;
        public Friend friend;

        public void CustomStart()
        {
            GetChildByTag();
            SetPosition();
            SetScale();
        }

        public void GetChildByTag()
        {
            foreach (Transform child in transform)
            {
                if (child.tag == "Text")
                {
                    pseudoTextComponent = child.GetComponent<Text>();
                }
            }
        }
        
        public void SetPosition()
        {
            transform.localPosition = new Vector3(0, 0, 0);
        }

        public void SetScale()
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
  
        public void SetUIValues()
        {            
            pseudoTextComponent.text = friend.pseudo;
        }

        public void DeleteThisFriend()
        {
            FriendPanel.Instance.DeleteFriend(this);
        }

        public void ClickOnFriendToChat()
        {
            if (SettingsPanelController.instance.chatButton.transform.localScale.x == 0)
            {
                SettingsPanelController.instance.ShowChatButton();
            }

            if(!ChatPanel.Instance.groupChatList.Exists(_=>_.Title == friend.pseudo))
            {
                ChatPanel.Instance.InstantiateGroupChat(friend.pseudo, friend.login);
            }
            else
            {
                ChatPanel.Instance.SetActifGroupChatAndMaskOthers(friend.pseudo);
            }

            ChatPanel.Instance.ShowThisPanelHideOthers();
        }
    }
}
