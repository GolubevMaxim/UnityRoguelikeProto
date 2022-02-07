using UnityEngine;

namespace RoguelikeProto.Scripts.Player
{
    [CreateAssetMenu(fileName = "Player settings", menuName = "Settings/Player")]
    public class PlayerSettingsSo : ScriptableObject
    {
        [field: SerializeField] public float speed;
        [field: SerializeField] public float maxHealth;
        [field: SerializeField] public float rollTime;
        [field: SerializeField] public float rollDistance;
        [field: SerializeField] public float rollCooldown;
    }
}
