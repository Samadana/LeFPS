using Assets.Scripts.Common.Helper;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoginMenu : MonoBehaviour
{
    public static LoginMenu instance { get; private set; }
    EventSystem system;
    public InputField loginField;
    public InputField passwordField;
    public Button submitButton;

    void Start()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
        system = EventSystem.current;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();

            if (next != null)
            {

                InputField inputfield = next.GetComponent<InputField>();
                if (inputfield != null) inputfield.OnPointerClick(new PointerEventData(system));  //if it's an input field, also set the text caret

                system.SetSelectedGameObject(next.gameObject, new BaseEventData(system));
            }
        }
    }

    public void CallLogin()
    {
        StartCoroutine(CoroutineCallLogin());
    }

    public IEnumerator CoroutineCallLogin()
    {
        if (loginField.text.Length > 3 && passwordField.text.Length > 3)
        {
            var login = loginField.text;
            var password = passwordField.text;

            var nbTry = 0;
            StartCoroutine(DBmanager.instance.PostConnectUser(login, password));

            while (!DBmanager.instance.connected && nbTry < 10)
            {
                nbTry++;
                yield return new WaitForSeconds(0.5f);
            }

            if (DBmanager.instance.connected)
            {
                SceneHelper.GoMenuLobby();
            }
            else
            {
                Debug.Log("1# Password or Login incorrect");
            }
        }
        else
        {
            Debug.Log("2# Password or Login incorrect");
        }
    }

    public void VerifyInputs()
    {
        submitButton.interactable = (loginField.text.Length >= 1 && passwordField.text.Length >= 1);
    }

}
