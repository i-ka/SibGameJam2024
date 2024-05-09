using System;
using Code.Scripts.StateMachine;
using UnityEngine;

namespace Code.Scripts.Enemy
{
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
            var vectorToPlayer = _context.Player.position - _context.Self.transform.position;
            var sqrPlayerDistance = vectorToPlayer.sqrMagnitude;
            var directionToPlayer = vectorToPlayer.normalized;
            if (sqrPlayerDistance <= Mathf.Pow(_context.Self.angerDistance, 2))
            {
                Debug.DrawRay(_context.Self.transform.position, directionToPlayer, Color.green);
                var hit = Physics.Raycast(_context.Self.transform.position, directionToPlayer, out var hitInfo);
                if (hit && hitInfo.transform == _context.Player)
                    return BtNodeResult.Success();
            }
            
            //todo: go through checkpoints
            
            return BtNodeResult.Running();
        }

        public void OnExit()
        {
        }
    }
}