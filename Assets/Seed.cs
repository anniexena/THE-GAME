using UnityEditor.SceneManagement;
using UnityEngine;

public class Seed : MonoBehaviour
{
    // General
    [SerializeField] Sprite[] plantSprites; // Reference to array of sprites our plant can be
    public Inventory playerInventory; // Reference to the player's inventory

    // Relating to phases of growth
    public int phase = 0; // Current growth phase
    private const int MATURITY = 4; // Phase where tree can sprout seeds
    private float growTimer = 0; // Timer for growing
    private float growWait; // Time spent in a phase

    // Relating to spawning seeds at plant's maturity
    private float seedSpawnTimer = 0; // Timer for growing
    public float seedSpawnWait; // Time spent before spawning seeds
    private float seedSpawnWoffset = 4; // x-offset for spawning seeds
    private float seedSpawnHoffset = 6.6f; // y-offset for spawning seeds
    private int seeds; // Number of seeds to spawn
    private bool spread = false; // Whether we've spawned seeds yet

    // How much initial wood to drop, adds after each phase
    private int wood = 0;

    // Related to cutting down a plant
    private float cutTimer = 0; // Timer for cutting
    public float cutWait = 2.5f; // Time spent cutting
    private bool cutTimerStart = false; // Whether we've started cutting or not

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Sets inventory to correct GameObject
        playerInventory = GameObject.FindGameObjectWithTag("PlayerInventory").GetComponent<Inventory>();

        // Sets sprite and collider
        gameObject.GetComponent<SpriteRenderer>().sprite = plantSprites[phase];
        updateCollider();

        // Determines seeds and wait time for spawning them
        seeds = Random.Range(1, 5);
        seedSpawnWait = Random.Range(10, 20);

        // Determines time spent in the first phase
        growWait = seedSpawnWait + Random.Range(20, 50);

        // Determines wood drop based on starting phase
        for (int i = 0; i < phase; i++)
        {
            wood += Random.Range(1, 2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y); // Updates layering
        growTimer += Time.deltaTime;

        // If our timer's gone off, increment the phase
        if (growTimer > growWait)
        {
            // If still a phase with a sprite, update sprite, phase, collider, and reset growth timer
            if (phase < plantSprites.Length - 1)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = plantSprites[++phase];
                wood += Random.Range(1, 2);
                updateCollider();

                Debug.Log("Plant growing up to phase " + phase + "!");

                // Reset grow timer vars, randomly generates time spent in next phase again
                growTimer = 0;
                growWait = seedSpawnWait + Random.Range(20, 50);
            }
            else // Otherwise, plant has died
            {
                Debug.Log("Plant has now died");
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
            cutTimer += Time.deltaTime;
            if (cutTimer > cutWait)
            {
                // Reset timer variables
                cutTimerStart = false;
                cutTimer = 0;

                growTimer -= cutTimer; // Prevents phase change during cutting
                if (!spread && phase == MATURITY) { playerInventory.addSeeds(seeds); } // Plant would have seeds we could collect
                playerInventory.addWood(wood);
                Debug.Log("Plant cut down");
                Destroy(gameObject);
            }
        }
    }

    // If we left-click on the plant, we start cutting it down
    private void OnMouseDown()
    {
        if (phase > 0)
        {
            Debug.Log("Cutting Plant down");
            cutTimerStart = true;
        }
    }

    // Updates collider based on sprite
    void updateCollider()
    {
        if (GetComponent<BoxCollider2D>() != null)
        {
            Destroy(GetComponent<BoxCollider2D>());
        }
        BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
        collider.size = GetComponent<SpriteRenderer>().bounds.size; // Match sprite size
        collider.offset = Vector2.zero; // Center the collider
    }

    // Sets the phase and updates sprite and collider
    void setPhase(int p)
    {
        if (p >= 0 && p < plantSprites.Length) 
        { 
            phase = p;
            gameObject.GetComponent<SpriteRenderer>().sprite = plantSprites[p];
            updateCollider();
        }
    }

    public int getPhase()
    {
        return phase;
    }

    // Spawns the seeds
    void spawn()
    {
        Debug.Log("Plant disperses " + seeds);
        for (int i = 0; i < seeds; i++)
        {
            float low_x = transform.position.x - seedSpawnWoffset;
            float low_y = transform.position.y - seedSpawnHoffset;
            float high_x = low_x + 2 * seedSpawnWoffset;
            float high_y = low_y + 2* seedSpawnHoffset;

            GameObject newSeed = Instantiate(gameObject, new Vector3(Random.Range(low_x, high_x),
                Random.Range(low_x, high_x), 0), transform.rotation);
            newSeed.GetComponent<Seed>().setPhase(0); // Ensure it starts as a seed
        }
    }
}
