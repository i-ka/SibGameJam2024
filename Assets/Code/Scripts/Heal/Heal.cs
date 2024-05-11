using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    [SerializeField] private float _heal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HealthComponent healthComponent = other.GetComponent<HealthComponent>();
            healthComponent.ApplyHeal(_heal);
            Destroy(this.gameObject);
        }
    }
}
