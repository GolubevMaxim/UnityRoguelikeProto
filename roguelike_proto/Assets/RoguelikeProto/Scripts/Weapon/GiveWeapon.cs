using RoguelikeProto.Scripts.Player;
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
            if(Input.GetKeyDown(KeyCode.Alpha1))
                gettingWeapon(Weapon.Ak47);
            if(Input.GetKeyDown(KeyCode.Alpha2))
                gettingWeapon(Weapon.Pistol);
        }

        void gettingWeapon(Weapon weapon)
        {
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

            // correctWeaponFlip(_weapon);
            transform.GetComponent<Storage>().Start();
            _weapon.transform.SetParent(transform);
        }

        void correctWeaponFlip(GameObject weapon)
        {
            //if (weapon.transform.rotation.eulerAngles.z is > 90 and < 270)
            if(Input.mousePosition.x < Screen.width / 2f)
                weapon.transform.Find("sprite").GetComponent<SpriteRenderer>().flipY = true;
        }

        public enum Weapon
        { None = 0, Ak47 = 1, Pistol = 2 }
    }
}
