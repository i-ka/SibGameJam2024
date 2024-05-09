using System;
using Code.Scripts.StateMachine;
using Code.Scripts.StateMachine.BtNodes;
using UnityEngine;

namespace Code.Scripts.Enemy
{
    [RequireComponent(typeof(StateMachineRunner))]
    public class SimpleEnemy : MonoBehaviour
    {
        private StateMachineRunner _stateMachineRunner;
        [SerializeField] private Transform playerTransform;

        public float angerDistance = 10;
        public float attackDistance = 2;

        private void Awake()
        {
            _stateMachineRunner = GetComponent<StateMachineRunner>();

            var state = new RunBtState(BuildBehaviourTree());
            _stateMachineRunner.StateMachine = new StateMachine.StateMachine(state);
        }

        private void Update()
        {
            //Debug.Log($"Distance to player is {(playerTransform.position - transform.position).magnitude}");
        }


        private IBtNode BuildBehaviourTree()
        {
            var context = new EnemyBTContext()
            {
                Player = playerTransform,
                Self = this
            };
            return new RepeatNode(
                new SequenceNode(
                    new PatrolNode(context, Array.Empty<Transform>()),
                    new ChaseNode(context),
                    new AlwaysSuccessNode(new RepeatNode(
                            new SequenceNode(
                                new AttackNode(context),
                                new WaitNode(1)
                            )
                        )
                    )
                )
            );
        }
    }
}