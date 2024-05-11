using System.Collections;
using Code.Scripts.MainMenu;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.Video;
using Zenject;

public class MainMenuView : MonoBehaviour
{
    private MainMenuController _controller;
    [SerializeField]
    private GameObject settingsMenu;
    [SerializeField] private VideoPlayer player;

    [Inject]
    public void Construct(MainMenuController mainMenuController)
    {
        _controller = mainMenuController;
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