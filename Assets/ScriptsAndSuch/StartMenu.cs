using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject howToMenu;
    private void Start()
    {
        startMenu.SetActive(true);
        howToMenu.SetActive(false);
    }
    public void CloseStartMenu()
    {
        startMenu.SetActive(false);
    }

    public void OpenHowTo() {
        howToMenu.SetActive(true);
    }

    public void CloseHowTo() {
        howToMenu.SetActive(false);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
