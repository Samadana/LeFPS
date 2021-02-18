using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanelController : MonoBehaviour
{
    public static SettingsPanelController instance { get; private set; }
    public List<GameObject> panelsList;
    public Button optionsButton;
    public Button friendButton;
    public Button chatButton;
    public List<Button> buttonsList;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
        
        GetAllButtonsByName();
        SetPosition();
        SetScale();
    }

    public void SetScale()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    public void SetPosition()
    {
        transform.localPosition = new Vector3(0, 0, 0);
    }

    public void GetAllButtonsByName()
    {
        foreach (Transform child in transform)
        {
            if (child.name == "OptionsButton")
            {
                optionsButton = child.GetComponent<Button>();
                buttonsList.Add(optionsButton);
            }
            if (child.name == "FriendButton")
            {
                friendButton = child.GetComponent<Button>();
                buttonsList.Add(friendButton);
            }
            if (child.name == "ChatButton")
            {
                chatButton = child.GetComponent<Button>();
                buttonsList.Add(chatButton);
            }
        }
    }

    public void ShowFriendButton()
    {
        friendButton.transform.localScale = new Vector3(1, 1, 1);
    }
    public void ShowChatButton()
    {
        chatButton.transform.localScale = new Vector3(1, 1, 1);
    }

    public void SetAllButtonNotInteractable()
    {
        foreach (Button button in buttonsList)
        {
            button.interactable = false;
        }
    }

    public void SetAllButtonInteractable()
    {
        foreach (Button button in buttonsList)
        {
            button.interactable = true;
        }
    }

    public void SetAllPanelsInvisible()
    {
        foreach (GameObject panel in panelsList)
        {
            if (panel.activeSelf)
                panel.SetActive(false);
        }
    }
}
