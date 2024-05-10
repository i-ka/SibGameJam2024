using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnimationController : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject logo;
    
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnIntroAnimationEnd()
    {
        mainMenu.SetActive(true);
        logo.SetActive(false);
        _animator.enabled = false;
    }
}
