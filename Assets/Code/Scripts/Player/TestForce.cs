using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestForce : MonoBehaviour
{
    [SerializeField]
    private Rigidbody player;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            player.AddExplosionForce(2000, transform.position, 200);
        }
    }
}
