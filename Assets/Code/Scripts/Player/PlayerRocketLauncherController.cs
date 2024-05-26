using Code.Scripts.Weapon;
using UnityEngine;

namespace Code.Scripts.Player
{
    public class PlayerRocketLauncherController : MonoBehaviour
    {
        [SerializeField]
        private RocketLauncher launcher;
   
        public void OnFire()
        {
            launcher.Shoot();
        }
    
        public void Reload()
        {
            launcher.Reload();
        }
    }
}
