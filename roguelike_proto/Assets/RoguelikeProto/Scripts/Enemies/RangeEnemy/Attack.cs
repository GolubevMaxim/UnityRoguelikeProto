using UnityEngine;

namespace RoguelikeProto.Scripts.Enemies.RangeEnemy
{
    public class Attack : State
    {
        public Attack(Transform player, Transform npc) : base(player, npc)
        {
            Name = STATE.Attack;
        }

        private void AttackNpc()
        {
            Debug.Log("Attack");
        }

        protected override void Update()
        {
            if (Vector2.Distance(_npc.transform.position, _player.transform.position) > attackRange)
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