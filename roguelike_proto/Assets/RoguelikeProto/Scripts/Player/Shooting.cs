using RoguelikeProto.Scripts.Bullet;
using UnityEngine;

namespace RoguelikeProto.Scripts.Player
{
    public class Shooting : MonoBehaviour
    {
        [SerializeField] private GameObject aim;

        private Vector3 _bulletSummonPoint;
        private void Start()
        {
            /*foreach (Transform playerChild in transform)
            {
                if (playerChild.name.Equals("Weapon"))
                {
                    foreach (Transform weaponChild in playerChild.transform)
                    {
                        if (weaponChild.name.Equals("ak47") || weaponChild.name.Equals("pistol"))
                        {
                            foreach (Transform exactWeaponChild in weaponChild.transform)
                            {
                                if (exactWeaponChild.name.Equals("BulletSummonPoint"))
                                {
                                    _bulletSummonPoint = exactWeaponChild.position;
                                    Debug.Log("Hooray something happened");               
                                }
                            }
                        }
                    }
                }
            }*/
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && GetComponent<Movement>()._isRolling == false)
                transform.Find("Weapon").GetComponent<Weapon.Shooting>().Shoot(aim);
        }

    }
}
