using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public GameObject popupPanel;
    public void ShowPopup()
    {
        popupPanel.SetActive(true);
    }
    public void HidePopup()
    {
        popupPanel.SetActive(false);
    }
}
