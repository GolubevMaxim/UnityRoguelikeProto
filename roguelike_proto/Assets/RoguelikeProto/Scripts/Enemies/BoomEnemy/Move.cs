using UnityEngine;

namespace RoguelikeProto.Scripts.Enemies.BoomEnemy
{
    public class Move : State
    {
        public Move(Transform player, Transform npc, EnemySettingsSo enemySettingsSo) : base(player, npc, enemySettingsSo)
        {
            Name = STATE.Move;
        }

        private void MoveNpcInPlayerDirection()
        {
            _npc.transform.Translate((_player.transform.position - _npc.transform.position).normalized
                                     * (EnemySettings.speed * Time.deltaTime));
        }

        protected override void Update()
        {
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