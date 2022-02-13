using UnityEngine;

namespace RoguelikeProto.Scripts.Enemies
{
    public class Health : MonoBehaviour
    {
        public float currentHealth;
        [SerializeField] private EnemySettingsSo _enemySettings;

        private void Start()
        {
            currentHealth = _enemySettings.maxHealth;
        }

        private void FixedUpdate()
        {
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
