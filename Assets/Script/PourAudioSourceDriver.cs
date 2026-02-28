using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PourAudioSourceDriver : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private void Reset()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        audioSource.loop = true;
        audioSource.playOnAwake = false;
    }

    public void StartLoop()
    {
        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    public void StopLoop()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
    }

    public void SetIntensity(float value)
    {
        audioSource.volume = Mathf.Clamp01(value);
    }
}
