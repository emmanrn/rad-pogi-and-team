using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanicMeter : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == RoomType.PlayRoom)
            gameObject.SetActive(false);
    }

    private void ChangePanicMeter(float amount)
    {
        fillImage.fillAmount = amount/100f;
        //Debug.Log(amount);
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