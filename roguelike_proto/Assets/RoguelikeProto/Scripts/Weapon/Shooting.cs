using RoguelikeProto.Scripts.Bullet;
using UnityEngine;

namespace RoguelikeProto.Scripts.Weapon
{
    public class Shooting : MonoBehaviour
    {
        [SerializeField] private GameObject prefab; // bullet prefab

        public void Shoot(GameObject target)
        {
            Vector3 bulletSummonPoint = Vector3.zero;
            GiveWeapon.Weapon currentWeapon = GetComponent<GiveWeapon>()._currentWeapon;
            switch (currentWeapon)
            {
                case GiveWeapon.Weapon.None:
                    return;
                case GiveWeapon.Weapon.Ak47:
                    bulletSummonPoint = transform.Find("ak47(Clone)").Find("BulletSummonPoint").position;
                    break;
                case GiveWeapon.Weapon.Pistol:
                    bulletSummonPoint = transform.Find("pistol(Clone)").Find("BulletSummonPoint").position;
                    break;
            }
            var bullet = Instantiate(prefab, bulletSummonPoint, Quaternion.identity);
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            bullet.GetComponent<BulletBehaviour>().Init((target.transform.position - bulletSummonPoint).normalized);
        }
    }
}
