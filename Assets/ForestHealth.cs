using UnityEngine;
using UnityEditor.Rendering;

public class ForestHealth : MonoBehaviour
{
    private int health;

    private bool changeTimerStart = false;
    private float changeTimer = 0;
    private int changeRate = 3; // How quickly to be adding/removing animal
    private const int TERMINAL = 5;

    public GameObject[] Animals;

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
                revise(animalsActual, animalsExpected);
            }
        }
    }


    void revise(int actual, int expected)
    {
        if (actual < expected)
        {
            int animal = Random.Range(0, Animals.Length);
            Instantiate(Animals[animal], new Vector3(Random.Range(-250, 250), Random.Range(-10, 10), 0), Quaternion.identity);
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
}
