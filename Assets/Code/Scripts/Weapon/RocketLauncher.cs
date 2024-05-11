using UnityEngine;
using UnityEngine.Events;

namespace Code.Scripts.Weapon.RocketLauncher
{
    public class RocketLauncher : MonoBehaviour
    {
        //[SerializeField] private Animator _playerAnim;

        //[SerializeField] private float _reloadTime;
        //private float _reloadTimer;
        [SerializeField] private int _ammoCount;
        [SerializeField] private int _ammo;
        [SerializeField] private float _delay;
        private float _shotDelay;
        

        private bool _gunIsReady = true;

        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _bulletPoint;

        [SerializeField] private UnityEvent<int> AmmoCountChanged;
        

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
            if(_gunIsReady && _shotDelay >= _delay && _ammo > 0)
            {
                Instantiate(_bulletPrefab, _bulletPoint.position, _bulletPoint.rotation);
                _shotDelay = 0;
                _ammo--;
                AmmoCountChanged?.Invoke(_ammo);
            }
        
        }

        public void Reload()
        {
            _ammo++;
            _gunIsReady = true;
            AmmoCountChanged?.Invoke(_ammo);
        }
    }
}