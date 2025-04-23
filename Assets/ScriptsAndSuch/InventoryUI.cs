using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory; 
    [SerializeField] private GameObject inventoryMenu;
    [SerializeField] private TextMeshProUGUI birchSeeds;
    [SerializeField] private TextMeshProUGUI birchLogs;
    [SerializeField] private TextMeshProUGUI pineSeeds;
    [SerializeField] private TextMeshProUGUI pineLogs;
    [SerializeField] private TextMeshProUGUI cherrySeeds;
    [SerializeField] private TextMeshProUGUI cherryLogs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Check for user input to open inventory
        if (Input.GetKeyDown(KeyCode.I)) {
            if (inventoryMenu.activeInHierarchy) {
                CloseMenu();
            }
            else {
                OpenMenu();
            }
        }
        setInventory();
    }

    public void OpenMenu() {
        inventoryMenu.SetActive(true);
    }

    public void CloseMenu() {
        inventoryMenu.SetActive(false);
    }

    private void setInventory() {
        birchSeeds.text = inventory.getSeeds("Birch") + "";
        birchLogs.text = inventory.getWood("Birch") + "";
        pineSeeds.text = inventory.getSeeds("Pine") + "";
        pineLogs.text = inventory.getWood("Pine") + "";
        cherrySeeds.text = inventory.getSeeds("Cherry") + "";
        cherryLogs.text = inventory.getWood("Cherry") + "";
    }
    public void testMenu() {
        Debug.Log("Clicked close");
    }
}
