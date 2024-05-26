using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using Debug = UnityEngine.Debug;

namespace Code.Scripts.Weapon
{
    public class RocketLauncher : MonoBehaviour
    {
        //[SerializeField] private Animator _playerAnim;

        [SerializeField] private float _reloadTime = 1;
        [SerializeField] private int _ammoCount;
        [SerializeField] private int _ammo;
        [SerializeField] private float _delay;
        [SerializeField] private float _shootCooldown;
        

        private bool _isReloadInProgress = false;

        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _bulletPoint;

        [SerializeField] private UnityEvent<int> AmmoCountChanged;
        [SerializeField] private UnityEvent RealodStarted;
        [SerializeField] private UnityEvent ReloadEnded;

        [SerializeField] private AudioClip shootSound;
        [SerializeField] private AudioClip reloladSound;
        

        private void Start()
        {
            _ammo = _ammoCount;
            AmmoCountChanged?.Invoke(_ammo);
        }

        private void Update()
        {
            if (_shootCooldown > 0)
                _shootCooldown -= Time.deltaTime;
        }

        public void Shoot()
        {
            if(!_isReloadInProgress && _shootCooldown <= 0 && _ammo > 0)
            {
                Instantiate(_bulletPrefab, _bulletPoint.position, _bulletPoint.rotation);
                _shootCooldown = _delay;
                _ammo--;
                if (shootSound)
                    AudioSource.PlayClipAtPoint(shootSound, transform.position);
                AmmoCountChanged?.Invoke(_ammo);
            }
        
        }
        
        

        public void Reload()
        {
            StartCoroutine(ReloadInner());
        }

        public IEnumerator ReloadInner()
        {
            Debug.Log("Start reload");
            if (_isReloadInProgress)
            {
                yield break;
            }
            _isReloadInProgress = true;
            RealodStarted?.Invoke();
            
            while (_ammo < _ammoCount)
            {
                _ammo++;
                AmmoCountChanged?.Invoke(_ammo);
                if (reloladSound)
                    AudioSource.PlayClipAtPoint(reloladSound, transform.position);
                yield return new WaitForSeconds(1.0f);
            }
            ReloadEnded?.Invoke();
            _isReloadInProgress = false;
        }
    }
}