using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using Zenject.SpaceFighter;

public class RocketGun : MonoBehaviour
{
    //[SerializeField] private Animator _playerAnim;

    //[SerializeField] private float _reloadTime;
    //private float _reloadTimer;
    [SerializeField] private int _ammoCount;
    private int _ammo;
    [SerializeField] private float _delay;
    private float _shotDelay;

    private bool _gunIsReady = true;

    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletPoint;
    [SerializeField] private Animator _anim;

    private void Start()
    {
        _ammo = _ammoCount;
    }

    private void Update()
    {
        if(_shotDelay < _delay)
        {
            _shotDelay += Time.deltaTime;
        }
        //if (_ammo <= 0)
        //{
        //    _playerAnim.SetTrigger("reload");
        //    _gunIsReady = false;
        //}
        if (Input.GetButtonDown("Fire1"))
        {
            if (_ammo > 0)
                shoot();
        }
    }

    private void shoot()
    {
        if(_gunIsReady && _shotDelay >= _delay)
        {
            Instantiate(_bulletPrefab, _bulletPoint.position, _bulletPoint.rotation);
            _shotDelay = 0;
            _ammo--;
        }
    }

    public void reload()
    {
        _ammo++;
        _gunIsReady = true;
        if(_ammo < _ammoCount)
        {
            _anim.SetTrigger("reload");
        }
    }
}
