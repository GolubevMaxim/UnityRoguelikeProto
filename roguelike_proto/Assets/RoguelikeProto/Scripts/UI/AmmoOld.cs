using RoguelikeProto.Scripts.Weapon;
using UnityEngine;
using UnityEngine.UI;

namespace RoguelikeProto.Scripts.UI
{
    public class AmmoOld : MonoBehaviour
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

            GetComponent<Text>().text = "Ammo: " + ammo;
        }
    }
}
