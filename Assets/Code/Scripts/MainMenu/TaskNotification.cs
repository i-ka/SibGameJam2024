using System;
using UnityEngine;

namespace Code.Scripts.MainMenu
{
    public class TaskNotification: MonoBehaviour
    {
        private RectTransform _rectTransform;
        private Animator _animator;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _animator = GetComponent<Animator>();
        }

        private void OnAnimationEnd()
        {
            gameObject.SetActive(false);
            _animator.enabled = false;
        }
    }
}