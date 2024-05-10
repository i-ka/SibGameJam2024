using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts.MainMenu;
using UnityEngine;
using Zenject;

public class InGameMenuView : MonoBehaviour
{
    [SerializeField]
    private GameObject settingsMenu;

    private InGameMenuController _controller;

    [Inject]
    private void Construct(InGameMenuController controller)
    {
        _controller = controller;
        _controller.GameMenuShown += OnOpenInGameMenuShown;
        _controller.GameMenuHidden += OnCloseGameMenu;
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

    public void OnOpenInGameMenuShown() => gameObject.SetActive(true);
    public void OnCloseGameMenu() => gameObject.SetActive(false);
}
