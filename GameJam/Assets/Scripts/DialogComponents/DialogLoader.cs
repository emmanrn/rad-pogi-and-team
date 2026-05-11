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
    }
}
