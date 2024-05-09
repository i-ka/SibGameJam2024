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
        private EnemyBTContext _btContext;

        public float angerDistance = 10;
        public float attackDistance = 2;

        private void Awake()
        {
            _stateMachineRunner = GetComponent<StateMachineRunner>();
            _navMeshAgent = GetComponent<NavMeshAgent>();

            _btContext = new EnemyBTContext()
            {
                Player = playerTransform,
                DetectedPlayer = null,
                Self = transform,
                NavMeshAgent = _navMeshAgent
            };

            var chaseState = new RunBtState<EnemyBTContext>(
                new SequenceNode("ChaseSequence",
                    new WaitNode(1, "WaitBeforeChase"),
                    new ChaseNode(_btContext, angerDistance, attackDistance)
                ), _btContext);

            var patrolState = new RunBtState<EnemyBTContext>(new RepeatNode(
                new SequenceNode("PatrolSequence",
                    new PatrolNode(_btContext, angerDistance, patrolPath),
                    new PropagateDetectedPlayerToHive(_btContext)
                )
            ), _btContext);

            var fightState = new RunBtState<EnemyBTContext>(new RepeatNode(new SequenceNode("AttackSequence",
                new AttackNode(_btContext, attackDistance),
                new WaitNode(1)
            )), _btContext);

            var stateMachine = new StateMachine<EnemyBTContext>(patrolState, _btContext);

            stateMachine.AddTransition(patrolState, chaseState,
                ctx => ctx.DetectedPlayer);
            
            stateMachine.AddTransition(chaseState, patrolState,
                ctx => !ctx.DetectedPlayer);
            
            stateMachine.AddTransition(chaseState, fightState, IsPlayerOnAttackDistance);
            
            stateMachine.AddTransition(fightState, chaseState,
                ctx => !IsPlayerOnAttackDistance(ctx));

            _stateMachineRunner.StateMachine = stateMachine;
        }

        private bool IsPlayerOnAttackDistance(EnemyBTContext context)
        {
            var player = context.Hive.DetectedPlayer ?? context.DetectedPlayer;
            return (player.position - context.Self.position).sqrMagnitude <= Mathf.Pow(attackDistance, 2);
        }

        public void SetPLayer(TestPlayer player)
        {
            _btContext.Player = player.transform;
        }

        public void SetHive(HiveContext hiveContext)
        {
            _btContext.Hive = hiveContext;
        }
    }
}