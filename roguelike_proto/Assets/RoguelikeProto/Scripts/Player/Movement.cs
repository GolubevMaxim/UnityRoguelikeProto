using UnityEngine;

namespace RoguelikeProto.Scripts.Player
{
    public class Movement : MonoBehaviour
    {
        // Start is called before the first frame update

        [SerializeField] private float _speed;

        [SerializeField] private Rigidbody2D _rigidbody2D;

        private void FixedUpdate()
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");

            var directionVec = Vector2.ClampMagnitude(new Vector2(x, y), 1f);
            _rigidbody2D.velocity = directionVec * _speed;
        }
    }
}