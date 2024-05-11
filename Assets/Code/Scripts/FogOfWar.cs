using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

public class FogOfWar : MonoBehaviour
{
    private VisualEffect fogEffect;

    [SerializeField]
    private float maxForceValue = 15;
    private float targetForce;

    private void Awake()
    {
        fogEffect = GetComponent<VisualEffect>();
    }

    private void Update()
    {
        var currentValue = fogEffect.GetFloat("Force Value");
        fogEffect.SetFloat("Force Value", Mathf.Lerp(currentValue, targetForce, Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        targetForce = 15;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        targetForce = 0;
    }
}
