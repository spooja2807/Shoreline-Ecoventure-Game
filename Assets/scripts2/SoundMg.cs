using UnityEngine;

public class SoundMg : MonoBehaviour
{
    public static SoundMg Instance;
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
