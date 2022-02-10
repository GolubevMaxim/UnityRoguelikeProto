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
        [SerializeField] private WeaponSettingsSo ak47Settings;
        [SerializeField] private WeaponSettingsSo pistolSettings;
        private bool _isShooting = false;
        private bool _isOnCooldown = false;
        private bool _isOnReload = false;
        private SpriteRenderer weaponSprite;

        void Update()
        {
            Transform weapon = transform.Find("Weapon");
            GiveWeapon.Weapon currentWeapon = weapon.GetComponent<GiveWeapon>()._currentWeapon;
            WeaponSettingsSo currentSettings = null;
            switch (currentWeapon)
            {
                case GiveWeapon.Weapon.None:
                    return;
                case GiveWeapon.Weapon.Ak47:
                    currentSettings = ak47Settings;
                    break;
                case GiveWeapon.Weapon.Pistol:
                    currentSettings = pistolSettings;
                    break;
                default:
                    return;
            }

            if (weapon.GetComponent<Storage>().currentBulletsCount <= 0)
            {
                _isOnReload = true;
                StartCoroutine(ReloadingCoroutine(currentSettings));
            }
            if (Input.GetMouseButtonDown(0) && !GetComponent<Movement>()._isRolling && !_isShooting && !_isOnCooldown && !_isOnReload)
            {
                _isShooting = true;
                StartCoroutine(ShootingCoroutine(currentSettings));
                weapon.GetComponent<Storage>().currentBulletsCount--;
            }
        }
        IEnumerator ShootingCoroutine(WeaponSettingsSo currentSettings)
        {
            transform.Find("Weapon").GetComponent<Weapon.Shooting>().Shoot(aim);
            _isShooting = false;
            _isOnCooldown = true;
            yield return new WaitForSeconds(currentSettings.cooldown);
            _isOnCooldown = false;
        }

        IEnumerator ReloadingCoroutine(WeaponSettingsSo currentSettings)
        {
            weaponSprite = GameObject.FindWithTag("Weapon").transform.Find("sprite").GetComponent<SpriteRenderer>();
            weaponSprite.color = Color.red;
            float rechargeTimeCounter = currentSettings.rechargeTime;
            while (rechargeTimeCounter > 0)
            {
                float t = (currentSettings.rechargeTime - rechargeTimeCounter) / currentSettings.rechargeTime;
                weaponSprite.color = Color.Lerp(Color.red, Color.white, t);
                rechargeTimeCounter -= Time.deltaTime;
                yield return null;
            }
            transform.Find("Weapon").GetComponent<Storage>().currentBulletsCount = currentSettings.storage;
            _isOnReload = false;
        }
    }
}
