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
    }

    // Update is called once per frame
    void Update()
    {
        inventoryText.text = "Birch = (Seeds x " + inventory.getSeeds("Birch") + ", Wood x " + inventory.getWood("Birch") + ")";
        inventoryText.text += "\nPines = (Seeds x " + inventory.getSeeds("Pine") + ", Wood x " + inventory.getWood("Pine") + ")";
        inventoryText.text += "\nCherries = (Seeds x " + inventory.getSeeds("Cherry") + ", Wood x " + inventory.getWood("Cherry") + ")";
    }
}
