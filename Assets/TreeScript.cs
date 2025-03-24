using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public Inventory playerInventory;
    private int seeds;


    private float seedSpawnTimer = 0;
    public float seedSpawnWait;
    private bool seedSpawnTimerStart = true;

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
