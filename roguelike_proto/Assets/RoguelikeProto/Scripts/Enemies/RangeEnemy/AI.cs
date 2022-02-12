using UnityEngine;

namespace RoguelikeProto.Scripts.Enemies.RangeEnemy
{
    public class AI : MonoBehaviour
    {
        [SerializeField] private EnemySettingsSo enemySettings;
        private Transform _player;

        private State _state;
        void Start()
        {
            _player = GameObject.FindWithTag("Player").transform;
            _state = new Move(_player, this.transform, enemySettings);
        }

        void Update()
        {
            if (_player == null)
            {
                return;
            }

            _state = _state.Process();
        }
    }
}