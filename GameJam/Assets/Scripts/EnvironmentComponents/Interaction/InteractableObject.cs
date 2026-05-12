using System;
using System.Collections;
using DialogComponents;
using UnityEngine;

//Serves as jumping point for interactable objects
public class InteractableObject : MonoBehaviour, IInteractable
{
    private DialogLoader _dialogLoader;
    private ShardDropper _shardDropper;
    private PhotographComponent _photographComponent;

    public void Start()
    {
        gameObject.TryGetComponent(out _dialogLoader);
        gameObject.TryGetComponent(out _shardDropper);
        gameObject.TryGetComponent(out _photographComponent);
    }

    public void Interact()
    {
        Debug.Log($"Interacted with {gameObject.name}");
        StartCoroutine(InteractionOperation());
    }

    private IEnumerator InteractionOperation()
    {
        if (_dialogLoader != null)
        {
            Player.Instance.SetState(Player.State.IsInDialogue);
            _dialogLoader.LoadDialog();
            
            if (Player.Instance.currentState == Player.State.IsInDialogue)
                yield return new WaitUntil(() => Player.Instance.currentState != Player.State.IsInDialogue);
        }
        
        if (_shardDropper != null)
        {
            _shardDropper.DropShard();
        }
        
        if (_photographComponent != null)
        {
            _photographComponent.ObtainPhotograph();
        }
        
        yield break;
    }

    private void LateUpdate()
    {
        if (PlayerReference.Instance.cam != null)
            transform.LookAt(PlayerReference.Instance.cam.transform);
    }
}