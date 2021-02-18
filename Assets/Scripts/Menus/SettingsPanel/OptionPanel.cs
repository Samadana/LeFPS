using Assets.Scripts.Menus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPanel : Panel
{
    public static OptionPanel Instance { get; set; }
    public GameObject background;
    public GameObject panel;
    public Button mainButton;
    public Button quitButton;
    public Dropdown resolutionsDropdown;
    Resolution[] resolutions;


    void Start()
    {
        StartCoroutine(CoroutineWaitAndStart());
    }


    public IEnumerator CoroutineWaitAndStart()
    {
        while (SettingsPanelController.instance == null)
        {
            yield return new WaitForSeconds(0.1f);
        }
        if (Instance != null)
            Destroy(Instance);
        Instance = this;

        GetChildByTag();
        FillResolutions();
        CustomStart(SettingsPanelController.instance.panelsList);
    }

    public void GetChildByTag()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "DropDown")
            {
                resolutionsDropdown = child.GetComponent<Dropdown>();
            }
            if (child.tag == "Background")
            {
                background = child.gameObject;
            }
            if (child.tag == "Illustration")
            {
                panel = child.gameObject;
            }
            if (child.tag == "Button")
            {
                quitButton = child.GetComponent<Button>();
            }      
        }
    }

    public void FillResolutions()
    {
        Debug.Log(resolutionsDropdown);
        resolutions = Screen.resolutions;
        resolutionsDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;

            if (!options.Contains(option))
                options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionsDropdown.AddOptions(options);
        resolutionsDropdown.value = currentResolutionIndex;
        resolutionsDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

}
