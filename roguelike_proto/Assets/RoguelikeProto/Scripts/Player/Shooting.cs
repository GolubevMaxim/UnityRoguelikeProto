using RoguelikeProto.Scripts.Bullet;
using UnityEngine;

namespace RoguelikeProto.Scripts.Player
{
    public class Shooting : MonoBehaviour
    {
        [SerializeField] private GameObject aim;

        private Vector3 _bulletSummonPoint;

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && GetComponent<Movement>()._isRolling == false)
                transform.Find("Weapon").GetComponent<Weapon.Shooting>().Shoot(aim);
        }
    }
}
