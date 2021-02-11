using Assets.Scripts.Menus;
using System.Collections;
using UnityEngine;

public class LeavePanel : Panel
{
    public static LeavePanel Instance { get; set; }

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

        CustomStart(SettingsPanelController.instance.panelsList);
    }


    public void LeaveApplication()
    {
        Application.Quit();
    }
}
