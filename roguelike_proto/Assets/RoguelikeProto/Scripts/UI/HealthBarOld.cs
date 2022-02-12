using RoguelikeProto.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

namespace RoguelikeProto.Scripts.UI
{
    public class HealthBarOld : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        void Update()
        {
            if (player == null) return;
            GetComponent<Text>().text = "Health: " + player.GetComponent<Health>().currentHealth;
        }
    }
}
