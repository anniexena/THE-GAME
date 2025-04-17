using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject seed;
    public Camera cam;
    private int seedsStoring;
    private int woodStoring;
    public GameObject house;
    private const int HOUSE_COST = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        seedsStoring = 0;
        woodStoring = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f; // Set this to the distance from the camera

        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
        worldPos.z = 0f; // Set Z to 0 if you're working in 2D

        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
        if (hit.collider == null)
        {
            if (Input.GetMouseButtonDown(1) && seedsStoring > 0)
            {
                Instantiate(seed, worldPos, Quaternion.identity);
                seedsStoring--;
                Debug.Log("Seeds: " + seedsStoring);
            }

            // Spawns a house
            //if (Input.GetKeyDown(KeyCode.Space) && woodStoring > HOUSE_COST)
            //{
            //    Instantiate(house, worldPos, Quaternion.identity);
            //    woodStoring -= HOUSE_COST;
            //    Debug.Log("Wood: " + woodStoring);
            //}
        }
        else
        {
            if (hit.collider.gameObject.tag == "House")
            {
                if (Input.GetMouseButtonDown(0) && woodStoring > HOUSE_COST)
                {
                    House house = hit.collider.GetComponent<House>();
                    woodStoring -= HOUSE_COST;
                    house.fix();
                    Debug.Log("Wood: " + woodStoring);
                }
            }
        }


    }

    public int getWood()
    {
        return woodStoring;
    }
    public int getSeeds()
    {
        return seedsStoring;
    }

    public void addSeeds(int toAdd)
    {
        seedsStoring += toAdd;
        Debug.Log("Seeds: " + seedsStoring);
    }

    public void addWood(int toAdd)
    {
        woodStoring += toAdd;
        Debug.Log("Wood: " + woodStoring);
    }
}
