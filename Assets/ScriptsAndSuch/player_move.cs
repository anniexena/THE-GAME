using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rigidBody;
    private Vector3 change;
    private Animator animator;
    private bool audioPlaying = false;
    public AudioClip runAudio;
    private AudioSource runAudioSource;

    GameObject startMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        startMenu = GameObject.Find("StartMenu");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            startMenu.SetActive(true);
        }
        // Don't let player move when dialogue is playing
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            animator.SetBool("Moving", false);
            stopRunAudio();
            return;
        }

        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if (change != Vector3.zero) {
            playRunAudio();
            MoveCharacter();
            animator.SetFloat("Move_X", change.x);
            animator.SetFloat("Move_Y", change.y);
            animator.SetBool("Moving", true);
            GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * -100);
        }
        else {
            animator.SetBool("Moving", false);
            stopRunAudio();
        }
    }

    private void playRunAudio()
    {
        if (runAudioSource == null)
        {
            runAudioSource = Instantiate(SFXManager.instance.SFX, transform.position, Quaternion.identity);
            runAudioSource.clip = runAudio;
            runAudioSource.volume = 1f;
            runAudioSource.loop = true;
            runAudioSource.Play();
        }
    }

    private void stopRunAudio()
    {
        if (runAudioSource != null)
        {
            runAudioSource.Stop();
            Destroy(runAudioSource.gameObject);
            runAudioSource = null;
        }
    }

    void MoveCharacter() {
        rigidBody.MovePosition(transform.position + change.normalized * speed * Time.deltaTime);
    }

    public void plantSeeds()
    {
        animator.SetBool("isDigging", true);
        Debug.Log("Planting");
        StartCoroutine(resetPlantingAnimation());
    }

    public IEnumerator resetPlantingAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("isDigging", false);
    }
}
