using Code.Scripts.MainMenu;
using UnityEngine;
using Zenject;

public class MainMenuView : MonoBehaviour
{
    private MainMenuController _controller;
    private SettingsMenuView _settingsMenuView;


    [Inject]
    public void Construct(MainMenuController mainMenuController, SettingsMenuView settingsMenuView)
    {
        _controller = mainMenuController;
        _settingsMenuView = settingsMenuView;
    }

    public void OnGameStartButton()
    {
        _controller.StartGame();
    }

    public void OnGameSettings()
    {
        _settingsMenuView.gameObject.SetActive(true);
    }

    public void OnExitGame()
    {
        _controller.ExitGame();
    }
}