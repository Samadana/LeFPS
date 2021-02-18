using UnityEngine;

public class TextChatPanel : MonoBehaviour
{
    // Start is called before the first frame update
    public void CustomStart()
    {
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
}
