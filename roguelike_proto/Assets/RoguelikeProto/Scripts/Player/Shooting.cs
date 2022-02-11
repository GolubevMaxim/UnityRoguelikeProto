using System;
using System.Collections;
using RoguelikeProto.Scripts.Bullet;
using RoguelikeProto.Scripts.Weapon;
using UnityEngine;

namespace RoguelikeProto.Scripts.Player
{
    public class Shooting : MonoBehaviour
    {
        [SerializeField] private GameObject aim;

        GameObject GetWeapon()
        {
            foreach (Transform child in transform)
            {
                if (child.name.Equals("Weapon"))
                {
                    foreach (Transform weaponChild in child.transform)
                    {
                        if (weaponChild.CompareTag("Weapon"))
                        {
                            return weaponChild.gameObject;
                        }
                    }
                }
            }
            return null;
        }
        void Update()
        {
            if (Input.GetMouseButtonDown(0) && !GetComponent<Movement>()._isRolling)
            {
                GetWeapon().GetComponent<PlayerShooting>().Shoot(aim);
            }
        }
    }
}
