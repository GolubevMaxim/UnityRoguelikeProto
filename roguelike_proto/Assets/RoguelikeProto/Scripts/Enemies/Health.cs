using UnityEngine;

namespace RoguelikeProto.Scripts.Enemies
{
    public class Health : MonoBehaviour
    {
        public float currentHealth;
        [SerializeField] private float maxHealth;

        private void Start()
        {
            currentHealth = maxHealth;
        }

        private void FixedUpdate()
        {
            if(currentHealth <= 0) Destroy(gameObject);
        }
    }
}
