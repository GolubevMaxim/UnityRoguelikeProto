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
            if (_weapon == null) GetComponent<Text>().text = "";
            else
            {
                var ammo = 0;
                bool onReload = false;

                foreach (Transform child in _weapon.transform)
                {
                    if (child.CompareTag("Weapon"))
                    {
                        var component = child.GetComponent<PlayerShooting>();
                        ammo = component.currentBulletsCount;
                        onReload = component._onReload;
                    }
                }

                if (!onReload)
                    GetComponent<Text>().text = " Ammo: " + ammo;
                else
                    GetComponent<Text>().text = " Reloading... ";
            }
        }
    }
}
