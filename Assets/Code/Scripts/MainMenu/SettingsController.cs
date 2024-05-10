using System;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Scripts.MainMenu
{
    public class SettingsController
    {
        public float MasterVolume
        {
            get => AudioListener.volume;
            set => AudioListener.volume = value;
        }
    }
}