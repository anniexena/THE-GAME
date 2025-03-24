using System.Runtime.InteropServices;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public Inventory playerInventory;
    public GameObject seed;
    private int seeds;

    private float seedSpawnTimer = 0;
    public float seedSpawnWait;
    private bool seedSpawnTimerStart = true;
    private float wOffset = 4;
    private float hOffset = 6.6f;

    private float cutTimer = 0;
    public float cutWait = 2.5f;
    private bool cutTimerStart = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("PlayerInventory").GetComponent<Inventory>();
        seeds = Random.Range(1, 5);
        seedSpawnWait = Random.Range(10, 20);
    }

    // Update is called once per frame
    void Update()
    {
        if (seedSpawnTimerStart)
        {
            seedSpawnTimer += Time.deltaTime;
            if (seedSpawnTimer > seedSpawnWait)
            {
                Debug.Log("Tree disperses " + seeds);
                for (int i = 0; i < seeds; i++)
                {
                    float low_x = transform.position.x - wOffset;
                    float high_x = transform.position.x + wOffset;
                    float low_y = transform.position.y - hOffset;
                    float high_y = transform.position.y + hOffset;

                    Instantiate(seed, new Vector3(Random.Range(low_x, high_x), 
                        Random.Range(low_x, high_x), 0), transform.rotation);
                }
                seedSpawnTimerStart = false;
            }
        }

        if (cutTimerStart)
        {
            cutTimer += Time.deltaTime;
            if (cutTimer > cutWait)
            {
                Debug.Log("Tree cut down");
                cutTimerStart = false;
                cutTimer = 0;
                playerInventory.addSeeds(seeds);
                Destroy(gameObject);
            }
        }

    }

    private void OnMouseDown()
    {
        Debug.Log("Cutting tree down");
        cutTimerStart = true;
    }
}
