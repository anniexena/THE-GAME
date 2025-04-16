using UnityEngine;
using UnityEditor.Rendering;

public class TownHealth : MonoBehaviour
{
    private int health;

    private bool changeTimerStart = false;
    private float changeTimer = 0;
    private int changeRate = 3; // How quickly to be adding/removing people

    public GameObject person;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int peopleActual = GameObject.FindGameObjectsWithTag("People").Length;
        int healthyHouses = GameObject.FindGameObjectsWithTag("House").Length;
        int peopleExpected = Mathf.FloorToInt(healthyHouses * 2.5f); // 3 houses = 7 people

        if (!changeTimerStart && peopleActual != peopleExpected)
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
                revise(peopleActual, peopleExpected);
                //print("Houses present: " + healthyHouses);
                //print("People present: " + peopleActual);
            }
        }
    }


    void revise(int actual, int expected)
    {
        if (actual < expected)
        {
            Instantiate(person, new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0), Quaternion.identity);
        }
        else
        {
            GameObject target = GameObject.Find("People");
            Destroy(target);
        }

    }
}
