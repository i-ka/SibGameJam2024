using UnityEngine;

namespace Code.Scripts.MainMenu
{
    public class SettingsController
    {
        public void SetMasterVolume(float masterVolume)
        {
            AudioListener.volume = masterVolume;
        }
    }
}