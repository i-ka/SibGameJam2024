using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;

    [SerializeField] private UnityEvent<float> healthChanged;
    [SerializeField] private UnityEvent died;

    public void Awake()
    {
        currentHealth = maxHealth;
    }

    public void ApplyDamage(float damage)
    {
        if (currentHealth <= 0)
            return;

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth) ;
        healthChanged?.Invoke(currentHealth);
        if (currentHealth <= 0)
        {
            died?.Invoke();
            
        }
    }

}
