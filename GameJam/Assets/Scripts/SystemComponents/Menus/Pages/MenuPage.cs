using UnityEngine;

public class MenuPage : MonoBehaviour
{
    public enum PageType { Config, Pause }
    public PageType pageType;
    [SerializeField] private CanvasGroup menu;
    public virtual void Open()
    {
        // menu.SetActive(true);
        menu.alpha = 1;
        menu.interactable = true;
        menu.blocksRaycasts = true;
    }

    public virtual void Close()
    {
        // menu.SetActive(false);
        menu.alpha = 0;
        menu.interactable = false;
        menu.blocksRaycasts = false;
    }
}
