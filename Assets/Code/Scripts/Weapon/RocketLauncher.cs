using System.Collections;
using System.Diagnostics;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using Debug = UnityEngine.Debug;

namespace Code.Scripts.Weapon.RocketLauncher
{
    public class RocketLauncher : MonoBehaviour
    {
        //[SerializeField] private Animator _playerAnim;

        [SerializeField] private float _reloadTime = 1;
        private float _reloadTimer;
        [SerializeField] private int _ammoCount;
        [SerializeField] private int _ammo;
        [SerializeField] private float _delay;
        private float _shotDelay;
        

        private bool _isReloadInProgress = false;

        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _bulletPoint;

        [SerializeField] private UnityEvent<int> AmmoCountChanged;
        [SerializeField] private UnityEvent RealodStarted;
        [SerializeField] private UnityEvent ReloadEnded;
        

        private void Start()
        {
            _ammo = _ammoCount;
            AmmoCountChanged?.Invoke(_ammo);
        }

        private void Update()
        {
            if(_shotDelay < _delay)
            {
                _shotDelay += Time.deltaTime;
            }
        }

        public void Shoot()
        {
            if(!_isReloadInProgress && _shotDelay >= _delay && _ammo > 0)
            {
                Instantiate(_bulletPrefab, _bulletPoint.position, _bulletPoint.rotation);
                _shotDelay = 0;
                _ammo--;
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
                yield return new WaitForSeconds(1.0f);
            }
            ReloadEnded?.Invoke();
            _isReloadInProgress = false;
        }
    }
}