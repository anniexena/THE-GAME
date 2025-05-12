using UnityEngine;

public class NPC : MonoBehaviour
{
    public House house;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * -100);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public int getHouseState()
    {
        return house.getFixesNeeded();
    }

    public int getHouseCost()
    {
        return house.getFixesNeeded() * house.getCost();
    }

    public string getHouseType()
    {
        return house.getWoodType();
    }


}
