using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GeneralUIImage : MonoBehaviour
{
    public static UnityEvent<Sprite, string> OnGeneralUIImageChange = new UnityEvent<Sprite, string>();
    
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Button button;
    [SerializeField] private Image img;
    [SerializeField] private TextMeshProUGUI txt;

    private void Awake()
    {
        button.onClick.AddListener(CloseCanvas);
    }
    
    private void OnChangeImage(Sprite sprite, string text)
    {
        if (canvasGroup == null) return;
        
        Player.Instance.SetState(Player.State.IsViewing);
        
        InitCanvasGroup(true);
        img.sprite = sprite;
        img.preserveAspect = true;
        txt.text = text;
    }

    private void InitCanvasGroup(bool state)
    {
        canvasGroup.alpha = state ? 1f:0f;
        canvasGroup.blocksRaycasts = state;
        canvasGroup.interactable = state;
    }
    
    private void CloseCanvas()
    {
       InitCanvasGroup(false);
       Player.Instance.SetState(Player.State.IsPlaying);
    }
    
    private void OnEnable()
    {
        OnGeneralUIImageChange.AddListener(OnChangeImage);
    }
    
    private void OnDisable()
    {
        OnGeneralUIImageChange.AddListener(OnChangeImage);
    }


}