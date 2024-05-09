using System;
using UnityEngine;

namespace Code.Scripts.Enemy
{
    public class EnemyBTContext
    {
        public SimpleEnemy Self { get; set; }
        public Transform Player { get; set; }
    }
}