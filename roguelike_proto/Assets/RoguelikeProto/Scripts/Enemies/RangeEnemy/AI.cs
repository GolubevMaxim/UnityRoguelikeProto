using UnityEngine;

namespace RoguelikeProto.Scripts.Enemies.RangeEnemy
{
    public class AI : MonoBehaviour
    {
        [SerializeField] private Transform _player;

        private State _state;
        
        // Start is called before the first frame update
        void Start()
        {
            _state = new Move(_player, this.transform);
        }

        // Update is called once per frame
        void Update()
        {
            _state = _state.Process();
        }
    }
}