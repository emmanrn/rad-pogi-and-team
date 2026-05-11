using System.Collections.Generic;
using UnityEngine;

namespace DialogComponents
{
    [CreateAssetMenu(menuName = "Dialogs/DialogObject")]
    public class DialogObject : ScriptableObject
    {
        // simple progression first i guess
        public DialogSpeakerObject speaker;
        public List<string> dialogs;    // how will i add their moods, idk we'll tackle that later
    }
}
