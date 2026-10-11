using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private SoundLibrary sfxLib;
    [SerializeField] private AudioSource audioSource;
    void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); }
    }
    public void PlaySound(AudioClip clip, float volume = 1)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip, volume);
        }
    }
    public void PlaySound(string name, float volume = 1)
    {
        PlaySound(sfxLib.GetClipFromName(name), volume);
    }
    public void StopSound(string name)
    {
        if (audioSource.isPlaying && audioSource.clip == sfxLib.GetClipFromName(name))
        {
            audioSource.Stop();
        }
    }
    public void PlaySound2D(string name)
    {
        audioSource.PlayOneShot(sfxLib.GetClipFromName(name), 0.35f);
    }
}