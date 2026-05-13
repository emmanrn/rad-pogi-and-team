using System;
using System.Collections;
using UnityEngine;
namespace DialogComponents
{
    public class DialogLoader : MonoBehaviour
    {
        [SerializeField] private DialogObject dialogObject;
        
        public void LoadDialog()
        {
            DialogSystem.Instance.SendDialogToUI(dialogObject);
        }
        
        public IEnumerator AutoLoadDialog()
        {
            yield return new WaitUntil(() => Player.Instance.IsPlayerInitialized);
            DialogSystem.Instance.SendDialogToUI(dialogObject);
        }

        private void Start()
        {
            if (dialogObject.loadOnPlay == true)
                StartCoroutine(AutoLoadDialog());
        }
    }
}
