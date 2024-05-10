using UnityEngine;
using Zenject;

namespace Code.Scripts.MainMenu
{
    public class SettingsMenuView: MonoBehaviour
    {
        private SettingsController _controller;
        
        [Inject]
        public void Construct(SettingsController controller)
        {
            _controller = controller;
        }

        public void OnMasterVolumeChanged(float value)
        {
            _controller.SetMasterVolume(value);
        }

        public void OnBackButtonPressed()
        {
            gameObject.SetActive(false);
        }
    }
}