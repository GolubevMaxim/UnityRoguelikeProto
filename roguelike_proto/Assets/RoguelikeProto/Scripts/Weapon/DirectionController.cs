using System;
using UnityEngine;

namespace RoguelikeProto.Scripts.Weapon
{
    public class DirectionController : MonoBehaviour
    {
        [SerializeField] private GameObject _target;

        private void FixedUpdate()
        {
            var weaponPos = transform.position;
            var targetPos = _target.transform.position;
            var angle = Vector3.SignedAngle( Vector3.right, targetPos - weaponPos, Vector3.forward);
            
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}