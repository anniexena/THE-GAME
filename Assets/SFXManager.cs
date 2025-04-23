using UnityEngine;
using System.Collections;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    public AudioSource SFX;
    public AudioClip ambianceAudio;

    private void Awake()
    {
        if (instance == null) { instance = this; }
        playAmbiance(ambianceAudio, transform, 1f);
    }

    private void playAmbiance(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(SFX, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.loop = true;
        audioSource.Play();
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