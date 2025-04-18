using UnityEngine;

public class TextField : MonoBehaviour
{
    public int randomInt;
    public bool satisfied;
    public int numActive;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void CheckSatisfied() {
        satisfied = (numActive == randomInt);
    }
}
