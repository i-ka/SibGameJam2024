using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform _pointForTeleport;
    [SerializeField] private Portal[] _portalOther;
    private bool _canTeleport = true;

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("Player") && _canTeleport)
        {
            _portalOther[Random.Range(0, _portalOther.Length)].Teleport(coll.transform);
        }
    }

    public void Teleport(Transform player)
    {
        player.SetPositionAndRotation(_pointForTeleport.position, _pointForTeleport.rotation);
    }
}
