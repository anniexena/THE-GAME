using UnityEngine;

public class Edge : MonoBehaviour
{
    public int startID;
    public int endID;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Comment in start as a test
    }

    // Update is called once per frame
    public Edge(int startID, int endID) {
        this.startID = startID;
        this.endID = endID;
    }
}
