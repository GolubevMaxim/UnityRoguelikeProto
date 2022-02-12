using System;
using RoguelikeProto.Scripts.Player;
using RoguelikeProto.Scripts.UI;
using UnityEngine;

namespace RoguelikeProto.Scripts.Weapon
{
    public class GiveWeapon : MonoBehaviour
    {
        [SerializeField] private GameObject ak47Prefab;
        [SerializeField] private GameObject pistolPrefab;
        [SerializeField] private GameObject weaponSummonPoint;
        
        private GameObject _weapon;
        public Weapon _currentWeapon;
        void Start()
        {
            _currentWeapon = Weapon.None;
            gettingWeapon(Weapon.Ak47);
        }
        
        void Update()
        {
            if (PauseMenu.OnPause) return;
            var scroll = Input.GetAxis("Mouse ScrollWheel");

            if (Math.Abs(scroll) > 0.1)
            {
                if (_currentWeapon == Weapon.Ak47)
                {
                    gettingWeapon(Weapon.Pistol);
                }
                else
                {
                    gettingWeapon(Weapon.Ak47);
                }
            }
            if(Input.GetKeyDown(KeyCode.Alpha1))
                gettingWeapon(Weapon.Ak47);
            if(Input.GetKeyDown(KeyCode.Alpha2))
                gettingWeapon(Weapon.Pistol);
        }

        void gettingWeapon(Weapon weapon)
        {
            if (weapon.Equals(_currentWeapon)) return;
            //destroying old weapon
            switch (_currentWeapon)
            {
                case Weapon.None:
                    break;
                case Weapon.Ak47:
                    Destroy(transform.Find("ak47(Clone)").gameObject);
                    break;
                case Weapon.Pistol:
                    Destroy(transform.Find("pistol(Clone)").gameObject); // could be simplified
                    break;
            }
            //giving new one
            switch (weapon)
            {
                case Weapon.Ak47:
                    _weapon = Instantiate(ak47Prefab, weaponSummonPoint.transform.position, transform.rotation);
                    _currentWeapon = Weapon.Ak47;
                    break;
                case Weapon.Pistol:
                    _weapon = Instantiate(pistolPrefab, weaponSummonPoint.transform.position, transform.rotation);
                    _currentWeapon = Weapon.Pistol;
                    break;
            }
            
            _weapon.transform.SetParent(transform);
            CorrectWeaponFlip(_weapon);
        }

        void CorrectWeaponFlip(GameObject weapon)
        {
            if (GameObject.FindWithTag("Player").GetComponent<Flipper>().flip)
            {
                var oldScale = weapon.transform.localScale;
                weapon.transform.localScale = new Vector3(oldScale.x, -oldScale.y, oldScale.z);
            }
        }

        public enum Weapon
        { None = 0, Ak47 = 1, Pistol = 2 }
    }
}
