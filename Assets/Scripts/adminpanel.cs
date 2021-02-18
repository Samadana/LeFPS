using Assets.Scripts.Common.Helper;
using UnityEngine;
using UnityEngine.UI;

public class adminpanel : MonoBehaviour
{
    public InputField login;
    public InputField pass;
    public LoginMenu loginscript;

    // Start is called before the first frame update
    void Start()
    {
        if (!ConfigManager.adminBuild) Destroy(this.gameObject);
        DestroyAdminPanel();
    }

    public void DestroyAdminPanel()
    {
        foreach (GameObject adminpanel in GameObject.FindGameObjectsWithTag("AdminPanel"))
        {
            if (adminpanel != this.gameObject)
                Destroy(adminpanel);
        }
    }

    public void GoLogin()
    {
        SceneHelper.GoLogin();
    }

    public void GoTestLobby()
    {
        SceneHelper.GoTestLobby();
    }

    public void LoginAdmin()
    {
        login.text = "admin";
        pass.text = "test";
        loginscript.CallLogin();
    }

    public void LoginJ2()
    {
        login.text = "marty";
        pass.text = "maty";
        loginscript.CallLogin();
    }
}
