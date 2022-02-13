using UnityEngine;

namespace RoguelikeProto.Scripts.Enemies
{
    [CreateAssetMenu(fileName = "Enemy settings", menuName = "Settings/Enemy")]
    public class EnemySettingsSo : ScriptableObject
    {
        [SerializeField] public float speed;
        [SerializeField] public int maxHealth;
        [SerializeField] public float attackRange;
        [SerializeField] public float missDirection;
    }
}
