using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class Inventory : MonoBehaviour
{
    public GameObject BirchSeed;
    public GameObject PineSeed;
    public GameObject CherrySeed;

    public Camera cam;

    private Dictionary<string, int> seedsStoring;
    private string seedIndex;
    private Dictionary<string, int> woodStoring;
    private string woodIndex;

    private Dictionary<string, GameObject> seedToSpawn;

    private bool alreadyFixing = false;
    public AudioClip fixAudio;
    public AudioClip pickupAudio;

    private Tilemap[] invalidSpawnTiles; // Invalid spawn tiles


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        seedsStoring = new Dictionary<string, int>();
        woodStoring = new Dictionary<string, int>();
        woodIndex = "Birch";
        seedIndex = "Birch";

        seedToSpawn = new Dictionary<string, GameObject>();
        seedToSpawn["Birch"] = BirchSeed;
        seedToSpawn["Pine"] = PineSeed;
        seedToSpawn["Cherry"] = CherrySeed;

        // Starting values
        woodStoring["Birch"] = 50;
        woodStoring["Cherry"] = 50;
        woodStoring["Pine"] = 50;
        seedsStoring["Birch"] = 50;
        seedsStoring["Cherry"] = 50;
        seedsStoring["Pine"] = 50;


        InvalidSpawnTiles invalidSpawns = GameObject.Find("InvalidSpawnTiles").GetComponent<InvalidSpawnTiles>();
        invalidSpawnTiles = invalidSpawns.invalidSpawnTiles;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;

        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
        worldPos.z = 0f;

        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
        if (hit.collider == null)
        {
            if (Input.GetMouseButtonDown(1) && isValidTile(worldPos) && getSeeds(seedIndex) > 0)
            {
                print("we should be able to plant seeds");
                GameObject newSeed = Instantiate(seedToSpawn[seedIndex], worldPos, Quaternion.identity);
                seedsStoring[seedIndex] -= 1;
            }
        }
        else if (hit.collider.gameObject.tag == "House")
        {
            House house = hit.collider.GetComponent<House>();
            if (Input.GetMouseButtonDown(0) && !alreadyFixing) {

                // if (house.woodType == woodIndex && house.getFixesNeeded() > 0)
                if (house.getWoodType() == woodIndex && house.getFixesNeeded() > 0 && getWood(woodIndex) > house.getCost())
                {
                    alreadyFixing = true;
                    Debug.Log(woodIndex);
                    woodStoring[woodIndex] -= house.getCost();
                    StartCoroutine(fixHouse(house));
                }
            }
        }
    }

    private IEnumerator fixHouse(House house)
    {
        yield return StartCoroutine(SFXManager.instance.PlaySFXClipAndWait(fixAudio, transform, 1f));
        house.fix();
        alreadyFixing = false;
    }

    public int getWood(string woodType)
    {
        if (!woodStoring.ContainsKey(woodType))
        {
            return 0;
        }
        return woodStoring[woodType];
    }

    public int getSeeds(string seedType)
    {
        if (!seedsStoring.ContainsKey(seedType))
        {
            return 0;
        }
        return seedsStoring[seedType];
    }

    public void addSeeds(string seedType, int toAdd)
    {
        SFXManager.instance.PlaySFXClip(pickupAudio, transform, 1f);
        if (!seedsStoring.ContainsKey(seedType))
        {
            seedsStoring[seedType] = toAdd;
        }
        else
        {
            seedsStoring[seedType] += toAdd;
        }
    }

    public void addWood(string woodType, int toAdd)
    {
        SFXManager.instance.PlaySFXClip(pickupAudio, transform, 1f);
        if (!woodStoring.ContainsKey(woodType))
        {
            woodStoring[woodType] = toAdd;
        }
        else
        {
            woodStoring[woodType] += toAdd;
        }
    }

    public void SelectSeedIndex(string seedType)
    {
        if (seedToSpawn.ContainsKey(seedType))
        {
            seedIndex = seedType;
            Debug.Log("Selected Seed: " + seedIndex);
        }
    }

    public void SelectWoodIndex(string woodType)
    {
        if (woodStoring.ContainsKey(woodType))
        {
            woodIndex = woodType;
            Debug.Log("Selected Wood: " + woodIndex);
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
