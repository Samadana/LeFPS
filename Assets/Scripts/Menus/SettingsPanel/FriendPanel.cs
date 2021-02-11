using Assets.Scripts.Menus;
using Assets.Scripts.Menus.SettingsPanel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendPanel : Panel
{
    public static FriendPanel Instance { get; set; }
    public Transform friendListTransform;
    public Transform addFriendPanel;
    public InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CoroutineWaitAndStart());
    }

    public IEnumerator CoroutineWaitAndStart()
    {
        while(SettingsPanelController.instance == null)
        {
            yield return new WaitForSeconds(0.1f);
        }

        if (Instance != null)
            Destroy(Instance);
        Instance = this;
               
        GetChildByTag();
        ShowHideAddFriendPanel();
        CustomStart(SettingsPanelController.instance.panelsList);
    }

    public void GetChildByTag()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "Panel")
            {
                friendListTransform = child;
            }

            if (child.tag == "SubPanel")
            {
                addFriendPanel = child;
                foreach (Transform subChild in addFriendPanel)
                {
                    if (subChild.tag == "Text")
                    {
                        inputField = subChild.GetComponent<InputField>();
                    }
                }
            }
        }
    }

    public void ShowHideAddFriendPanel()
    {
        if (addFriendPanel.gameObject.activeSelf)
        {
            addFriendPanel.gameObject.SetActive(false);
        }
        else
        {
            addFriendPanel.gameObject.SetActive(true);
        }
    }

    public void AddFriend()
    {
        if (inputField.text != null)
        {
            StartCoroutine(DBmanager.instance.PostAddAFriend(inputField.text));
        }

        ShowHideAddFriendPanel();
    }

    public void DeleteFriend(FriendUI friendUI)
    {
        StartCoroutine(DBmanager.instance.PostDeleteAFriend(friendUI));        
    }

    public void InvokeAllFriends()
    {
        int i = 0;        
        while (i < DBmanager.instance.playerFriendList.Count)
        {
            var friend = (GameObject)Instantiate(Resources.Load("UI/SettingsPanel/FriendUI"));
            var friendScript = friend.GetComponent<FriendUI>();
            friend.transform.SetParent(friendListTransform);
            friendScript.CustomStart();
            friendScript.friend = DBmanager.instance.playerFriendList[i];
            friendScript.SetUIValues();            
            i++;
        }
    }

    public void InvokeAFriend(Friend friend)
    {
        var friendGameObject = (GameObject)Instantiate(Resources.Load("UI/SettingsPanel/FriendUI"));
        var friendUI = friendGameObject.GetComponent<FriendUI>();
        friendUI.friend = friend;
        friendUI.transform.SetParent(friendListTransform);
        friendUI.CustomStart();
        friendUI.SetUIValues();        
    }

    public void DeleteFriendFromFriendListAndDestroyObject(FriendUI friendUI)
    {
        DBmanager.instance.playerFriendList.Remove(friendUI.friend);
        Destroy(friendUI.gameObject);
    }
}
