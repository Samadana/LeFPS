using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollArea : MonoBehaviour
{
    public Transform TextContainerGroup;

    // Start is called before the first frame update
    public void CustomStart()
    {
        SetSize();
        SetPosition();
        FindChild();
    }

    public void FindChild()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "Text")
            {
                foreach (Transform subChild in child)
                {
                    if (subChild.tag == "Text")
                    {
                        TextContainerGroup = subChild.transform;
                    }
                }
            }
        }

    }

    public void SetSize()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    public void SetPosition()
    {
        transform.localPosition = new Vector3(0, 100, 0);
    }
}
