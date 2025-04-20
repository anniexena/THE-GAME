using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rigidBody;
    private Vector3 change;
    private Animator animator;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Don't let player move when dialogue is playing
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            animator.SetBool("Moving", false);
            return;
        }

        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if (change != Vector3.zero) {
            MoveCharacter();
            animator.SetFloat("Move_X", change.x);
            animator.SetFloat("Move_Y", change.y);
            animator.SetBool("Moving", true);
            GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * -100);
        }
        else {
            animator.SetBool("Moving", false);
        }
    }

    void MoveCharacter() {
        rigidBody.MovePosition(transform.position + change.normalized * speed * Time.deltaTime);
    }
}
