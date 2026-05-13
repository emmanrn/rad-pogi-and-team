using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


//So much to decouple and make modular here i dont have time.
public class PlayerBlinkSystem : MonoBehaviour
{
    private EnvironmentSystemManager envManager => EnvironmentSystemManager.Instance;
    public static UnityEvent<float> OnPanicMeterChange = new UnityEvent<float>();
    public static UnityEvent<bool> OnBlinkChange = new UnityEvent<bool>();

    [SerializeField] private float panicAmount;
    [SerializeField] private float panicIncreaseMultiplier;
    [SerializeField] private float panicDecreaseMultiplier;

    private Coroutine blinkCoroutine = null;
    private bool isBlinking;

    private void Start()
    {
        SwitchToBlinking();
    }

    void SwitchToBlinking()
    {
        if (SceneManager.GetActiveScene().name == RoomType.PlayRoom)
            isBlinking = true;
    }

    void Update()
    {
        if (panicAmount > 0f && !isBlinking)
        {
            panicAmount = Mathf.Max(panicAmount - panicDecreaseMultiplier * Time.deltaTime, 0f);
            OnPanicMeterChange.Invoke(panicAmount);
        }

        if (Player.Instance == null) return;
        if (Player.Instance.currentState != Player.State.IsPlaying || PauseSystem.Instance.paused) return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (isBlinking)
            {
                if (blinkCoroutine == null) return;

                isBlinking = false;

                StopCoroutine(blinkCoroutine);
                blinkCoroutine = null;

                OnBlinkEnd();
            }
            else
            {
                isBlinking = true;

                if (blinkCoroutine == null)
                    blinkCoroutine = StartCoroutine(Blink());
            }
        }
    }

    private IEnumerator Blink()
    {
        isBlinking = true;
        OnBlinkStart();

        while (panicAmount < 100f && isBlinking)
        {
            //Start Incrementing time if player state IsPlaying, otherwise stop.
            float timePassed = 0f;
            if (Player.Instance.currentState != Player.State.IsPlaying)
                yield return new WaitUntil(() => Player.Instance.currentState == Player.State.IsPlaying);
            else
                timePassed = Time.deltaTime;

            if (panicAmount <= 100f)
            {
                panicAmount = Mathf.Min(panicAmount + panicIncreaseMultiplier * timePassed, 100f);
                OnPanicMeterChange.Invoke(panicAmount);
            }
            yield return null;
        }

        OnBlinkEnd();
        isBlinking = false;
    }

    private void OnBlinkStart()
    {
        envManager.TransformState(true);
        OnBlinkChange.Invoke(true);
        Debug.Log("Blink start");
    }

    private void OnBlinkEnd()
    {
        envManager.TransformState(false);
        OnBlinkChange.Invoke(false);
        Debug.Log("Blink End");
    }
}
