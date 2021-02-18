using Assets.Scripts.Menus;
using Assets.Scripts.Menus.SettingsPanel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChatPanel : Panel
{
    public static ChatPanel Instance { get; set; }
    public InputField InputTextZoneInputField = null;
    public Transform InputTextZoneContent = null;
    public Transform ButtonGroup;
    public List<GroupChat> groupChatList = new List<GroupChat>();
    public GroupChat ActifGroupChat;

    // Start is called before the first frame update
    void Start()
    {
        FindChildByTag();
        StartCoroutine(CoroutineWaitAndStart());
    }

    public IEnumerator CoroutineWaitAndStart()
    {
        while (SettingsPanelController.instance == null)
        {
            yield return new WaitForSeconds(0.2f);
        }

        if (Instance != null)
            Destroy(Instance);
        Instance = this;
        Debug.Log(this.gameObject.name);
        CustomStart(SettingsPanelController.instance.panelsList);
    }
    
    public void FindChildByTag()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "Button")
            {
                ButtonGroup = child.transform;
            }
            
            if (child.tag == "SubPanel")
            {
                foreach (Transform subChild in child)
                {
                    if (subChild.tag == "SubPanel")
                    {
                        InputTextZoneContent = subChild;
                        foreach (Transform subSubChild in subChild)
                        {
                            if (subSubChild.tag == "Text")
                            {
                                InputTextZoneInputField = subSubChild.transform.GetComponent<InputField>();
                            }
                        }
                    }
                }
            }
        }
    }

    public void MaskAllOtherExceptActifGroupChat()
    {
        foreach (GroupChat gc in groupChatList)
        {
            if(gc.Title != ActifGroupChat.Title)
            {
                gc.ScrollArea.gameObject.SetActive(false);
                gc.ButtonGroupChat.SetColor(new Color(1, 1, 1, 1));
            }
        }
    }

    public void SetActifGroupChatAndMaskOthers(ButtonUIGroupChat button)
    {
        var tempGroupChat = groupChatList.Where(_ => _.ButtonGroupChat == button).FirstOrDefault();
        if (tempGroupChat == null) return;
        tempGroupChat.ButtonGroupChat.SetColor(new Color(1, 0.7f, 0.3f, 1));
        tempGroupChat.ScrollArea.gameObject.SetActive(true);
        MaskAllOtherExceptActifGroupChat();
    }

    public void SetActifGroupChatAndMaskOthers(string friendLogin)
    {
        var tempGroupChat = groupChatList.Where(_ => _.LoginPlayer == friendLogin).FirstOrDefault();
        if (tempGroupChat == null) return;
        tempGroupChat.ButtonGroupChat.SetColor(new Color(1, 0.7f, 0.3f, 1));
        tempGroupChat.ScrollArea.gameObject.SetActive(true);
        MaskAllOtherExceptActifGroupChat();
    }

    public GroupChat InstantiateGroupChat(string friendPseudo, string friendLogin)
    {
        var buttonGroupChat = Instantiate(Resources.Load("UI/SettingsPanel/ButtonGroupChat") as GameObject);
        buttonGroupChat.transform.SetParent(ButtonGroup);

        Debug.Log("J'instantie le group chat du login : " + friendLogin);
        var buttonScript = buttonGroupChat.transform.GetComponent<ButtonUIGroupChat>();
        buttonScript.CustomStart();
        buttonGroupChat.GetComponentInChildren<Text>().text = friendLogin;
        buttonGroupChat.GetComponent<Button>().onClick.AddListener(buttonScript.ShowThisGroupChat);

        var ScrollArea = Instantiate(Resources.Load("UI/SettingsPanel/ScrollArea") as GameObject);
        ScrollArea.transform.SetParent(this.transform);
        ScrollArea.transform.GetComponent<ScrollArea>().CustomStart();

        var groupChat = new GroupChat(friendLogin, friendPseudo, buttonGroupChat.transform.GetComponent<ButtonUIGroupChat>(), ScrollArea.transform.GetComponent<ScrollArea>());
        if (groupChatList.Count == 0)
        {
            ActifGroupChat = groupChat;
        }

        groupChatList.Add(groupChat);       

        return groupChat;
    }

    public void ShowMessageReceive(string loginPlayerSend, string groupChatTitle, string message)
    {
        Debug.Log("Je show le message reçu ShowMessageReceive");
        if (string.IsNullOrEmpty(message)) return;
        var playerSendGroupChat = groupChatList.Where(_ => _.LoginPlayer == loginPlayerSend).FirstOrDefault();

        if (playerSendGroupChat == null)
        {
            playerSendGroupChat = InstantiateGroupChat(groupChatTitle, loginPlayerSend);
        }

        var Date = DateTime.Now;        
        var TextChat = Instantiate(Resources.Load("UI/SettingsPanel/TextChatPanel") as GameObject);
        TextChat.transform.SetParent(playerSendGroupChat.ScrollArea.TextContainerGroup);
        TextChat.transform.localPosition = new Vector3(0, 0, 0);
        TextChat.transform.localScale = new Vector3(1, 1, 1);
        TextChat.GetComponent<Text>().color = new Color(1, 0, 0, 1);
        TextChat.GetComponent<Text>().text = "[" + Date.Hour + ":" + Date.Minute + "] " + loginPlayerSend + " : " + message;
    }

    public void ShowMessageSended(string loginPlayerSend)
    {
        Debug.Log("Je show le message envoyé ShowMessageSended");
        if (string.IsNullOrEmpty(InputTextZoneInputField.text)) return;

        var Date = DateTime.Now;
        var TextChat = Instantiate(Resources.Load("UI/SettingsPanel/TextChatPanel") as GameObject);
        TextChat.transform.SetParent(ActifGroupChat.ScrollArea.TextContainerGroup);
        TextChat.transform.localPosition = new Vector3(0, 0, 0);
        TextChat.transform.localScale = new Vector3(1, 1, 1);
        TextChat.GetComponent<Text>().color = new Color(1, 1, 1, 1);
        TextChat.GetComponent<Text>().text = "[" + Date.Hour + ":" + Date.Minute + "] " + loginPlayerSend + " : " + InputTextZoneInputField.text;
        InputTextZoneInputField.text = "";
    }

}
