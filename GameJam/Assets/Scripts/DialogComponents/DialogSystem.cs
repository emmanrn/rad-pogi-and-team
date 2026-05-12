using UnityEngine;

namespace DialogComponents
{
    public class DialogSystem : MonoBehaviour
    {
        public static DialogSystem Instance;


        void Awake()
        {
            if (Instance)
                return;

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void SendDialogToUI(DialogObject dialogObject)
        {
            DialogUI.Instance.StartDialog(dialogObject);
        }


    }
}