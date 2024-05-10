using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _maxDamage;

    [SerializeField] private GameObject _blast;
    private bool _exploded;

    private void Update()
    {
        transform.Translate(transform.up * _speed * Time.deltaTime, transform);
    }
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Enemy"))
        {
            //получение макс урона врагом
        }

        if (!_exploded)
        {
            Instantiate(_blast, transform.position + transform.right, Quaternion.identity);
            _exploded = true;
        }
        Destroy(this.gameObject);
    }
}