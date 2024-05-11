using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HealthSlider : MonoBehaviour
{
    [SerializeField]
    private Gradient healthColorGradient;

    [SerializeField] private Image fillImage;

    private Slider _slider;
    private TestPlayer _health;

    [Inject]
    public void Construct(TestPlayer player)
    {
        _health = player;
    }

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Update()
    {
        if (!_health.HealthComponent)
            return;

        var currentValue = _health.HealthComponent.CurrentHealth / _health.HealthComponent.MaxHealth;
        _slider.value = Mathf.Lerp(_slider.value, currentValue, 0.5f);
        fillImage.color = healthColorGradient.Evaluate(_slider.value);
    }
}
