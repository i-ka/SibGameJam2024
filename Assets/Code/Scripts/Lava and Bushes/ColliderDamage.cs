using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ColliderDamage : MonoBehaviour
{
    [SerializeField] float _damagePerSecond;
    [SerializeField] float _delay;
    [SerializeField] float _timer = 0f;
    [SerializeField] bool _damageToEnemies;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthComponent component = collision.gameObject.GetComponent<HealthComponent>();
            _timer += Time.deltaTime;
            if(_timer >= _delay)
            {
                component.ApplyDamage(_damagePerSecond);
                _timer = 0f;
            }
        }
        else if(collision.gameObject.CompareTag("Enemy") && _damageToEnemies)
        {
            HealthComponent component = collision.gameObject.GetComponent<HealthComponent>();
            _timer += Time.deltaTime;
            if (_timer >= _delay)
            {
                component.ApplyDamage(_damagePerSecond);
                _timer = 0f;
            }
        }
    }
}
