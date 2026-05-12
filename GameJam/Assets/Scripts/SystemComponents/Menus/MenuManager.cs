using System.Linq;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }
    private MenuPage activePage = null;
    private bool isOpen = false;
    [SerializeField] private CanvasGroup root;
    [SerializeField] private GameObject buttons;
    [SerializeField] private MenuPage[] pages;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            DestroyImmediate(gameObject);
    }

    private MenuPage GetPage(MenuPage.PageType pageType)
    {
        return pages.FirstOrDefault(page => page.pageType == pageType);
    }

    public void OpenConfigPage()
    {
        var page = GetPage(MenuPage.PageType.Config);
        OpenPage(page);
    }
    public void OpenPausePage()
    {
        var page = GetPage(MenuPage.PageType.Pause);
        OpenPage(page);
    }

    private void OpenPage(MenuPage page)
    {
        if (page == null)
            return;

        if (activePage != null && activePage != page)
            activePage.Close();

        page.Open();
        activePage = page;

        if (!isOpen)
            OpenRoot();

    }

    public void CloseButtons()
    {
        buttons.SetActive(false);
    }
    public void OpenButtons()
    {
        buttons.SetActive(true);
    }

    public void OpenRoot()
    {
        root.alpha = 1;
        root.blocksRaycasts = true;
        root.interactable = true;
        isOpen = true;
    }

    public void CloseRoot()
    {
        root.alpha = 0;
        root.blocksRaycasts = false;
        root.interactable = false;
        isOpen = false;
    }
}
