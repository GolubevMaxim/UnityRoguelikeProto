using UnityEngine;

namespace RoguelikeProto.Scripts.Weapon
{
    [CreateAssetMenu(fileName = "Weapon Settings", menuName = "Settings/Weapon")]
    public class WeaponSettingsSo : ScriptableObject
    {
        [field: SerializeField] public float cooldown;
        [field: SerializeField] public int storage;
        [field: SerializeField] public float rechargeTime;

        [field: SerializeField] public GameObject bulletPrefab;
    }
}
