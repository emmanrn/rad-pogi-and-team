using System.Collections;
using UnityEngine;
using UnityEngine.Events;



//So much to decouple and make modular here i dont have time.
public class PlayerBlinkSystem : MonoBehaviour
{
    private EnvironmentSystemManager envManager => EnvironmentSystemManager.Instance;
    public static UnityEvent<float> OnPanicMeterChange =  new UnityEvent<float>();
    
    [SerializeField] private float panicAmount;
    [SerializeField] private float panicIncreaseMultiplier;
    [SerializeField] private float panicDecreaseMultiplier;
    
    private Coroutine blinkCoroutine;
    private bool isBlinking;
    
    void Update()
    {
        if (panicAmount > 0f && !isBlinking)
        {
            panicAmount = Mathf.Max(panicAmount - panicDecreaseMultiplier * Time.deltaTime,   0f);
            OnPanicMeterChange.Invoke(panicAmount);
        }
        
        if (Input.GetKeyDown(KeyCode.B))
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
            if (Player.Instance.currentState != Player.State.IsPlaying)
                continue;
            
            if (panicAmount <= 100f)
            {
                panicAmount = Mathf.Min(panicAmount + panicIncreaseMultiplier * Time.deltaTime, 100f);
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
        Debug.Log("Blink start");
    }

    private void OnBlinkEnd()
    {
        envManager.TransformState(false);
        Debug.Log("Blink End");
    }
}
