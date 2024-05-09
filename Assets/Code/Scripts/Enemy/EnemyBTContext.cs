using System;
using Code.Scripts.StateMachine;
using UnityEngine;

namespace Code.Scripts.Enemy
{
    public class EnemyBTContext
    {
        public SimpleEnemy Self { get; set; }
        public Transform Player { get; set; }
    }
    
    public class PatrolNode: IBtNode
    {
        private readonly EnemyBTContext _context;
        private readonly Transform[] _path;

        public PatrolNode(EnemyBTContext context, Transform[] path)
        {
            _context = context;
            _path = path;
        }
        
        public void OnEnter()
        {
            
        }

        public BtNodeResult Tick()
        {
            var sqrPlayerDistance = (_context.Player.position - _context.Self.transform.position).sqrMagnitude;
            if (sqrPlayerDistance <= 9)
            {
                return BtNodeResult.Success();
            }
            //todo: go through checkpoints
            return BtNodeResult.Running();
        }

        public void OnExit()
        {
        }
    }

    public class ChaseNode: IBtNode
    {
        private readonly EnemyBTContext _context;

        public ChaseNode(EnemyBTContext context)
        {
            _context = context;
        }
        
        public void OnEnter()
        {
        }

        public BtNodeResult Tick()
        {
            var vectorToPlayer = _context.Player.position - _context.Self.transform.position;
            var sqrPlayerDistance = vectorToPlayer.sqrMagnitude;
            var directionToPlayer = vectorToPlayer.normalized;
            if (sqrPlayerDistance > 9)
            {
                return BtNodeResult.Failure();
            }
            
            _context.Self.transform.Translate(directionToPlayer * (10 * Time.deltaTime));
            if (sqrPlayerDistance < 4)
            {
                return BtNodeResult.Success();
            }

            return BtNodeResult.Running();
        }

        public void OnExit()
        {
        }
    }

    public class AttackNode: IBtNode
    {
        private readonly EnemyBTContext _context;

        public AttackNode(EnemyBTContext context)
        {
            _context = context;
        }
        
        public void OnEnter()
        {
        }

        public BtNodeResult Tick()
        {
            var vectorToPlayer = _context.Player.position - _context.Self.transform.position;
            var sqrPlayerDistance = vectorToPlayer.sqrMagnitude;
            var directionToPlayer = vectorToPlayer.normalized;
            if (sqrPlayerDistance > 4)
            {
                return BtNodeResult.Failure();
            }
            Debug.Log("Attack player");
            return BtNodeResult.Success();
        }

        public void OnExit()
        {
        }
    }
    
    public class WaitNode: IBtNode
    {
        private readonly float _timeSeconds;

        private float _currentTime;

        public WaitNode(float timeSeconds)
        {
            _timeSeconds = timeSeconds;
        }

        public void OnEnter()
        {
            _currentTime = 0;
        }

        public BtNodeResult Tick()
        {
            if (_currentTime >= _timeSeconds)
            {
                return BtNodeResult.Success();
            }

            _currentTime += Time.deltaTime;
            return BtNodeResult.Running();
        }

        public void OnExit()
        {
        }
    }
}