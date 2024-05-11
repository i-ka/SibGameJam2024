using System.Collections;
using System.Collections.Generic;
using Code.Scripts.Weapon.RocketLauncher;
using UnityEngine;

public class RocketLauncherShoot : MonoBehaviour
{
    [SerializeField]
    private RocketLauncher _rocketLauncher;

    //this is jetpack!!!
    [SerializeField] private RocketGun _rocketGun;

    private void OnFire()
    {
        _rocketLauncher.Shoot();
    }

    private void OnReload()
    {
        _rocketLauncher.Reload();
    }

    public void ShootJetpack()
    {
        _rocketGun.shoot();
    }
}
