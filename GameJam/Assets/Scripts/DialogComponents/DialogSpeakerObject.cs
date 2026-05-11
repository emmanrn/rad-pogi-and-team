using UnityEngine;
namespace DialogComponents
{
    public enum CharacterSprites
    {
        Neutral,
        Happy,
    }

    [CreateAssetMenu(menuName = "Dialogs/DialogSpeakerObject")]
    public class DialogSpeakerObject : ScriptableObject
    {
        public string speakerName;
        public CharacterSprites[]  characterSprites;
        public float talkingSpeed = 0.25f;
        public AudioClip voiceClip;
    }
}