using UnityEngine;

namespace RoguelikeProto.Scripts.Enemies.DefaultEnemy
{
    public class Move : State
    {
        public Move(Transform player, Transform npc, EnemySettingsSo enemySettingsSo) : base(player, npc, enemySettingsSo)
        {
            Name = STATE.Move;
        }

        private void MoveNpcInPlayerDirection()
        {
            _npc.GetComponent<DefaultMoving>().Move(_player, _npc, EnemySettings);
        }

        protected override void Update()
        {
            if (_npc.GetComponent<DefaultMoving>().inMove) return;
            if (Vector2.Distance(_npc.transform.position, _player.transform.position)
                < EnemySettings.attackRange)
            {
                NextState = new Attack(_player, _npc, EnemySettings);
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