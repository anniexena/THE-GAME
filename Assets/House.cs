using UnityEngine;
using UnityEngine.VFX;

public class House : MonoBehaviour
{
    private float fixTimer = 0; // Timer for fixing
    public float fixWait; // Time before it needs to be fixed
    public bool needsFixing = false;
    public Sprite broken;
    public Sprite normal;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fixWait = Random.Range(10, 30);
    }

    // Update is called once per frame
    void Update()
    {
        if (!needsFixing)
        {
            fixTimer += Time.deltaTime;
            if (fixTimer > fixWait)
            {
                fixTimer = 0;
                needsFixing = true;
                GetComponent<SpriteRenderer>().sprite = broken;
            }
        }
    }

    public void fix()
    {
        if (needsFixing)
        {
            fixWait = Random.Range(10, 30);
            fixTimer = 0;
            needsFixing = false;
            GetComponent<SpriteRenderer>().sprite = normal;
        }
    }
}
