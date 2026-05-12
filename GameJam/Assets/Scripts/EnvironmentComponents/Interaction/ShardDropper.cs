using UnityEngine;

public class ShardDropper : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites; //Dream State- 0, Dis State - 1
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