using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanicMeter : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private CanvasGroup canvasGroup;

    private void Start()
    {
        // if (SceneManager.GetActiveScene().name == RoomType.PlayRoom)
        //     gameObject.SetActive(false);
        
        canvasGroup.alpha = 0;
    }

    private void ChangePanicMeter(float amount)
    {
        fillImage.fillAmount = amount/100f;
        if (amount<0)
            canvasGroup.alpha = 0;
        else
        {
            canvasGroup.alpha = 1;
        }
    }

    private void OnEnable()
    {
        PlayerBlinkSystem.OnPanicMeterChange.AddListener(ChangePanicMeter);
    }

    private void OnDisable()
    {
        PlayerBlinkSystem.OnPanicMeterChange.RemoveListener(ChangePanicMeter);
    }
}