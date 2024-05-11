using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToBone : MonoBehaviour
{
    [SerializeField]
    private Transform bone;

    [SerializeField] private Transform point;

    private void Update()
    {
        transform.position = bone.position + (transform.position - point.position);
    }
}
