using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Blast : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _force;
    [SerializeField] private float _playerKnockbackReductionRatio = 15;
    [SerializeField] private bool _pushesPlayer = true;
    [SerializeField] private float _damage;
    private float _maxDamage;

    public void Explode()
    {
        StartCoroutine(destroy());
        Collider[] overLappedColliders = Physics.OverlapSphere(transform.position, _radius);
        foreach (var collider in overLappedColliders)
        {
            Bullet bullet = collider.GetComponent<Bullet>();
            float distance = Vector3.Distance(transform.position, collider.transform.position);
            var canDamage = collider.TryGetComponent(out HealthComponent health);
            if (canDamage && !collider.CompareTag("Player"))
            {
                var damage = _damage;
                if (distance >= _radius / 2)
                    damage /= 1.5f;
                else if (distance >= _radius / 4)
                    damage /= 1.2f;
                    
                health.ApplyDamage(damage);
            }
            Rigidbody rigidbody = collider.attachedRigidbody;
            if (rigidbody && !bullet)
            {
                
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
            else if (_pushesPlayer)
            {
                ThirdPersonController controller = collider.GetComponent<ThirdPersonController>();
                if (controller)
                {
                    float force = _force / _playerKnockbackReductionRatio;
                    var directionToPlayer = (controller.transform.position - transform.position).normalized;
                    controller.ApplyForce(directionToPlayer * force);
                }
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