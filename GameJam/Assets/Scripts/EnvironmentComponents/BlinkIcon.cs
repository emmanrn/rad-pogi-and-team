using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BlinkIcon : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Image img;
    [SerializeField] private TextMeshProUGUI txt;

    private void Start()
    {
       DisableIconOnPlayerRoom();
    }

    private void DisableIconOnPlayerRoom()
    {
        if (SceneManager.GetActiveScene().name == RoomType.PlayRoom)
        {
            img.sprite = sprites[2];
            txt.text = "???";
        }
    }
    
    private void ChangeBlinkSprite(bool isBlink)
    {
        img.sprite = isBlink ? sprites[1] : sprites[0];
    }

    private void OnEnable()
    {
        PlayerBlinkSystem.OnBlinkChange.AddListener(ChangeBlinkSprite);
    }

    private void OnDisable()
    {
        PlayerBlinkSystem.OnBlinkChange.RemoveListener(ChangeBlinkSprite);
    }
}