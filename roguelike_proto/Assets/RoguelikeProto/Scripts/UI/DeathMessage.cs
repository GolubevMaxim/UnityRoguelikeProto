using RoguelikeProto.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

namespace RoguelikeProto.Scripts.UI
{
    public class DeathMessage : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        void Update()
        {
            if (player == null)
                GetComponent<Text>().text = "YOU DIED!";
            else
                GetComponent<Text>().text = "";
        }
    }
}
