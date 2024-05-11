using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _maxDamage;
    [SerializeField] private AudioClip impactSound;
    public VisualEffect _blast;

    private Blast _blastLogic;
    private Rigidbody _rigidbody;
    private bool _exploded;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _blastLogic = GetComponent<Blast>();
        _rigidbody.velocity = -transform.forward * _speed;
    }


    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Enemy"))
        {
            //получение макс урона врагом
        }

        if (!_exploded && !coll.gameObject.CompareTag("Player"))
        {
            //Instantiate(_blast, transform.position + -transform.forward, Quaternion.identity);
            _exploded = true;
            _blast.SendEvent("OnHit");
            _rigidbody.velocity = new Vector3(0, 0, 0);
            if (impactSound)
                AudioSource.PlayClipAtPoint(impactSound, transform.position);
            _blastLogic.Explode();
        }
    }
}