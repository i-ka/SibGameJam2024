using System;
using System.Collections;
using Code.Scripts.StateMachine;
using Code.Scripts.StateMachine.BtNodes;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Code.Scripts.Enemy
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(StateMachineRunner))]
    public class LavaMultiped : MonoBehaviour
    {
        public event Action<LavaMultiped> OnDestroyed;
        private StateMachineRunner _stateMachineRunner;
        private Animator _animator;

        [SerializeField] private float damage = 2;
        [SerializeField] private Transform playerTransform;

        private NavMeshAgent _navMeshAgent;
        private EnemyBTContext _btContext;

        public float angerDistance = 10;
        public float attackDistance = 2;
        private static readonly int SpeedAnimationPropertyId = Animator.StringToHash("MotionSpeed");
        private static readonly int Death = Animator.StringToHash("Death");
        private static readonly int AwakeAnimationId = Animator.StringToHash("Awake");

        private GameStateController _gameStateController;

        [Inject]
        public void Construct(GameStateController gameStateController)
        {
            _gameStateController = gameStateController;
        }

        private void Awake()
        {
            _stateMachineRunner = GetComponent<StateMachineRunner>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _btContext = new EnemyBTContext()
            {
                Player = playerTransform,
                DetectedPlayer = null,
                Self = transform,
                NavMeshAgent = _navMeshAgent,
                IsDying = false,
                Animator = _animator
            };

            var chaseState = new RunBtState<EnemyBTContext>(
                new SequenceNode("ChaseSequence",
                    new TriggerAnimationNode(_btContext, AwakeAnimationId),
                    new WaitNode(1, "WaitBeforeChase"),
                    new ChaseNode(_btContext, angerDistance, attackDistance)
                ), _btContext);

            var patrolState = new RunBtState<EnemyBTContext>(new SequenceNode("Patrol",
                new RepeatNode(new SequenceNode("PatrolSequence",
                    new PatrolNode(_btContext, angerDistance)
                ))
            ), _btContext);

            var fightState = new RunBtState<EnemyBTContext>(new RepeatNode(new SequenceNode("AttackSequence",
                new AttackNode(_btContext, damage, attackDistance),
                new WaitNode(1)
            )), _btContext);

            var dyingState = new RunBtState<EnemyBTContext>(new RepeatNode(new WaitNode(1)), _btContext);

            var stateMachine = new StateMachine<EnemyBTContext>(patrolState, _btContext);

            stateMachine.AddTransition(patrolState, chaseState,
                ctx => ctx.DetectedPlayer);

            stateMachine.AddTransition(chaseState, patrolState,
                ctx => !ctx.DetectedPlayer);

            stateMachine.AddTransition(chaseState, fightState, IsPlayerOnAttackDistance);

            stateMachine.AddTransition(fightState, chaseState,
                ctx => !IsPlayerOnAttackDistance(ctx));

            stateMachine.AddTransition(fightState, dyingState, c => c.IsDying);
            stateMachine.AddTransition(chaseState, dyingState, c => c.IsDying);
            stateMachine.AddTransition(patrolState, dyingState, c => c.IsDying);

            _stateMachineRunner.StateMachine = stateMachine;
        }

        private void Update()
        {
            _animator.SetFloat(SpeedAnimationPropertyId, _navMeshAgent.speed);
        }

        public void OnDead()
        {
            _gameStateController.EnemyKilled();
            StartCoroutine(PlayDeadSequence());
        }

        private IEnumerator PlayDeadSequence()
        {
            _animator.SetTrigger(Death);
            _btContext.IsDying = true;
            yield return new WaitForSeconds(1.0f);
            Destroy(gameObject);
        }

        public void OnDamaged(float damageGotten)
        {
            //todo damage effect
        }

        private void OnDestroy()
        {
            OnDestroyed?.Invoke(this);
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