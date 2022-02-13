using UnityEngine;

namespace RoguelikeProto.Scripts.Enemies.BoomEnemy
{
    public class AI : MonoBehaviour
    {
        private Transform _player;
        private State _state;

        [SerializeField] private EnemySettingsSo _enemySettings;
        void Start()
        {
            _player = GameObject.FindWithTag("Player").transform;
            _state = new Move(_player, this.transform, _enemySettings);
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