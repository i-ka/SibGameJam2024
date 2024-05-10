using Code.Scripts.MainMenu;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class MainMenuView : MonoBehaviour
{
    private MainMenuController _controller;
    [SerializeField]
    private GameObject settingsMenu;

    [Inject]
    public void Construct(MainMenuController mainMenuController)
    {
        _controller = mainMenuController;
    }

    public void OnGameStartButton()
    {
        _controller.StartGame();
    }

    public void OnGameSettings()
    {
        settingsMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnExitGame()
    {
        _controller.ExitGame();
    }
}