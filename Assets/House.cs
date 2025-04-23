using UnityEngine;
using UnityEngine.VFX;

public class House : MonoBehaviour
{
    public float fixWaitLow; // Lowest time before house needs to be fixed
    public float fixWaitHigh; // Highest time before house needs to be fixed
    public Sprite[] houseStates;
    public string woodType;
    public int cost;

    private float fixTimer = 0; // Timer for fixing
    private int fixesNeeded; // 0 -> least broken/no fixing necessary
    private float fixWait;
    private const int MAXSTATES = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fixesNeeded = 1;
        GetComponent<SpriteRenderer>().sprite = houseStates[fixesNeeded];
        fixWait = Random.Range(fixWaitLow, fixWaitHigh);
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * -100);
    }

    // Update is called once per frame
    void Update()
    {
        fixTimer += Time.deltaTime;
        if (fixTimer > fixWait)
        {
            fixTimer = 0;
            fixesNeeded = Mathf.Min(MAXSTATES - 1, fixesNeeded + 1);
            GetComponent<SpriteRenderer>().sprite = houseStates[fixesNeeded];
        }
    }

    public void fix()
    {
        if (fixesNeeded > 0)
        {
            fixWait = Random.Range(fixWaitLow, fixWaitHigh);
            fixTimer = 0;
            fixesNeeded = Mathf.Max(0, fixesNeeded - 1);
            GetComponent<SpriteRenderer>().sprite = houseStates[fixesNeeded];
        }
    }

    public int getFixesNeeded()
    {
        return fixesNeeded;
    }

    public int getCost()
    {
        return cost;
    }
}
