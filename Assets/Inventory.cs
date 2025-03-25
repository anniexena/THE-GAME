using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject seed;
    public Camera cam;
    private int seedsStoring;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        seedsStoring = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && seedsStoring > 0) {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10f; // Set this to the distance from the camera

            Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
            worldPos.z = 0f; // Set Z to 0 if you're working in 2D

            Instantiate(seed, worldPos, Quaternion.identity);

            seedsStoring--;
            Debug.Log("Seeds: " + seedsStoring);
        }
    }

    public void addSeeds(int toAdd)
    {
        seedsStoring += toAdd;
        Debug.Log("Seeds: " + seedsStoring);
    }
}
