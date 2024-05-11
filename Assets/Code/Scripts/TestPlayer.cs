using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class TestPlayer : MonoBehaviour
{
    public HealthComponent HealthComponent { get; private set; }

    private Animator _animator;
    private ThirdPersonController _controller;
    private RocketLauncherShoot _rocketLauncher;
    private static readonly int Death = Animator.StringToHash("Death");

    private void Awake()
    {
        HealthComponent = GetComponent<HealthComponent>();
        _animator = GetComponent<Animator>();
        _controller = GetComponent<ThirdPersonController>();
        _rocketLauncher = GetComponent<RocketLauncherShoot>();
    }

    public void OnDead()
    {
        _animator.SetTrigger(Death);
        _controller.DisableMove = true;
        _rocketLauncher.enabled = false;
    }
}
