using System;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Scripts.Enemy
{
    public class EnemyBTContext
    {
        public Transform Player { get; set; }
        public Transform DetectedPlayer { get; set; }
        public Transform Self { get; set; }
        public NavMeshAgent NavMeshAgent { get; set; }
        public bool IsDying { get; set; }
        
        public HiveContext Hive { get; set; }
    }

    public class HiveContext
    {
        public Transform DetectedPlayer { get; set; }
        public Transform HivePosition { get; set; }
        public float PatrolRadius { get; set; }
    }
}