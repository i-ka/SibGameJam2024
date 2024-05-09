using System;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Scripts.Enemy
{
    public class EnemyBTContext
    {
        public Transform DetectedPlayer { get; set; }
        public Transform Self { get; set; }
        public NavMeshAgent NavMeshAgent { get; set; }
    }
}