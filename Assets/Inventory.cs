using UnityEngine;

public class Inventory : MonoBehaviour
{
    private int seedsStoring;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        seedsStoring = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addSeeds(int toAdd)
    {
        seedsStoring += toAdd;
        Debug.Log("Seeds: " + seedsStoring);
    }
}
