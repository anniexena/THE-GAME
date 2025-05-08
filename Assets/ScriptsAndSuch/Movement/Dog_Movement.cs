using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Dog_Movement : MonoBehaviour
{
    [SerializeField] private float speed;

    public Animator animator;
    public Transform player;
    private float stopDistance = 7f;
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

    // WILL BE THE DIGGING UP SEEDS PART
    //// Mouse left-click
    //private void OnMouseDown()
    //{
    //    Vector3 spawnPos = new Vector3(Random.Range(low_x, high_x),
    //                Random.Range(low_y, high_y), 0);

    //    // Will be used to ensure seeds don't spawn on top of other objects
    //    float checkRadius = 4.5f; // Adjust this based on your seed size
    //    Collider2D hit = Physics2D.OverlapCircle(spawnPos, checkRadius);

    //    // Will be used to ensure seeds spawn only on grass
    //    bool validTile = isValidTile(spawnPos);

    //    if (hit == null && validTile)
    //    {
    //        GameObject newSeed = Instantiate(gameObject, spawnPos, transform.rotation);
    //        newSeed.GetComponent<Seed>().setPhase(0); // Ensure it starts as a seed
    //        spawn++;
    //        break;
    //    }
    //}

    //bool isValidTile(Vector3 pos)
    //{
    //    foreach (Tilemap tilemap in invalidSpawnTiles)
    //    {
    //        Vector3Int cellPos = tilemap.WorldToCell(pos);
    //        TileBase tileAtPos = tilemap.GetTile(cellPos);
    //        if (tileAtPos != null) { return false; }
    //    }
    //    return true;
    //}
}
