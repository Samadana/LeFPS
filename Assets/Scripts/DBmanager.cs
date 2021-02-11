using Assets.Scripts.Menus.SettingsPanel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DBmanager : MonoBehaviour
{
    public static DBmanager instance { get; private set; }
    public string login;
    public List<string> accountInformations = null;

    public List<Friend> playerFriendList = new List<Friend>();
    public PlayerInfos playerInfos;

    public bool connected;

    public class PlayerInfos
    {
        public int id;
        public string login;
        public string pseudo;
        public string lastConnectionDate;
    }

    void Start()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;

        playerInfos = new PlayerInfos();
    }

    public void LogOut()
    {
        login = null;
    }

    public IEnumerator PostConnectUser(string login, string password)
    {
        Debug.Log(login);
        WWWForm form = new WWWForm();
        form.AddField("login", login);
        form.AddField("password", password);
        WWW www = new WWW("http://46.101.230.201/sqlconnect/GetLogin.php", form);
        yield return www;
        Debug.Log(www.text);
        if (int.TryParse(www.text, out int returnedInt))
        {
            this.login = login;
            playerInfos.id = returnedInt;
            StartCoroutine(GetAccountInformations());
            StartCoroutine(GetAllFriend());
        }
        else
        {
            Debug.Log("User login failed. Error #" + www.text);
        }
    }

    public IEnumerator GetAccountInformations()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", playerInfos.id);
        WWW www = new WWW("http://46.101.230.201/sqlconnect/GetAccountInformations.php", form);
        yield return www;
        Debug.Log(www.text);
        var accountInformationsTemp = www.text.Split(',');
        accountInformations = accountInformationsTemp.ToList();
        playerInfos.login = accountInformations[1];
        playerInfos.pseudo = accountInformations[2];
        playerInfos.lastConnectionDate = accountInformations[3];
        StartCoroutine(PutUpdateAccountInformations());
    }

    public IEnumerator GetAllFriend()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", playerInfos.id);
        WWW www = new WWW("http://46.101.230.201/sqlconnect/GetAllFriend.php", form);
        yield return www;
        Debug.Log(www.text);
        if (!String.IsNullOrEmpty(www.text))
        {
            var friendListTemp = www.text.Split(',');
            for (int i = 0; i < friendListTemp.Length; i++)
            {
                if(!string.IsNullOrEmpty(friendListTemp[i]))
                {
                    playerFriendList.Add(new Friend(false, friendListTemp[i], friendListTemp[i + 1]));
                }
                i++;
            }
        }
    }

    public IEnumerator PutUpdateAccountInformations()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", playerInfos.id);
        WWW www = new WWW("http://46.101.230.201/sqlconnect/PutUpdateAccountInformations.php", form);
        yield return www;
        Debug.Log(www.text);
        connected = true;
    }  

    public IEnumerator PostAddAFriend(string friendLogin)
    {
        WWWForm form = new WWWForm();        
        form.AddField("idaccount", playerInfos.id);
        form.AddField("friendlogin", friendLogin);
        WWW www = new WWW("http://46.101.230.201/sqlconnect/PostAddFriend.php", form);
        yield return www;
        Debug.Log(www.text);
        if(!string.IsNullOrEmpty(www.text))
        {
            var friendArray = www.text.Split(',');
            var friendAdded = new Friend(false, friendArray[0], friendArray[1]);
            playerFriendList.Add(friendAdded);
            FriendPanel.Instance.InvokeAFriend(friendAdded);
        }
    }

    public IEnumerator PostDeleteAFriend(FriendUI friendUI)
    {
        WWWForm form = new WWWForm();
        form.AddField("idaccount", playerInfos.id);
        form.AddField("friendlogin", friendUI.friend.login);
        WWW www = new WWW("http://46.101.230.201/sqlconnect/PostDeleteFriend.php", form);
        yield return www;
        Debug.Log(www.text);
        if (!String.IsNullOrEmpty(www.text) &&  int.Parse(www.text) > 0)
        {
            FriendPanel.Instance.DeleteFriendFromFriendListAndDestroyObject(friendUI);
        }
    }

    public IEnumerator PostCreateMatch(int port)
    {        
        WWWForm form = new WWWForm();
        form.AddField("port", port);
        WWW www = new WWW("http://46.101.230.201/sqlconnect/PostCreateMatch.php", form);
        yield return www;
        Debug.Log(www.text);
    }
}
