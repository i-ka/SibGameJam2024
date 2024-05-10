using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Blast : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _force;
    private float _maxDamage;

    public void Explode()
    {
        StartCoroutine(destroy());
        Collider[] overLappedColliders = Physics.OverlapSphere(transform.position, _radius);
        foreach (var collider in overLappedColliders)
        {
            Bullet bullet = collider.GetComponent<Bullet>();
            ThirdPersonController controller = collider.GetComponent<ThirdPersonController>();
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
            else if (controller)
            {
                float force = _force / 4;
                Vector3 direction = new Vector3(-(_radius - (transform.position.x - controller.transform.position.x)* 3), 
                                    (_radius - (transform.position.y - controller.transform.position.y) * 3),
                                    (_radius - (transform.position.z - controller.transform.position.z) * 3));
                controller.ApplyForce(direction);
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