using UnityEngine;

namespace RoguelikeProto.Scripts.Enemies.MeleeEnemy
{
    public class State
    {
        protected float attackRange = 5f;
        protected float enemySpeed = 2f;
        public enum STATE
        {
            Move,
            Attack
        }
        
        public enum EVENT
        {
            Enter,
            Update,
            Exit
        }

        protected STATE Name;
        protected EVENT stage;
        protected Transform _player;
        protected Transform _npc;
        protected State NextState;

        protected State(Transform player, Transform npc)
        {
            stage = EVENT.Enter;
            _npc = npc;
            _player = player;
        }

        protected virtual void Enter()
        {
            stage = EVENT.Update;
        }

        protected virtual void Update()
        {
            stage = EVENT.Update;
        }

        protected virtual void Exit()
        {
            stage = EVENT.Exit;
        }
        
        public State Process()
        {
            if (stage == EVENT.Enter)
            {
                Enter();
            }

            if (stage == EVENT.Update)
            {
                Update();
            }

            if (stage == EVENT.Exit)
            {
                Exit();
                return NextState;
            }

            return this;
        }
    }
}