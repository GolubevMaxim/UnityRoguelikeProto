using UnityEngine;

namespace RoguelikeProto.Scripts.Enemies.RangeEnemy
{
    public class Attack : State
    {
        private float _enterTime;
        public Attack(Transform player, Transform npc) : base(player, npc)
        {
            Name = STATE.Attack;
        }

        private void AttackNpc()
        {
            Debug.Log("Attack");
        }

        protected override void Enter()
        {
            _enterTime = Time.time;
            base.Enter();
        }


        protected override void Update()
        {
            if ((Time.time - _enterTime) > 1 &&
                (Vector2.Distance(_npc.transform.position, _player.transform.position) > attackRange))
            {
                NextState = new Move(_player, _npc);
                stage = EVENT.Exit;
            }
            else
            {
                AttackNpc();
                base.Update();   
            }
        }
    }
}