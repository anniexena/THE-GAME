using UnityEngine;

public class Dog_Movement : MonoBehaviour
{
    [SerializeField] private float speed;

    public Animator animator;
    public Transform player;
    private float stopDistance = 1f;
    public bool isFollowing;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (!isFollowing)
        //{
        //    isFollowing = DialogueManager.GetInstance().isFollowing
        //}
        if (DialogueManager.GetInstance().isFollowing) {
                Follow();
            }
        
        if (!DialogueManager.GetInstance().isFollowing) {
                StopFollow();
            }
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * -100);
    }

    void Follow() {
        // Get direction for animator and distance
        Vector3 direction = (player.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, player.position);


        if (distance > stopDistance) {
            MoveDog();
            animator.SetFloat("Move_X", direction.x);
            animator.SetFloat("Move_Y", direction.y);
            animator.SetBool("Moving", true);
        }
        else {
            animator.SetBool("Moving", false);
        }
    }
    
    void StopFollow() {
            animator.SetFloat("Move_X", 0);
            animator.SetFloat("Move_Y", 0);
            animator.SetBool("Moving", false);
    }

    void MoveDog() {
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }
}
