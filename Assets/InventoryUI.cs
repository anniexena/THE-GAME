using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public TextMeshProUGUI inventoryText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        print(inventory);
    }

    // Update is called once per frame
    void Update()
    {
        print(inventoryText);
        inventoryText.text = "SEEDS = " + inventory.getSeeds() + 
            "   WOOD = " + inventory.getWood();
    }
}
