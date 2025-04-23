using UnityEngine;
using System.Collections;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    public AudioSource SFX;
    public AudioClip ambianceAudio;
    public AudioClip[] songs;

    private float musicTimer = 0f;
    private float musicWait;

    private void Awake()
    {
        //musicWait = Random.Range(120, 180);
        musicWait = Random.Range(0, 3);
        if (instance == null) { instance = this; }
        playAmbiance(ambianceAudio, transform, 1f);
    }

    void Update() {
        musicTimer += Time.deltaTime;
        if (musicTimer > musicWait)
        {
            int songChoice = Random.Range(0, songs.Length);
            StartCoroutine(PlaySFXClipAndWait(songs[songChoice], transform, 1f));
            musicTimer = 0;
            musicWait = Random.Range(100, 150);
        }
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