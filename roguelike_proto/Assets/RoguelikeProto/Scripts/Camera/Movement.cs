using UnityEngine;

namespace RoguelikeProto.Scripts.Camera
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private GameObject _target;

        private void FixedUpdate()
        {
            var targetPos = _target.transform.position;
            targetPos.z = -15;
            
            transform.position += (targetPos - transform.position) * 0.1f;
        }
    }
}
