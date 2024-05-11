using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    [field:SerializeField] public float MaxHealth { get; private set; }
    [field:SerializeField] public float CurrentHealth { get; private set; }

    [SerializeField] private UnityEvent<float> healthChanged;
    [SerializeField] private UnityEvent died;

    public void Awake()
    {
        CurrentHealth = MaxHealth;
        healthChanged?.Invoke(CurrentHealth / MaxHealth);
    }

    public void ApplyDamage(float damage)
    {
        if (CurrentHealth <= 0)
            return;

        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, MaxHealth) ;
        healthChanged?.Invoke(CurrentHealth / MaxHealth);
        if (CurrentHealth <= 0)
        {
            died?.Invoke();
            
        }
    }

}
