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
            Debug.Log("Patroling");
        }

        public BtNodeResult Tick()
        {
            var sqrPlayerDistance = (_context.Player.position - _context.Self.transform.position).sqrMagnitude;
            //Debug.Log($"Distance to player is {sqrPlayerDistance}");
            if (sqrPlayerDistance <= Mathf.Pow(_context.Self.angerDistance, 2))
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
}