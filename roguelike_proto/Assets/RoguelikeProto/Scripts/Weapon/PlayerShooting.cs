using System;
using System.Collections;
using RoguelikeProto.Scripts.Bullet;
using UnityEngine;
using UnityEngine.Serialization;

namespace RoguelikeProto.Scripts.Weapon
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private WeaponSettingsSo settings;
        private bool _onCooldown;
        public bool _onReload;
        public int currentBulletsCount;

        private void Start()
        {
            currentBulletsCount = settings.storage;
        }

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
        IEnumerator ReloadingCoroutine(WeaponSettingsSo currentSettings)
        {
            var weaponSprite = transform.Find("sprite").GetComponent<SpriteRenderer>();
            weaponSprite.color = Color.red;
            float rechargeTimeCounter = currentSettings.rechargeTime;
            var isVisible = true;
            int counter = 0;
            while (rechargeTimeCounter > 0)
            {
                if (counter >= 10)
                {
                    isVisible = !isVisible;
                    counter = 0;
                }
                float t = (currentSettings.rechargeTime - rechargeTimeCounter) / currentSettings.rechargeTime;
                weaponSprite.color = Color.Lerp(Color.red, Color.white, t);
                weaponSprite.enabled = isVisible;
                rechargeTimeCounter -= Time.deltaTime;
                counter++;
                yield return null;
            }
            weaponSprite.enabled = true;
            weaponSprite.color = Color.white;
            currentBulletsCount = currentSettings.storage;
            _onReload = false;
        }

        public void Shoot(GameObject target)
        {
            if (currentBulletsCount <= 0)
            {
                _onReload = true;
                StartCoroutine(ReloadingCoroutine(settings));
            }
            if (_onCooldown || _onReload)
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
            currentBulletsCount--;
            StartCoroutine(CooldownCoroutine());
        }
    }
}
