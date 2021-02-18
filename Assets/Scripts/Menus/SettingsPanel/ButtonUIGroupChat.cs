using UnityEngine;
using UnityEngine.UI;

public class ButtonUIGroupChat : MonoBehaviour
{
    public Button button;

    // Start is called before the first frame update
    public void CustomStart()
    {
        button = GetComponent<Button>();
        SetSize();
        SetPosition();
    }

    public void SetSize()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    public void SetPosition()
    {
        transform.localPosition = new Vector3(0, 0, 0);
    }

    public void SetColor(Color color)
    {
        var colors = button.colors;
        colors.normalColor = color;
        colors.highlightedColor = color;
        colors.pressedColor = color;
        colors.selectedColor = color;
        button.colors = colors;
    }

    public void ShowThisGroupChat()
    {
        ChatPanel.Instance.SetActifGroupChatAndMaskOthers(this);
    }
}