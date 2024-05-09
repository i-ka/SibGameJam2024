using System;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Scripts.Enemy
{
    public class EnemyBTContext
    {
        public LavaMultiped Self { get; set; }
        public Transform Player { get; set; }
        public NavMeshAgent NavMeshAgent { get; set; }
    }
}