using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Scripts.MainMenu
{
    public class PlayerReloadView: MonoBehaviour
    {
        [SerializeField] private GameObject[] rockets;
        [SerializeField] private Animator reloadAnimator;
        [SerializeField] private Image launcherImage;
        private Sprite _defaultSprite;

        private void Awake()
        {
            _defaultSprite = launcherImage.sprite;
            reloadAnimator.enabled = false;
        }

        public void OnAmmoCountChanged(int ammoCount)
        {
            foreach (var (i, r) in rockets.Select((r, i) => (i, r)))
            {
                r.SetActive(i < ammoCount);
            }
        }

        public void OnReloadStart()
        {
            reloadAnimator.enabled = true;
            //reloadAnimator.StartPlayback();
        }

        public void OnReloadEnd()
        {
            //reloadAnimator.StopPlayback();
            reloadAnimator.enabled = false;
            launcherImage.sprite = _defaultSprite;
        }
    }
}