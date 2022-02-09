using System;
using UnityEngine;

namespace RoguelikeProto.Scripts.Weapon
{
    public class Storage : MonoBehaviour
    {
        [SerializeField] private WeaponSettingsSo ak47Settings;
        [SerializeField] private WeaponSettingsSo pistolSettings;
        public int currentBulletsCount;

        public void Start()
        {
            GiveWeapon.Weapon currentWeapon = GetComponent<GiveWeapon>()._currentWeapon;
            //TODO replace currentWeapon into player settings
            switch (currentWeapon)
            {
                case GiveWeapon.Weapon.None:
                    return;
                case GiveWeapon.Weapon.Ak47:
                    currentBulletsCount = ak47Settings.storage;
                    break;
                case GiveWeapon.Weapon.Pistol:
                    currentBulletsCount = pistolSettings.storage;
                    break;
                default:
                    return;
            }
        }
    }
}
