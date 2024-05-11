using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ColliderDamage : MonoBehaviour
{
    [SerializeField] float _damagePerSecond;
    [SerializeField] float _delay;
    [SerializeField] bool _damageToEnemies;
    private HealthComponent _healthComponent;

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            _healthComponent = coll.gameObject.GetComponent<HealthComponent>();
            HealthComponent healthComponent = _healthComponent;
            StartCoroutine(damageEverySecond(healthComponent));
        }
        else if (coll.gameObject.CompareTag("Enemy") && _damageToEnemies)
        {
            _healthComponent = coll.gameObject.GetComponent<HealthComponent>();
            HealthComponent healthComponent = _healthComponent;
            StartCoroutine(damageEverySecond(healthComponent));
        }
    }

    private void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.CompareTag("Player") || coll.gameObject.CompareTag("Enemy"))
        {
            StopAllCoroutines();
        }
    }

    IEnumerator damageEverySecond(HealthComponent component)
    {
        while (true)
        {
            component.ApplyDamage(_damagePerSecond);
            yield return new WaitForSecondsRealtime(_delay);
        }
    }
}
