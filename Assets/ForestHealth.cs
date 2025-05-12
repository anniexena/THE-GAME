using UnityEngine;
using System;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class ForestHealth : MonoBehaviour
{
    private int health;

    private bool changeTimerStart = false;
    private float changeTimer = 0;
    private int changeRate = 3; // How quickly to be adding/removing animal
    private const int TERMINAL = 5;
    private Tilemap[] invalidSpawnTiles; // Invalid spawn tiles

    public GameObject[] Animals;
    public ForestDecor forestDecor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvalidSpawnTiles invalidSpawns = GameObject.Find("InvalidSpawnTiles").GetComponent<InvalidSpawnTiles>();
        invalidSpawnTiles = invalidSpawns.invalidSpawnTiles;
    }

    // Update is called once per frame
    void Update()
    {
        int animalsActual = GameObject.FindGameObjectsWithTag("Animal").Length;
        GameObject[] plants = GameObject.FindGameObjectsWithTag("Seed");
        int healthyPlants = 0;
        for (int i = 0; i < plants.Length; i++)
        {
            Seed plant = plants[i].GetComponent<Seed>();
            if (plant.getPhase() < TERMINAL) { healthyPlants++; }
        }

        int animalsExpected = Mathf.FloorToInt(healthyPlants * 1.2f); // 7 plants = 8 animals

        if (!changeTimerStart && animalsActual != animalsExpected)
        {
            changeTimerStart = true;
        }

        if (changeTimerStart)
        {
            changeTimer += Time.deltaTime;
            if (changeTimer > changeRate)
            {
                // Reset timer variables
                changeTimerStart = false;
                changeTimer = 0;
                reviseAnimals(animalsActual, animalsExpected);
                reviseDecor(animalsActual, animalsExpected);
            }
        }
    }

    void reviseDecor(int actual, int expected)
    {
        if (actual < expected)
        {
            for (int i = 0; i < 5; i++)
            {
                Vector3 spawnPos = new Vector3(Random.Range(-400, 370), Random.Range(-172, 160), 0);
                float checkRadius = 5f;
                Collider2D hit = Physics2D.OverlapCircle(spawnPos, checkRadius);

                // Will be used to ensure seeds spawn only on grass
                bool validTile = isValidTile(spawnPos);

                if (hit == null && validTile)
                {
                    Instantiate(forestDecor, spawnPos, Quaternion.identity);
                }
            }
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject target = GameObject.Find("ForestDecor");
                Destroy(target);
            }
        }
    }


    void reviseAnimals(int actual, int expected)
    {
        if (actual < expected)
        {
            int animal = Random.Range(0, Animals.Length);
            float checkRadius = 5f;
            Vector3 spawnPos = new Vector3(Random.Range(-400, 370), Random.Range(-172, 160), 0);
            Collider2D hit = Physics2D.OverlapCircle(spawnPos, checkRadius);

            if (hit == null)
            {
                Instantiate(Animals[animal], spawnPos, Quaternion.identity);
            }
        }
        else
        {
            GameObject target = GameObject.Find("Animal");
            Destroy(target);
        }
    }

    public float getHealth()
    {
        float animalsActual = GameObject.FindGameObjectsWithTag("Animal").Length / 5;
        return Mathf.Min(animalsActual, 8f);
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
