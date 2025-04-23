using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private GameObject startMenu;
    private void Start()
    {
        startMenu.SetActive(true);
    }
    public void CloseStartMenu()
    {
        startMenu.SetActive(false);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
