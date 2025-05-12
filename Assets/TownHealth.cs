using UnityEngine;
using UnityEditor.Rendering;
using UnityEngine.Tilemaps;

public class TownHealth : MonoBehaviour
{
    private float healthyHouses;
    public TownDecor townDecor;
    private bool changeTimerStart = false;
    private float changeTimer = 0;
    private int changeRate = 3;
    private Tilemap[] invalidSpawnTiles; // Invalid spawn tiles

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvalidSpawnTiles invalidSpawns = GameObject.Find("InvalidSpawnTiles").GetComponent<InvalidSpawnTiles>();
        invalidSpawnTiles = invalidSpawns.invalidSpawnTiles;

        healthyHouses = GameObject.FindGameObjectsWithTag("House").Length;
    }

    // Update is called once per frame
    void Update()
    {
        healthyHouses = 8;
        GameObject[] houses = GameObject.FindGameObjectsWithTag("House");
        for (int i = 0; i < houses.Length; i++)
        {
            House house = houses[i].GetComponent<House>();
            if (house.getFixesNeeded() == 2) { healthyHouses--; }
            else if (house.getFixesNeeded() == 1) { healthyHouses -= 0.5f; }
            else { healthyHouses++; }
        }

        int decorActual = GameObject.FindGameObjectsWithTag("TownDecor").Length;
        int decorExpected = Mathf.FloorToInt(healthyHouses * 2f); // 7 plants = 8 animals

        if (!changeTimerStart && decorActual != decorExpected)
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
                reviseDecor(decorActual, decorExpected);
            }
        }
    }

    void reviseDecor(int actual, int expected)
    {
        if (actual < expected)
        {
            for (int i = 0; i < 3; i++)
            {
                Vector3 spawnPos = new Vector3(Random.Range(-138, 99), Random.Range(-70, 65), 0);
                float checkRadius = 5f;
                Collider2D hit = Physics2D.OverlapCircle(spawnPos, checkRadius);
                bool validTile = isValidTile(spawnPos);

                if (hit == null && validTile)
                {
                    Instantiate(townDecor, spawnPos, Quaternion.identity);
                }
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject target = GameObject.Find("TownDecor");
                Destroy(target);
            }
        }
    }

    public float getHealth()
    {
        return healthyHouses;
    }
    bool isValidTile(Vector3 pos)
    {
        foreach (Tilemap tilemap in invalidSpawnTiles)
        {
            Vector3Int cellPos = tilemap.WorldToCell(pos);
            TileBase tileAtPos = tilemap.GetTile(cellPos);
            if ((tilemap.name == "bg water" || tilemap.name == "water") && tileAtPos != null) { return false; }
        }
        return true;
    }
}
