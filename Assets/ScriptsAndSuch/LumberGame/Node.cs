using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private Sprite inactiveSprite;
    private SpriteRenderer spriteRenderer;
    private bool isActive = false;
    [SerializeField] public int id; // unique id for node

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start() {
        isActive = false;
        spriteRenderer.sprite = inactiveSprite;
    }
    // On click, set as active/not active
    void OnMouseDown() {
        isActive = !isActive;
        spriteRenderer.sprite = isActive ? activeSprite : inactiveSprite;
    }


}
