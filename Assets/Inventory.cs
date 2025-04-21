using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject BirchSeed;
    public GameObject PineSeed;
    public GameObject CherrySeed;

    public Camera cam;

    private const int HOUSE_COST = 5;
    private Dictionary<string, int> seedsStoring;
    private string seedIndex;
    private Dictionary<string, int> woodStoring;
    private string woodIndex;

    private Dictionary<string, GameObject> seedToSpawn;

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
    }

    // Update is called once per frame
    void Update()
    {
        // TEMPORARY SELECTION
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            seedIndex = "Birch";
            print("Curr Seeds: " + seedIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            seedIndex = "Pine";
            print("Curr Seeds: " + seedIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            seedIndex = "Cherry";
            print("Curr Seeds: " + seedIndex);
        }

        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            woodIndex = "Birch";
            print("Curr Wood: " + woodIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            woodIndex = "Pine";
            print("Curr Wood: " + woodIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            woodIndex = "Cherry";
            print("Curr Wood: " + woodIndex);
        }


        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f; // Set this to the distance from the camera

        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
        worldPos.z = 0f; // Set Z to 0 if you're working in 2D

        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
        if (hit.collider == null)
        {
            if (Input.GetMouseButtonDown(1) && getSeeds(seedIndex) > 0)
            {
                GameObject newSeed = Instantiate(seedToSpawn[seedIndex], worldPos, Quaternion.identity);
                seedsStoring[seedIndex] -= 1;
            }

            // Spawns a house
            //if (Input.GetKeyDown(KeyCode.Space) && woodStoring > HOUSE_COST)
            //{
            //    Instantiate(house, worldPos, Quaternion.identity);
            //    woodStoring -= HOUSE_COST;
            //    Debug.Log("Wood: " + woodStoring);
            //}
        }
        else if (hit.collider.gameObject.tag == "House")
        {
            if (Input.GetMouseButtonDown(0) && getWood(woodIndex) > HOUSE_COST)
            {
                House house = hit.collider.GetComponent<House>();
                if (house.woodType == woodIndex)
                {
                    woodStoring[woodIndex] -= HOUSE_COST;
                    house.fix();
                }
            }
        }

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
        if (!woodStoring.ContainsKey(woodType))
        {
            woodStoring[woodType] = toAdd;
        }
        else
        {
            woodStoring[woodType] += toAdd;
        }
    }
}
