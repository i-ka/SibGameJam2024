using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Blast : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _force;
    private float _maxDamage;

    private void OnEnable()
    {
        StartCoroutine(Off());
        Explode();
    }

    private void Explode()
    {
        Collider[] overLappedColliders = Physics.OverlapSphere(transform.position, _radius);
        foreach (var collider in overLappedColliders)
        {
            Bullet bullet = collider.GetComponent<Bullet>();
            Rigidbody rigidbody = collider.attachedRigidbody;
            if (rigidbody && !bullet)
            {
                float distance = Vector3.Distance(transform.position, rigidbody.transform.position);
                float force = _force;

                if (distance >= _radius * 0.75f)
                    force *= 0.25f;
                else if (distance >= _radius * 0.5f)
                    force *= 0.6f;
                else if (distance >= _radius * 0.25f)
                    force *= 0.85f;

                rigidbody.AddExplosionForce(force, transform.position, _radius);
                print(rigidbody.name + force);
                //Enemy enemy = rigidbody.GetComponent<Enemy>();
                //if (enemy)
                //{
                //    

                //    float damage = _damage;
                //    if (distance >= _radius / 2)
                //        damage /= 1.5f;
                //    else if (distance >= _radius / 4)
                //        damage /= 1.2f;

                //    enemy.helth -= damage; // логика урона врагу
                //}
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    IEnumerator Off()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }

}