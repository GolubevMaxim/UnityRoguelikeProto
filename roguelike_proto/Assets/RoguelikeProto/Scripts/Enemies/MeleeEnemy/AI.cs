using UnityEngine;

namespace RoguelikeProto.Scripts.Enemies.MeleeEnemy
{
    public class AI : MonoBehaviour
    {
        private Transform _player;

        private State _state;
        void Start()
        {
            _player = GameObject.FindWithTag("Player").transform;
            _state = new Move(_player, this.transform);
        }

        void Update()
        {
            _state = _state.Process();
        }
    }
}