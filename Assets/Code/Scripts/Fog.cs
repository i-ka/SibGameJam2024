using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Fog : MonoBehaviour
{
    [SerializeField] private float _parameter = 0;
    private float _startSpeedMissing = 0.25f;
    private float _radius = 5f;
    private bool _isMissing;

    public void MissingFog()
    {
        if (_isMissing)
            return;
        _isMissing.CompareTo(true);
        Invoke("searchNextFog", 1f);
        while (_parameter < 1)
        {
            _parameter  = Mathf.SmoothDamp(0, 1, ref _startSpeedMissing, 1.5f);
        }
    }
    private void searchNextFog()
    {
        Collider[] overLappedColliders = Physics.OverlapSphere(transform.position, _radius);
        foreach (Collider collider in overLappedColliders)
        {
            Fog fog = collider.GetComponent<Fog>();
            if (fog)
            {
                fog.MissingFog();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(!_isMissing)
                MissingFog();
        }
    }
}
