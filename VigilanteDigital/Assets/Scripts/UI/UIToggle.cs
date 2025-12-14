using UnityEngine;

public class UI_Toggle : MonoBehaviour
{
    public GameObject targetShow;
    public GameObject targetHide;

    public void Show()
    {
        if (targetShow != null) targetShow.SetActive(true);
    }

    public void Hide()
    {
        if (targetHide != null) targetHide.SetActive(false);
    }
    public void ShowHide()
    {
        if (targetShow != null) targetShow.SetActive(true);
        if (targetHide != null) targetHide.SetActive(false);
    }

}