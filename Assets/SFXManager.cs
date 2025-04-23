using UnityEngine;
using System.Collections;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    [SerializeField] private AudioSource SFX;

    private void Awake()
    {
        if (instance == null) { instance = this; }
    }

    public void PlaySFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        // Spawn audiosource
        AudioSource audioSource = Instantiate(SFX, spawnTransform.position, Quaternion.identity);

        // Assign necessary values + play audio
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();

        // Destroy after done playing
        float length = audioSource.clip.length;
        Destroy(audioSource.gameObject, length);
    }

    public IEnumerator PlaySFXClipAndWait(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        // Spawn audiosource
        AudioSource audioSource = Instantiate(SFX, spawnTransform.position, Quaternion.identity);

        // Assign necessary values + play audio
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();

        // Destroy after done playing
        float length = audioSource.clip.length;
        yield return new WaitForSeconds(length);
        Destroy(audioSource.gameObject, length);
    }
}