using TMPro;
using UnityEngine;

namespace DialogComponents
{
    public class DialogUI : MonoBehaviour
    {
        public static DialogUI Instance;
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

        [SerializeField] private TMP_Text dialogText;
        [SerializeField] private GameObject dialogBox;
        private DialogObject currentDialogObject;
        private int index = 0;

        public void StartDialog(DialogObject newDialogObject)
        {
            if (newDialogObject == null)
                return;

            currentDialogObject = newDialogObject;
            index = 0;  // start at the start

            LoadNextDialog();
        }

        // temporarily do text based instead of input i guess

        public void LoadNextDialog()
        {
            if (index > currentDialogObject.dialogs.Count - 1)
            {
                Debug.Log("Running");
                index = 0;
               currentDialogObject = null;
                dialogBox.SetActive(false);

                Player.Instance.SetState(Player.State.IsPlaying);
                return;
            }
            
            dialogText.text = currentDialogObject.dialogs[index];
            dialogBox.SetActive(true);
            index++;
        }
    }
}