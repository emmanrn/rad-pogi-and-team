using System.Collections;
using TMPro;
using UnityEngine;

namespace DialogComponents
{
    public class DialogUI : MonoBehaviour
    {
        public static DialogUI Instance;

        [SerializeField] private TMP_Text dialogText;
        [SerializeField] private GameObject dialogBox;

        public bool IsTyping { get; private set; }
        private TypewriterEffect typewriter;
        private Coroutine typingRoutine;

        private bool fastSkip;
        public bool FastSkipActive => fastSkip;

        private DialogObject currentDialog;
        private int index;

        void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            typewriter = new TypewriterEffect();
        }

        public void LoadNextDialogue()
        {
            if (IsTyping)
            {
                if (!FastSkipActive)
                {
                    FastSkip();
                }
                else
                {
                    SkipToEnd();
                }
            }
            else
            {
                ShowNextLine();
            }
        }

        public void StartDialog(DialogObject dialogObject)
        {
            currentDialog = dialogObject;
            index = 0;

            Show();
            ShowNextLine();
        }
        public void ShowNextLine()
        {
            if (currentDialog == null)
                return;

            if (index >= currentDialog.dialogs.Count)
            {
                EndDialog();
                return;
            }

            string line = currentDialog.dialogs[index];
            index++;

            PlayLine(line);
        }


        public void PlayLine(string line)
        {
            dialogText.text = line;
            dialogText.ForceMeshUpdate();
            dialogText.maxVisibleCharacters = 0;

            IsTyping = true;

            typewriter.Reset();

            if (typingRoutine != null)
                StopCoroutine(typingRoutine);

            typingRoutine = StartCoroutine(TypeRoutine());
            fastSkip = false;
            typewriter.SetSkipping(false);
        }

        private IEnumerator TypeRoutine()
        {
            bool done = false;

            while (!done)
            {
                if (typewriter.Next(dialogText, out float delay, out done))
                {
                    yield return new WaitForSeconds(delay);
                }
            }

            IsTyping = false;
        }

        public void FastSkip()
        {
            fastSkip = true;
            typewriter.SetSkipping(true);
        }


        public void SkipToEnd()
        {
            if (typingRoutine != null)
                StopCoroutine(typingRoutine);

            dialogText.maxVisibleCharacters = dialogText.textInfo.characterCount;
            IsTyping = false;
        }

        private void EndDialog()
        {
            currentDialog = null;
            Hide();
            Player.Instance.SetState(Player.State.IsPlaying);
        }

        public void Show() => dialogBox.SetActive(true);
        public void Hide() => dialogBox.SetActive(false);
    }
}