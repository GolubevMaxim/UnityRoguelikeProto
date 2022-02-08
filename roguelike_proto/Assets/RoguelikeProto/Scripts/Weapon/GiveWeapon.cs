using RoguelikeProto.Scripts.Player;
using UnityEngine;

namespace RoguelikeProto.Scripts.Weapon
{
    public class GiveWeapon : MonoBehaviour
    {
        [SerializeField] private GameObject ak47Prefab;
        [SerializeField] private GameObject pistolPrefab;
        [SerializeField] private GameObject weaponSummonPoint;
        [SerializeField] private GameObject aim;
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
                    Destroy(transform.Find("pistol(Clone)").gameObject);
                    break;
            }
            //giving new one
            switch (weapon)
            {
                case Weapon.Ak47:
                    _weapon = Instantiate(ak47Prefab, weaponSummonPoint.transform.position, Quaternion.identity);
                    SetAngle();
                    _weapon.transform.SetParent(transform);
                    _currentWeapon = Weapon.Ak47;
                    break;
                case Weapon.Pistol:
                    _weapon = Instantiate(pistolPrefab, weaponSummonPoint.transform.position, Quaternion.identity);
                    SetAngle();
                    _weapon.transform.SetParent(transform);
                    _currentWeapon = Weapon.Pistol;
                    break;
            }
        }

        void SetAngle()
        {
            bool flip = GameObject.Find("Player").GetComponent<Flipper>()._flip;
            _weapon.transform.Find("sprite").GetComponent<SpriteRenderer>().flipX = flip;
            float angle;
            angle = Vector3.SignedAngle(flip ? Vector3.left : Vector3.right, aim.transform.position - transform.position, Vector3.forward);
            _weapon.transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        public enum Weapon
        { None = 0, Ak47 = 1, Pistol = 2 }
    }
}
