using UnityEngine;

[System.Serializable]
public struct SoundEffect
{
    public string groupID;
    public AudioClip[] clips;
}
public class SoundLibrary : MonoBehaviour
{
    public SoundEffect[] effects;

    public AudioClip GetClipFromName(string name)
    {
        foreach (var fx in effects)
        {
            if (fx.groupID == name)
            {
                return fx.clips[Random.Range(0, fx.clips.Length)];
            }
        }
        return null;
    }
}