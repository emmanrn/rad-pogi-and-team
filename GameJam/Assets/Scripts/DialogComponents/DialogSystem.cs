using UnityEngine;

namespace DialogComponents
{
    public class DialogSystem : MonoBehaviour
    {
        public static DialogSystem Instance;

        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void SendDialogToUI(DialogObject dialogObject)
        {
            DialogUI.Instance.StartDialog(dialogObject);
        }
    }
}
