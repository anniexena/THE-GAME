using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Dog_Movement : MonoBehaviour
{
    [SerializeField] private float speed;

    public Animator animator;
    public Transform player;
    public Inventory playerInventory; // Reference to the player's inventory
    public Camera cam;
    public bool isFollowing;

    private float stopDistance = 7f;
    private Tilemap[] invalidSpawnTiles; // Invalid spawn tiles
    private bool following = false;
    private Vector3 destination;

    private bool digging = false;
    private float digTimer = 0;
    private float digWait = 2.7f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        destination = player.position;
        animator = GetComponent<Animator>();
        InvalidSpawnTiles invalidSpawns = GameObject.Find("InvalidSpawnTiles").GetComponent<InvalidSpawnTiles>();
        invalidSpawnTiles = invalidSpawns.invalidSpawnTiles;
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueManager.GetInstance().isFollowing) { following = true; }
        else { following = false; }

        if (following)
        {
            Follow(destination);
            if (Input.GetMouseButtonDown(0) && !digging && !DialogueManager.GetInstance().dialogueIsPlaying)
            {
                Vector3 worldPos = cam.ScreenToWorldPoint(Input.mousePosition);
                worldPos.z = 0f; // Prevent disappearing by locking to 2D plane
                bool validTile = isValidTile(worldPos);
                if (validTile) { 
                    destination = worldPos;
                    digging = true;
                }
            }

            if (!digging) { destination = player.position; }
        }
        else
        {
            StopFollow();
        }


        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * -100);
    }

    void Follow(Vector3 pos) {
        // Get direction for animator and distance
        Vector3 direction = (pos - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, pos);

        if (digging) { stopDistance = 1f; }
        else { stopDistance = 7f; }

        if (distance > stopDistance)
        {
            MoveDog(pos);
            animator.SetFloat("Move_X", direction.x);
            animator.SetFloat("Move_Y", direction.y);
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
            if (digging) { Dig(); }
        }
    }
    
    void StopFollow() {
        animator.SetFloat("Move_X", 0);
        animator.SetFloat("Move_Y", 0);
        animator.SetBool("Moving", false);
    }

    void MoveDog(Vector3 pos) {
        transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);
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

    void Dig()
    {
        digTimer += Time.deltaTime;
        if (digTimer > digWait)
        {
            digging = false;
            digTimer = 0;
            int seedAmount = UnityEngine.Random.Range(0, 3);
            int seedTypeInt = UnityEngine.Random.Range(0, 3);
            string seedType = null;
            switch (seedTypeInt)
            {
                case 0:
                    seedType = "Birch";
                    break;
                case 1:
                    seedType = "Pine";
                    break;
                case 2:
                    seedType = "Cherry";
                    break;
            }
            playerInventory.addSeeds(seedType, seedAmount);
            print("added " + seedAmount + " " + seedType + " seeds");
        }
    }


}
