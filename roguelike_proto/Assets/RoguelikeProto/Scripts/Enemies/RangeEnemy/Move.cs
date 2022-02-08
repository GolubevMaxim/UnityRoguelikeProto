using UnityEngine;

namespace RoguelikeProto.Scripts.Enemies.RangeEnemy
{
    public class Move : State
    {
        public Move(Transform player, Transform npc) : base(player, npc)
        {
            Name = STATE.Move;
        }

        private void MoveNpcInPlayerDirection()
        {
            _npc.transform.Translate((_player.transform.position - _npc.transform.position) 
                                     * (enemySpeed * Time.deltaTime));
        }

        protected override void Update()
        {
            if (Vector2.Distance(_npc.transform.position, _player.transform.position) < attackRange)
            {
                NextState = new Attack(_player, _npc);
                stage = EVENT.Exit;
            }
            else
            {
                MoveNpcInPlayerDirection();
                base.Update();   
            }
        }
    }
}