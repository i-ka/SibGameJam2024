using System.Collections;
using System.Collections.Generic;
using Code.Scripts.Weapon.RocketLauncher;
using UnityEngine;

public class RocketLauncherShoot : MonoBehaviour
{
    [SerializeField]
    private RocketLauncher _rocketLauncher;

    private void OnFire()
    {
        _rocketLauncher.Shoot();
    }

    private void OnReload()
    {
        _rocketLauncher.Reload();
    }
}
