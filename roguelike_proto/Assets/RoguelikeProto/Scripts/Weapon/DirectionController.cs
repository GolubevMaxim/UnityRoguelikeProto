using System;
using UnityEngine;

namespace RoguelikeProto.Scripts.Weapon
{
    public class DirectionController : MonoBehaviour
    {
        [SerializeField] private GameObject aim;
        private void FixedUpdate()
        {
            var weaponPos = transform.position;
            var aimPos = aim.transform.position;
            var angle = Vector3.SignedAngle( Vector3.right, aimPos - weaponPos, Vector3.forward);
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}