using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace DialogComponents
{
    [CreateAssetMenu(menuName = "Dialogs/DialogObject")]
    public class DialogObject : ScriptableObject
    {
       
        [Serializable]
        public class BGColorOverride
        {
            public Color bgColor = Color.black;
            public int id = 0;
       }
        
        // simple progression first i guess
        public DialogSpeakerObject speaker;
        public List<string> dialogs;    // how will i add their moods, idk we'll tackle that 
        public bool loadOnPlay = false;
        public bool isBGColorOverride = false;
        public BGColorOverride[] BgColorOverrides = null;
    }
}
