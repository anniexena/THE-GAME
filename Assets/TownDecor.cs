using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using UnityEngine.U2D;

public class TownDecor : MonoBehaviour
{
    public Sprite[] possibleDecors;
    private int decorIndex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        decorIndex = Random.Range(0, possibleDecors.Length);
        gameObject.GetComponent<SpriteRenderer>().sprite = possibleDecors[decorIndex];

        if (decorIndex == 4)
        {
            gameObject.transform.localScale = new Vector3(4, 4, 1);
        }

        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Vector2 spriteSize = sprite.rect.size / sprite.pixelsPerUnit;

        BoxCollider2D triggerCollider = gameObject.AddComponent<BoxCollider2D>();
        triggerCollider.size = spriteSize; // Match sprite size
        triggerCollider.offset = sprite.bounds.center; // Center the collider
        triggerCollider.isTrigger = true;

        BoxCollider2D hitboxCollider = gameObject.AddComponent<BoxCollider2D>();
        hitboxCollider.size = new Vector2(spriteSize.x / 2, spriteSize.y / 2); // Match sprite size
        hitboxCollider.offset = new Vector2(sprite.bounds.center.x, -spriteSize.y / 4); // Center the collider


        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * -100);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
