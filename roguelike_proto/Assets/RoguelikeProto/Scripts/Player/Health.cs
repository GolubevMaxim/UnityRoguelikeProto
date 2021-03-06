using System;
using UnityEngine;

namespace RoguelikeProto.Scripts.Player
{
    public class Health : MonoBehaviour
    {
        public float currentHealth;
        [SerializeField] private PlayerSettingsSo playerSettings;
        [SerializeField] private GameObject aim;
        private void Start()
        {
            currentHealth = playerSettings.maxHealth;
        }

        private void FixedUpdate()
        {
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
                Destroy(aim);
            }
        }
    }
}
