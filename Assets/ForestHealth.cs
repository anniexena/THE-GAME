using UnityEngine;
using UnityEditor.Rendering;

public class ForestHealth : MonoBehaviour
{
    private int health;

    private bool changeTimerStart = false;
    private float changeTimer = 0;
    private int changeRate = 3; // How quickly to be adding/removing animal

    public GameObject animal;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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
            if (plant.getPhase() < 5)
            {
                healthyPlants++;
            }
        }

        int animalsExpected = Mathf.FloorToInt(healthyPlants * 2.5f); // 3 houses = 7 animal

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
                revise(animalsActual, animalsExpected);
                //print("Healthy plants present: " + healthyPlants);
                //print("Animals present: " + animalsActual);
            }
        }
    }


    void revise(int actual, int expected)
    {
        if (actual < expected)
        {
            Instantiate(animal, new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0), Quaternion.identity);
        }
        else
        {
            GameObject target = GameObject.Find("Animal");
            Destroy(target);
        }

    }
}
