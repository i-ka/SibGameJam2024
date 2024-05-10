using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class TestPlayer : MonoBehaviour
{
    [field:SerializeField]
    public HealthComponent HealthComponent { get; private set; }
    
    private void Awake()
    {
        HealthComponent = GetComponent<HealthComponent>();
    }

    public void OnDead()
    {
        Debug.Log("Player is dead");
    }
}
