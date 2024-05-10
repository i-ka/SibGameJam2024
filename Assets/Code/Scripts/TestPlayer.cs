using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts;
using Code.Scripts.MainMenu;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(HealthComponent))]
public class TestPlayer : MonoBehaviour
{
    [field:SerializeField]
    public HealthComponent HealthComponent { get; private set; }

    private GameFlowController _gameFlow;

    [Inject]
    public void Construct(GameFlowController flowController)
    {
        _gameFlow = flowController;
    }
    
    private void Awake()
    {
        HealthComponent = GetComponent<HealthComponent>();
    }

    public void OnDead()
    {
        Debug.Log("Player is dead");
    }
}
