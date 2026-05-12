using UnityEngine;
using UnityEngine.UI;

public class ShardDropper : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites; //Dream State- 0, Dis State - 1
    [SerializeField] private Sprite shardImage;
    [SerializeField] private string shardName;
    
    private SpriteRenderer mySprite;
    public bool isDropped { get; private set; }= false;

    private void Awake()
    {
        mySprite = GetComponent<SpriteRenderer>();
        
        if (sprites[0] == null)
            mySprite.gameObject.SetActive(false);
        mySprite.sprite = sprites[0];
    }
    public void DropShard()
    {
        isDropped = true;
        GeneralUIImage.OnGeneralUIImageChange.Invoke(shardImage, shardName);
        EnvironmentSystemManager.Instance.CheckRoomCompletion();
    }

    public void ChangeSpriteState (bool blinkState)
    {
        Sprite newSprite = null;
        if (!blinkState)
            newSprite = sprites[0];
        else 
            newSprite = sprites[1];

        if (newSprite != null)
        {
            gameObject.SetActive(true);
            mySprite.sprite = newSprite;
        }
        else
           gameObject.SetActive(false);
    }
}