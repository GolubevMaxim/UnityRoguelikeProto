using RoguelikeProto.Scripts.Bullet;
using UnityEngine;

namespace RoguelikeProto.Scripts.Weapon
{
    public class Shooting : MonoBehaviour
    {
        [SerializeField] private GameObject ak47BulletPrefab; // bullet prefab
        [SerializeField] private GameObject pistolBulletPrefab;

        public void Shoot(GameObject target)
        {
            Vector3 bulletSummonPoint = Vector3.zero;
            GiveWeapon.Weapon currentWeapon = GetComponent<GiveWeapon>()._currentWeapon;
            GameObject bulletPrefab = null;
            
            switch (currentWeapon)
            {
                case GiveWeapon.Weapon.None:
                    return;
                case GiveWeapon.Weapon.Ak47:
                    bulletSummonPoint = transform.Find("ak47(Clone)").Find("BulletSummonPoint").position;
                    bulletPrefab = ak47BulletPrefab;
                    break;
                case GiveWeapon.Weapon.Pistol:
                    bulletSummonPoint = transform.Find("pistol(Clone)").Find("BulletSummonPoint").position;
                    bulletPrefab = pistolBulletPrefab;
                    break;
                default:
                    return;
            }
            
            var bullet = Instantiate(bulletPrefab, bulletSummonPoint, Quaternion.identity);
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GameObject.Find("Player").GetComponent<Collider2D>());
            foreach (var floor in GameObject.FindGameObjectsWithTag("Floor"))
            {
                Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), floor.GetComponent<Collider2D>());
            } 

            bullet.GetComponent<BulletBehaviour>().Init((target.transform.position - bulletSummonPoint).normalized);
        }
    }
}
