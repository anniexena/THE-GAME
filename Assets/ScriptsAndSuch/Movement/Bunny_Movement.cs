using UnityEngine;

public class Bunny_Movement : MonoBehaviour
{
    [SerializeField] private float speed;

    public Animator animator;

    public Transform pointA;
    public Transform pointB;
    private Vector3 target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = pointB.position;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //animator.SetFloat("Direction", 0f);
        // Move bunny towards target
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, target) < 0.1f) {
            if (Vector3.Distance(target, pointA.position) < 0.1f) {
                target = pointB.position;
                animator.SetFloat("Direction", -1f);
            }
            else if (Vector3.Distance(target, pointB.position) < 0.1f) {
                target = pointA.position;
                animator.SetFloat("Direction", 1f);
            }
        }
    }
}
