using UnityEngine;

public class Seed : MonoBehaviour
{
    [SerializeField] Sprite[] treeSprites;
    public int phase = 0; //default, overriden in editor for preexisting forest
    public GameObject seed;

    // Relating to phases of growth
    private float growTimer = 0;
    private float growWait = 30f;
    private bool growTimerStart = true;

    // Relating to spawning seeds at maturity
    private int seeds;
    private float seedSpawnTimer = 0;
    public float seedSpawnWait;
    private bool seedSpawnTimerStart;
    private bool spread = false;
    private float wOffset = 4;
    private float hOffset = 6.6f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = treeSprites[phase];
        seeds = Random.Range(1, 5);
        seedSpawnWait = Random.Range(10, 20);
        seedSpawnTimerStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (growTimerStart)
        {
            growTimer += Time.deltaTime;
            if (growTimer > growWait && phase < treeSprites.Length)
            {
                Debug.Log("Plant growing up to phase " + phase + "!");
                gameObject.GetComponent<SpriteRenderer>().sprite = treeSprites[phase++];
                growTimer = 0;
            }
        }

        if (phase == 4 && !spread)
        { 
            seedSpawnTimerStart = true;
            //Debug.Log("we can SEED");
        }

        if (seedSpawnTimerStart)
        {
            seedSpawnTimer += Time.deltaTime;
            if (seedSpawnTimer > seedSpawnWait)
            {
                spawn();
                spread = true;
                seedSpawnTimerStart = false;
            }
        }
    }

    void setPhase(int p)
    {
        if ( p >= 0 && p <= treeSprites.Length) 
        { 
            phase = p;
            gameObject.GetComponent<SpriteRenderer>().sprite = treeSprites[phase];
        }
    }

    void spawn()
    {
        Debug.Log("Tree disperses " + seeds);
        for (int i = 0; i < seeds; i++)
        {
            float low_x = transform.position.x - wOffset;
            float high_x = transform.position.x + wOffset;
            float low_y = transform.position.y - hOffset;
            float high_y = transform.position.y + hOffset;

            GameObject newSeed = Instantiate(seed, new Vector3(Random.Range(low_x, high_x),
                Random.Range(low_x, high_x), 0), transform.rotation);
            Seed seedScript = newSeed.GetComponent<Seed>();

            seedScript.setPhase(0);

        }
    }
}
