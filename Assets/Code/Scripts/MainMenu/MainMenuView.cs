using Code.Scripts.MainMenu;
using UnityEngine;
using Zenject;

public class MainMenuView : MonoBehaviour
{
    private MainMenuController _controller;

    [Inject]
    public void Construct(MainMenuController mainMenuController)
    {
        _controller = mainMenuController;
    }

    public void OnGameStartButton()
    {
        _controller.StartGame();
    }

    public void OnExitGame()
    {
        _controller.ExitGame();
    }
}