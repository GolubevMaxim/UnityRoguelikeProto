using RoguelikeProto.Scripts.Weapon;
using TMPro;
using UnityEngine;

namespace RoguelikeProto.Scripts.UI
{
    public class Ammo : MonoBehaviour
    {
        [SerializeField] private GameObject _weapon;
        void Update()
        {
            if (_weapon == null) return;
        
            var ammo = 0;
            
            foreach (Transform child in _weapon.transform)
            {
                if (child.CompareTag("Weapon"))
                {
                    ammo = child.GetComponent<PlayerShooting>().currentBulletsCount;
                }
            }

            GetComponent<TextMeshProUGUI>().text = "Ammo: " + ammo;
        }
    }
}
