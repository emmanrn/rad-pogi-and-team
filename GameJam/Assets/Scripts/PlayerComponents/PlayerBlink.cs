using System.Collections;
using UnityEngine;

public class PlayerBlink : MonoBehaviour
{
    private Player player => Player.Instance;
    [SerializeField] private float blinkInterval = 4f;
    [SerializeField] private float blinkDuration = 1f;

    private float timer;
    private bool isBlinking;

    void Start()
    {
        timer = blinkInterval;
    }

    void Update()
    {
        if (isBlinking)
            return;

        timer -= Time.deltaTime;

        // if (timer <= 0f)
        // {
        //     StartCoroutine(Blink());
        //     timer = blinkInterval;
        // }

        if (Input.GetKeyDown(KeyCode.B) && !isBlinking)
        {
            StartCoroutine(Blink());
            timer = blinkInterval;
        }

        // do some logic with game state and sanity
        // ...
    }

    private IEnumerator Blink()
    {
        isBlinking = true;

        OnBlinkStart();

        yield return new WaitForSeconds(blinkDuration);

        OnBlinkEnd();

        isBlinking = false;
    }

    private void OnBlinkStart()
    {
        Debug.Log("Blink start");
    }

    private void OnBlinkEnd()
    {
        Debug.Log("Blink end");
    }
}
