using System.Collections.Generic;
using System.Collections;
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

    private Dictionary<string, int> seedsStoring;
    private string seedIndex;
    private Dictionary<string, int> woodStoring;
    private string woodIndex;

    private Dictionary<string, GameObject> seedToSpawn;

    private bool alreadyFixing = false;
    public AudioClip fixAudio;
    public AudioClip pickupAudio;

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

        woodStoring["Birch"] = 50;
        woodStoring["Cherry"] = 50;
        woodStoring["Pine"] = 50;

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
            House house = hit.collider.GetComponent<House>();
            print("Mouse: " + Input.GetMouseButtonDown(0));
            print("Fixing: " + !alreadyFixing);
            if (Input.GetMouseButtonDown(0) && !alreadyFixing) {
                // if (house.woodType == woodIndex && house.getFixesNeeded() > 0)
                if (house.getFixesNeeded() > 0 && getWood(woodIndex) > house.getCost())
                {
                    alreadyFixing = true;
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
}
