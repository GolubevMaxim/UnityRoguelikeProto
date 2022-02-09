using UnityEngine;

namespace RoguelikeProto.Scripts.Bullet
{
    [CreateAssetMenu(fileName = "Bullet settings", menuName = "Settings/Bullet")]
    public class BulletSettingsSo : ScriptableObject
    {
        [field: SerializeField] public float damage;
        [field: SerializeField] public float speed;
        [field: SerializeField] public float lifetime;
        [field: SerializeField] public float random;
    }
}
