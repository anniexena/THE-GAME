using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject howToMenu;
    [SerializeField] private GameObject NatureTab;
    [SerializeField] private GameObject TownTab;
    [SerializeField] private GameObject BalanceTab;

    private void Start()
    {
        startMenu.SetActive(true);
        howToMenu.SetActive(false);
        TownTab.SetActive(false);
        BalanceTab.SetActive(false);
    }
    public void CloseStartMenu()
    {
        startMenu.SetActive(false);
    }

    public void OpenHowTo() {
        howToMenu.SetActive(true);
    }

    public void OpenNatureTab()
    {
        NatureTab.SetActive(true);
        TownTab.SetActive(false);
        BalanceTab.SetActive(false);
    }

    public void OpenTownTab()
    {
        NatureTab.SetActive(false);
        TownTab.SetActive(true);
        BalanceTab.SetActive(false);
    }

    public void OpenBalanceTab()
    {
        NatureTab.SetActive(false);
        TownTab.SetActive(false);
        BalanceTab.SetActive(true);
    }

    public void CloseHowTo() {
        howToMenu.SetActive(false);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
