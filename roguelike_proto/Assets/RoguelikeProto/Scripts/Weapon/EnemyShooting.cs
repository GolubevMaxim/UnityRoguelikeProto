using System;
using System.Collections;
using RoguelikeProto.Scripts.Bullet;
using UnityEngine;

namespace RoguelikeProto.Scripts.Weapon
{
    public class EnemyShooting : MonoBehaviour
    {
        [SerializeField] private WeaponSettingsSo settings;
        private bool _onCooldown;
        private Vector3 GetBulletSummonPoint()
        {
            return transform.Find("BulletSummonPoint").transform.position;
        }

        
        IEnumerator CooldownCoroutine()
        {
            _onCooldown = true;
            yield return new WaitForSeconds(settings.cooldown);
            _onCooldown = false;
        }
        
        public void Shoot(GameObject target)
        {
            if (_onCooldown)
            {
                return;
            }

            Vector3 bulletSummonPoint;
            
            try
            {
                bulletSummonPoint = GetBulletSummonPoint();
            }
            catch (NullReferenceException)
            {
                return;
            }
            
            var bulletPrefab = settings.bulletPrefab;
        
            if (bulletPrefab is null)
            {
                return;
            }
            
            var bullet = Instantiate(bulletPrefab, bulletSummonPoint, Quaternion.identity);

            bullet.GetComponent<BulletBehaviour>().Init((target.transform.position - bulletSummonPoint).normalized);
            StartCoroutine(CooldownCoroutine());
        }
    }
}
