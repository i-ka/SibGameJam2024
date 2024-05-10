using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Code.Scripts.MainMenu
{
    public class SettingsMenuView: MonoBehaviour
    {
        private SettingsController _controller;
        [SerializeField]
        private GameObject mainMenu;

        [SerializeField] private Slider masterVolumeSlider;
        
        [Inject]
        public void Construct(SettingsController controller)
        {
            _controller = controller;
        }

        public void OnMasterVolumeChanged(float value)
        {
            _controller.MasterVolume = value;
        }

        public void OnBackButtonPressed()
        {
            gameObject.SetActive(false);
        }

        public void OnSaveButtonPressed()
        {
            mainMenu.SetActive(true);
            gameObject.SetActive(false);
        }

        private void Update()
        {
            masterVolumeSlider.value = _controller.MasterVolume;
        }
    }
}