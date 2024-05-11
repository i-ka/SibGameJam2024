using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts;
using Code.Scripts.MainMenu;
using UnityEngine;
using Zenject;

public class InGameMenuView : MonoBehaviour
{
    [SerializeField]
    private GameObject settingsMenu;
    [SerializeField] private GameObject inGameInterface;
    [SerializeField] private GameObject winInterface;
    [SerializeField] private GameObject looseInterface;

    private InGameMenuController _controller;
    private GameStateController _gameStateController;

    [Inject]
    private void Construct(InGameMenuController controller, GameStateController gameStateController)
    {
        _controller = controller;
        _controller.GameMenuShown += OnOpenInGameMenu;
        _controller.GameMenuHidden += OnCloseGameMenu;

        _gameStateController = gameStateController;
        _gameStateController.GameOver += ShowGameOverMenu;
        _gameStateController.GameFinished += ShowGameOverMenu;

    }
    
    public void OnSettingsButton()
    {
        settingsMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnResumeButton()
    {
        _controller.IsGameMenuShown = false;
    }

    public void OnGoToMainMenuButton()
    {
        _controller.ToMainMenu();
    }

    private void OnOpenInGameMenu()
    {
        gameObject.SetActive(true);
        inGameInterface.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnCloseGameMenu()
    {
        gameObject.SetActive(false);
        inGameInterface.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void ShowGameOverMenu()
    {
        inGameInterface.SetActive(false);
        looseInterface.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
}
