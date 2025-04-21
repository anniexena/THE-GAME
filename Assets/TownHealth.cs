using UnityEngine;
using UnityEditor.Rendering;

public class TownHealth : MonoBehaviour
{
    private float healthyHouses;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthyHouses = GameObject.FindGameObjectsWithTag("House").Length;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] houses = GameObject.FindGameObjectsWithTag("House");
        for (int i = 0; i < houses.Length; i++)
        {
            House house = houses[i].GetComponent<House>();
            if (house.getFixesNeeded() == 2) { healthyHouses--; }
            else if (house.getFixesNeeded() == 1) { healthyHouses -= 0.5f; }
        }

    }
    public float getHealth()
    {
        return healthyHouses;
    }
}
