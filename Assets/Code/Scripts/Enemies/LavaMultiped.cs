using System;
using Code.Scripts.StateMachine;
using Code.Scripts.StateMachine.BtNodes;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Scripts.Enemy
{
    [RequireComponent(typeof(StateMachineRunner))]
    public class LavaMultiped : MonoBehaviour
    {
        private StateMachineRunner _stateMachineRunner;

        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform[] patrolPath;
        private NavMeshAgent _navMeshAgent;

        public float angerDistance = 10;
        public float attackDistance = 2;

        private void Awake()
        {
            _stateMachineRunner = GetComponent<StateMachineRunner>();
            _navMeshAgent = GetComponent<NavMeshAgent>();

            var context = new EnemyBTContext()
            {
                DetectedPlayer = null,
                Self = transform,
                NavMeshAgent = _navMeshAgent
            };

            var chaseState = new RunBtState<EnemyBTContext>(
                new SequenceNode("ChaseSequence",
                    new WaitNode(1, "WaitBeforeChase"),
                    new ChaseNode(context, angerDistance, attackDistance)
                ), context);

            var patrolState = new RunBtState<EnemyBTContext>(new RepeatNode(
                new PatrolNode(context, playerTransform, angerDistance, patrolPath)
            ), context);

            var fightState = new RunBtState<EnemyBTContext>(new RepeatNode(new SequenceNode("AttackSequence",
                new AttackNode(context, attackDistance),
                new WaitNode(1)
            )), context);

            var stateMachine = new StateMachine<EnemyBTContext>(patrolState, context);

            stateMachine.AddTransition(patrolState, chaseState,
                ctx => ctx.DetectedPlayer);
            
            stateMachine.AddTransition(chaseState, patrolState,
                ctx => !ctx.DetectedPlayer);
            
            stateMachine.AddTransition(chaseState, fightState,
                _ => (playerTransform.position - transform.position).sqrMagnitude <= Mathf.Pow(attackDistance, 2));
            
            stateMachine.AddTransition(fightState, chaseState,
                _ => (playerTransform.position - transform.position).sqrMagnitude > Mathf.Pow(attackDistance, 2));

            _stateMachineRunner.StateMachine = stateMachine;
        }
    }
}