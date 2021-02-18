using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class Panel : MonoBehaviour
    {
        private List<GameObject> panelListReference;

        public void CustomStart(List<GameObject> listToFill)
        {            
            panelListReference = listToFill;
            AddThisPanel();
            SetScale();
            HidePanel();
        }

        public void HidePanel()
        {
            if (this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(false);
            }
        }

        public void ShowThisPanelHideOthers()
        {
            foreach (GameObject go in panelListReference)
            {
                if (go.activeSelf)
                    go.SetActive(false);
            }

            this.gameObject.SetActive(true);
        }

        public void AddThisPanel()
        {
            panelListReference.Add(this.gameObject);
        }
        
        public void HideOrShowPanel()
        {
            if (transform.gameObject.activeSelf)
            {
                HidePanel();
            }
            else
            {
                ShowThisPanelHideOthers();
            }
        }

        public void SetScale()
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
