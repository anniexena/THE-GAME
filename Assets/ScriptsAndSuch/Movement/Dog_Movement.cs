using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Dog_Movement : MonoBehaviour
{
    [SerializeField] private float speed;

    public Animator animator;
    public Transform player;
    public Camera cam;
    private float stopDistance = 7f;
    public bool isFollowing;
    private Tilemap[] invalidSpawnTiles; // Invalid spawn tiles

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        InvalidSpawnTiles invalidSpawns = GameObject.Find("InvalidSpawnTiles").GetComponent<InvalidSpawnTiles>();
        invalidSpawnTiles = invalidSpawns.invalidSpawnTiles;
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueManager.GetInstance().isFollowing) {
                Follow();
        }
        
        if (!DialogueManager.GetInstance().isFollowing) {
                StopFollow();
        }

        if (Input.GetMouseButtonDown(0) && DialogueManager.GetInstance().isFollowing)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10f;

            Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
            worldPos.z = 0f;

            // Will be used to ensure seeds spawn only on grass
            bool validTile = isValidTile(worldPos);

            if (validTile)
            {
                print("BARK BARK!"); //MAKE DOG MOVE TO POINT
            }
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

    bool isValidTile(Vector3 pos)
    {
        foreach (Tilemap tilemap in invalidSpawnTiles)
        {
            Vector3Int cellPos = tilemap.WorldToCell(pos);
            TileBase tileAtPos = tilemap.GetTile(cellPos);
            if (tileAtPos != null) { return false; }
        }
        return true;
    }
}
