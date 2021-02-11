using Assets.Scripts.Common.Helper;
using Assets.Scripts.Menus.MenuMain;
using Assets.Scripts.Menus.SettingsPanel;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DBmanager;
using Debug = UnityEngine.Debug;

public class PlayerMenuController : NetworkBehaviour
{
    public List<GameObject> MainMenuPanelList = new List<GameObject> { };
    public List<Friend> playerFriendList = new List<Friend>();
    public PlayerInfos playerInfos = null;

    void Start()
    {
        GetMyDbInfos();
        StartCoroutine(CoroutineWaitLevelFinishLoading());        
    }

    public IEnumerator CoroutineWaitLevelFinishLoading()
    {
        while (playerInfos == null)
        {
            yield return new WaitForSeconds(1);
        }

        CustomStart();
    }

    public void GetMyDbInfos()
    {
        playerFriendList = DBmanager.instance.playerFriendList;
        playerInfos = DBmanager.instance.playerInfos;
    }

    // Start is called before the first frame update
    void CustomStart()
    {
        if (!isLocalPlayer) return;       
        FriendPanel.Instance.InvokeAllFriends();
        SettingsPanelController.instance.ShowFriendButton();
        InvokeQueueButton();
        InvokeSendMessageButton();
        CmdPostNewPlayerConnect(gameObject, playerInfos.login, playerInfos.pseudo);
    }

    //We have to invoke it so we'll can call Command from it
    void InvokeQueueButton()
    {
        GameObject QueueButton = Instantiate(Resources.Load("UI/Button/QueueButton", typeof(GameObject)), MainMenu_PlayPanel.instance.transform) as GameObject;
        QueueButton.transform.localPosition = new Vector3(0, -55, 0);
        MainMenu_PlayPanel.instance.queueButton = QueueButton.GetComponent<Button>();
        MainMenu_PlayPanel.instance.queueButton.onClick.AddListener(PostQueuePlayer);
    }

    //We have to invoke it so we'll can call Command from it
    void InvokeSendMessageButton()
    {
        GameObject sendMessageButton = Instantiate(Resources.Load("UI/Button/SendMessageButton", typeof(GameObject)), ChatPanel.Instance.InputTextZoneContent) as GameObject;
        sendMessageButton.transform.localPosition = new Vector3(624, 0, 0);
        sendMessageButton.GetComponent<Button>().onClick.AddListener(SendMessage);
    }
    void SendMessage()
    {
        Debug.Log("SendMessage()");
        CmdSendMessage(playerInfos.login, playerInfos.pseudo, ChatPanel.Instance.ActifGroupChat.LoginPlayer, ChatPanel.Instance.InputTextZoneInputField.text);
        Debug.Log("playerInfo login : " + playerInfos.login);
    }

    void PostQueuePlayer()
    {
        LauchGame();
    }

    [Command]
    public void CmdSendMessage(string loginPlayerSendMessage, string pseudoPlayerSendMessage, string loginPlayerGetMessage, string message)
    {      

        Debug.Log(loginPlayerSendMessage +" WantToSendMessage: to " + loginPlayerGetMessage);
        //var connectedPlayersFind = QueueManager.instance.connectedPlayers.Where(_ => _.playerInfos.login == loginPlayerGetMessage || playerInfos.login == loginPlayerSendMessage).ToList();
               
    }

    [ClientRpc]
    public void RpcReceiveMessage(string loginPlayerSend, string pseudoPlayerSend, string message)
    {
        Debug.Log("Rpc show message receive");
        ChatPanel.Instance.ShowMessageReceive(loginPlayerSend, pseudoPlayerSend, message);
    }

    [ClientRpc]
    public void RpcMessageHasBeenSend(string loginPlayerSend)
    {
        Debug.Log("Rpc show message send");
        ChatPanel.Instance.ShowMessageSended(loginPlayerSend);
    }

    public void LauchGame()
    {
        MenuNetworkControllerClient.instance.StopClient();

        Debug.Log("J'essaie de me deconnecter du server MainMenu");

        Destroy(MenuNetworkControllerClient.instance.gameObject);

        SceneHelper.GoGameLobby();
    }


    [Command]
    public void CmdPostNewPlayerConnect(GameObject player, string login, string pseudo)
    {
        if (!isServer) { return; }
        var playerMenuController = player.GetComponent<PlayerMenuController>();
        playerMenuController.playerInfos.login = login;
        playerMenuController.playerInfos.pseudo = pseudo;

        Debug.Log("PlayerConnected, login : " + playerMenuController.playerInfos.login);
    }
}
