using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Blast : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _force;
    [SerializeField] private float _upwardsForce;
    private float _maxDamage;

    public void Explode()
    {
        StartCoroutine(destroy());
        Collider[] overLappedColliders = Physics.OverlapSphere(transform.position, _radius);
        foreach (var collider in overLappedColliders)
        {
            Bullet bullet = collider.GetComponent<Bullet>();
            Movement player = collider.GetComponent<Movement>();
            Rigidbody rigidbody = collider.attachedRigidbody;
            if (rigidbody && !bullet)
            {
                rigidbody.AddExplosionForce(_force, transform.position, _radius, _upwardsForce, ForceMode.Impulse);
                //print(rigidbody.name + _force);
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

    private IEnumerator destroy()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }

}