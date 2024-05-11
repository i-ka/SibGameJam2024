using System.Collections;
using System.Collections.Generic;
using Code.Scripts.Weapon.RocketLauncher;
using UnityEngine;
using UnityEngine.VFX;

public class RocketLauncherShoot : MonoBehaviour
{
    [SerializeField]
    private RocketLauncher _rocketLauncher;

    //this is jetpack!!!
    [SerializeField] private RocketGun _rocketGun;
    [SerializeField] private VisualEffect _jetpackEffect;

    private void OnFire()
    {
        if (!enabled) return;
        _rocketLauncher.Shoot();
    }

    private void OnReload()
    {
        Debug.Log("Reload pressed");
        if (!enabled) return;
        _rocketLauncher.Reload();
    }

    public void ShootJetpack()
    {
        if (!enabled) return;
        _rocketGun.shoot();
    }
}
