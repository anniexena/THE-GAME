using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Bunny_Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float waitTime;

    public Animator animator;

    public Transform start;
    public Transform end;
    public Transform middle;
    private Vector3 target;
    private bool waiting;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Start with target at middle
        target = middle.position;
        animator = GetComponent<Animator>();
        animator.SetFloat("Direction", -1f);
        animator.SetBool("Moving", true);
        waiting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!waiting)
        {
            // Move bunny towards target
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target) < 0.1f)
            {
                // start -> middle
                if (Vector3.Distance(target, middle.position) < 0.1f && animator.GetFloat("Direction") == -1f)
                {
                    target = end.position;
                }
                // middle -> end
                else if (Vector3.Distance(target, end.position) < 0.1f)
                {
                    target = middle.position;
                    animator.SetFloat("Direction", 1f);
                    StartCoroutine(Wait());
                }
                // end -> middle
                else if (Vector3.Distance(target, middle.position) < 0.1f && animator.GetFloat("Direction") == 1f)
                {
                    target = start.position;
                }
                // middle -> start
                else if (Vector3.Distance(target, start.position) < 0.1f)
                {
                    target = middle.position;
                    animator.SetFloat("Direction", -1f);
                    StartCoroutine(Wait());
                }
            }
            GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * -100);

        }
    }

    IEnumerator Wait()
    {
        animator.SetBool("Moving", false);
        waiting = true;
        yield return new WaitForSeconds(waitTime);
        waiting = false;
        animator.SetBool("Moving", true);
    }
}
