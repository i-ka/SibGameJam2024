using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class GameOverView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    private GameStateController _gameStateController;
    private GameGoalSettings _gameGoalSettings;

    [Inject]
    public void Construct(GameStateController gameStateController, GameGoalSettings gameGoalSettings)
    {
        _gameStateController = gameStateController;
        _gameGoalSettings = gameGoalSettings;
    }

    private void OnEnable()
    {
        text.text = _gameStateController.IsWin
            ? $"Поздравляем вы прошли игру! Вы убили {_gameStateController.DestroyedEnemies} паразитов"
            : $"Игра окончена. Вы смогли уничтожить только {_gameStateController.DestroyedHives} ульев из {_gameGoalSettings.CountHivesToDestroy}";
    }

    public void OnRestart()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void OnGoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
