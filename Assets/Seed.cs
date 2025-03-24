using UnityEngine;

public class Seed : MonoBehaviour
{
    private float growTimer = 0;
    public float growWait = 3.5f;
    [SerializeField] Sprite[] treeSprites;
    private int spriteIndex;
    private bool growTimerStart = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (growTimerStart)
        {
            growTimer += Time.deltaTime;
            if (growTimer > growWait && spriteIndex < treeSprites.Length)
            {
                Debug.Log("Plant growing up!");
                gameObject.GetComponent<SpriteRenderer>().sprite = treeSprites[spriteIndex++];
                growTimer = 0;
            }
        }
    }
}
