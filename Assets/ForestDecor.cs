using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using UnityEngine.U2D;

public class ForestDecor : MonoBehaviour
{
    public Sprite[] possibleDecors;
    private int decorIndex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        decorIndex = Random.Range(0, possibleDecors.Length);
        gameObject.GetComponent<SpriteRenderer>().sprite = possibleDecors[decorIndex];

        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Vector2 spriteSize = sprite.rect.size / sprite.pixelsPerUnit;
        BoxCollider2D triggerCollider = gameObject.AddComponent<BoxCollider2D>();
        triggerCollider.size = spriteSize; // Match sprite size
        triggerCollider.offset = sprite.bounds.center; // Center the collider
        triggerCollider.isTrigger = true;

        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * -100);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
