using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MainMenuAnimationController : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject logo;

    [SerializeField] private GameObject bg;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private VideoPlayer player;
    [SerializeField] private string startGameSceneName;
    
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
    
    public void OnGameStartButton()
    {
        audioSource.Stop();
        bg.SetActive(false);
        mainMenu.SetActive(false);
        StartCoroutine(PlayVideo());
    }

    private IEnumerator PlayVideo()
    {
        player.gameObject.SetActive(true);
        player.Play();
        yield return new WaitForSeconds(44.5f);
        player.Stop();
        SceneManager.LoadScene(startGameSceneName);
    }
    
}
