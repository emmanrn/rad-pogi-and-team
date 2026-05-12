using UnityEngine;
namespace DialogComponents
{
    public class DialogLoader : MonoBehaviour, IInteractable
    {
        [SerializeField] private DialogObject dialogObject;
        public void LoadDialog()
        {
            DialogSystem.Instance.SendDialogToUI(dialogObject);
        }

        public void Interact()
        {
            Player.Instance.SetState(Player.State.IsInDialogue);
            LoadDialog();
        }
    }
}
