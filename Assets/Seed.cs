using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.U2D;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using static UnityEditor.PlayerSettings;
using System.Collections;

public class Seed : MonoBehaviour
{
    // Changeable Values
    public Sprite[] plantSprites; // Reference to array of sprites our plant can be
    public Inventory playerInventory; // Reference to the player's inventory
    public int phase; // Current growth phase
    public float growWaitLow; // Lowest possible time spent in a phase
    public float growWaitHigh; // Highest possible time spent in a phase
    public float seedSpawnWaitLow; // Lowest possible time spent before spawning seeds
    public float seedSpawnWaitHigh; // Highest possible time spent before spawning seeds
    public float cutWait; // Time needed to cut down tree
    public int woodHigh; // Highest possible number of wood to add each phase 
    public int seedsHigh; // Highest possible number of seeds to spawn
    public float seedSpawnXoffset; // x-offset for spawning seeds
    public float seedSpawnYoffset; // y-offset for spawning seeds
    public string seedName; // Type of seed to spawn
    private Tilemap[] invalidSpawnTiles; // Invalid spawn tiles

    // Properties to be updated based on changeable values
    private int seeds;
    private float seedSpawnWait;
    private float growWait;

    // Properties related to 'drops'
    private const int MATURITY = 4; // Phase where tree can sprout seeds
    private bool spread = false; // Whether we've spawned seeds yet
    private int wood = 0; // How much initial wood to drop, adds after each phase

    // Relating to timers
    private float growTimer = 0; // Timer for growing
    private float cutTimer = 0; // Timer for cutting
    private float seedSpawnTimer = 0; // Timer for growing
    private bool cutTimerStart = false; // Whether we've started cutting or not
    public AudioClip axeAudio;
    public AudioClip treeFallingAudio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Sets inventory to correct GameObject
        playerInventory = GameObject.FindGameObjectWithTag("PlayerInventory").GetComponent<Inventory>();

        // Sets sprite and colliders
        gameObject.GetComponent<SpriteRenderer>().sprite = plantSprites[phase];
        updateColliders();

        // Determines seeds and wait time for spawning them
        seeds = Random.Range(0, seedsHigh);
        seedSpawnWait = Random.Range(seedSpawnWaitLow, seedSpawnWaitHigh);

        // Determines time spent in the first phase
        growWait = Random.Range(growWaitLow, growWaitHigh);

        // Determines wood drop based on starting phase
        for (int i = 0; i < phase; i++) { wood += Random.Range(1, woodHigh); }

        // Sets invalid tiles to spawn on
        InvalidSpawnTiles invalidSpawns = GameObject.Find("InvalidSpawnTiles").GetComponent<InvalidSpawnTiles>();
        invalidSpawnTiles = invalidSpawns.invalidSpawnTiles;

        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * -100);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y); // Updates layering
        growTimer += Time.deltaTime;

        // If our timer's gone off, increment the phase
        if (growTimer > growWait)
        {
            // If still a phase with a sprite, update sprite, phase, collider, and reset growth timer
            if (phase < plantSprites.Length - 1)
            {
                setPhase(phase + 1);
                wood += Random.Range(1, woodHigh);

                //Debug.Log("Plant growing up to phase " + phase + "!");

                // Reset grow timer vars, randomly generates time spent in next phase again
                growTimer = 0;
                growWait = Random.Range(growWaitLow, growWaitHigh);
            }
            else // Otherwise, plant has died
            {
                //Debug.Log("Plant has now died");
                Destroy(gameObject);
            }
        }

        // If we can spread seeds and haven't yet, set off seed-spawning timer
        if (phase == MATURITY && !spread)
        {
            seedSpawnTimer += Time.deltaTime;

            // If timer goes off, spawn seeds and update 'spread'
            if (seedSpawnTimer > seedSpawnWait)
            {
                spawn();
                spread = true;
            }
        }

        // If we've started cutting, wait until timer goes off, then destroy the plant
        if (cutTimerStart)
        {
            cutTimerStart = false;
            StartCoroutine(cutTree());        
        }
    }

    private IEnumerator cutTree()
    {
        SFXManager.instance.PlaySFXClip(treeFallingAudio, transform, 0.5f);
        yield return StartCoroutine(SFXManager.instance.PlaySFXClipAndWait(axeAudio, transform, 1f));
        if (!spread && phase == MATURITY) {
            print("adding seeds " + seeds);
            playerInventory.addSeeds(seedName, seeds); // Plant would have seeds we could collect
        }
        playerInventory.addWood(seedName, wood);
        Destroy(gameObject);
    }

    // If we left-click on the plant, we start cutting it down
    private void OnMouseDown()
    {
        if (phase > 0) { cutTimerStart = true; }
    }

    // Updates collider based on sprite
    void updateColliders()
    {
        BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
        foreach (BoxCollider2D collider in colliders)
        {
            Destroy(collider);
        }


        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Vector2 spriteSize = sprite.rect.size / sprite.pixelsPerUnit;
        BoxCollider2D triggerCollider = gameObject.AddComponent<BoxCollider2D>();
        if (phase > 0)
        {
            BoxCollider2D hitboxCollider = gameObject.AddComponent<BoxCollider2D>();
            hitboxCollider.size = new Vector2(spriteSize.x / 2, spriteSize.y / 2); // Match sprite size
            hitboxCollider.offset = new Vector2(sprite.bounds.center.x, -spriteSize.y / 4); // Center the collider

            triggerCollider.size = spriteSize; // Match sprite size
            triggerCollider.offset = sprite.bounds.center; // Center the collider
            triggerCollider.isTrigger = true;
        }
        else
        {
            triggerCollider.size = spriteSize; // Match sprite size
            triggerCollider.offset = sprite.bounds.center; // Center the collider
            triggerCollider.isTrigger = true;
        }
    }

    // Sets the phase and updates sprite and collider
    void setPhase(int p)
    {
        if (p >= 0 && p < plantSprites.Length) 
        {
            phase = p;
            gameObject.GetComponent<SpriteRenderer>().sprite = plantSprites[p];
            updateColliders();
        }
    }

    public int getPhase()
    {
        return phase;
    }

    // Spawns the seeds
    void spawn()
    {
        int spawn = 0;
        for (int i = 0; i < seeds; i++)
        {
            int spawnAttempts = 5;
            for (int spawnAttempt = 0; spawnAttempt < spawnAttempts; spawnAttempt++)
            {
                float low_x = transform.position.x - seedSpawnXoffset;
                float low_y = transform.position.y - seedSpawnYoffset;
                float high_x = transform.position.x + seedSpawnXoffset;
                float high_y = transform.position.y + seedSpawnYoffset;
                Vector3 spawnPos = new Vector3(Random.Range(low_x, high_x),
                    Random.Range(low_y, high_y), 0);

                // Will be used to ensure seeds don't spawn on top of other objects
                float checkRadius = 4.5f; // Adjust this based on your seed size
                Collider2D hit = Physics2D.OverlapCircle(spawnPos, checkRadius);

                // Will be used to ensure seeds spawn only on grass
                bool validTile = isValidTile(spawnPos);

                if (hit == null && validTile)
                {
                    GameObject newSeed = Instantiate(gameObject, spawnPos, transform.rotation);
                    newSeed.GetComponent<Seed>().setPhase(0); // Ensure it starts as a seed
                    spawn++;
                    break;
                }
            }
        }
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
