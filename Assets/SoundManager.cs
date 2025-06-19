using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource waveAudioSource;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void PlayWaveSound()
    {
        if (!waveAudioSource.isPlaying)
            waveAudioSource.Play();
    }

    public void StopWaveSound()
    {
        waveAudioSource.Stop();
    }
}
