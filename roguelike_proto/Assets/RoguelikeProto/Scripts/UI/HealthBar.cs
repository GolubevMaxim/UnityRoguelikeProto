using RoguelikeProto.Scripts.Player;
using TMPro;
using UnityEngine;

namespace RoguelikeProto.Scripts.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        void Update()
        {
            if (player == null) return;
            GetComponent<TextMeshProUGUI>().text = "Health: " + player.GetComponent<Health>().currentHealth;
        }
    }
}
