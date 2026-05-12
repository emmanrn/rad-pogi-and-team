using TMPro;

public class TypewriterEffect
{
    private int index;
    private float cps = 20f;
    // private float punctuationDelay = 0.5f;
    private bool skipping;

    public void Reset() => index = 0;

    public void SetSkipping(bool value) => skipping = value;

    public bool Next(TMP_Text text, out float delay, out bool done)
    {
        var info = text.textInfo;

        if (index >= info.characterCount)
        {
            delay = 0;
            done = true;
            return false;
        }

        char c = info.characterInfo[index].character;
        text.maxVisibleCharacters++;

        delay = skipping ? 0.01f : (1f / cps);

        // if (IsPunctuation(c) && !skipping)
        //     delay = punctuationDelay;

        index++;
        done = false;
        return true;
    }

    private bool IsPunctuation(char c)
    {
        return ".?!,:;-".Contains(c);
    }
}