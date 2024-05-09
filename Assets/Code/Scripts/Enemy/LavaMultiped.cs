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
                Player = playerTransform,
                Self = this,
                NavMeshAgent = _navMeshAgent
            };

            var chaseState = new RunBtState<EnemyBTContext>(
                new SequenceNode("ChaseSequence",
                    new WaitNode(1, "WaitBeforeChase"),
                    new ChaseNode(context)
                ), context);

            var patrolState = new RunBtState<EnemyBTContext>(new RepeatNode(
                new PatrolNode(context, patrolPath)
            ), context);
            
            var fightState = new RunBtState<EnemyBTContext>(new SequenceNode("AttackSequence",
                new AttackNode(context),
                new WaitNode(1)
            ), context);
            
            var stateMachine = new StateMachine<EnemyBTContext>(patrolState, context);

            stateMachine.AddTransition();

            _stateMachineRunner.StateMachine = stateMachine;
        }

        private IBtNode BuildBehaviourTree()
        {
            var context = new EnemyBTContext()
            {
                Player = playerTransform,
                Self = this,
                NavMeshAgent = _navMeshAgent
            };
            return new RepeatNode(new AlwaysSuccessNode(
                new SequenceNode("MainSequence",
                    new PatrolNode(context, patrolPath),
                    new SequenceNode("ChaseSequence",
                        new WaitNode(1, "WaitBeforeChase"),
                        new ChaseNode(context)
                    ),
                    new AlwaysSuccessNode(new RepeatNode(
                        new SequenceNode("AttackSequence",
                            new AttackNode(context),
                            new WaitNode(1)
                        )))
                )
            ), tag: "MainLoop");
        }
    }
}