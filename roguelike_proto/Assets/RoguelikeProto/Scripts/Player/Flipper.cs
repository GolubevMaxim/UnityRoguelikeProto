using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace RoguelikeProto.Scripts.Player
{
    public class Flipper : MonoBehaviour
    {
        [SerializeField] private GameObject weapon;
        public bool flip;

        void FixedUpdate()
        {
            flip = Screen.width / 2f > Input.mousePosition.x;

            foreach (Transform child in transform)
            {
                if (child.name == "sprite")
                {
                    child.GetComponent<SpriteRenderer>().flipX = flip;
                }
            }

            var currentScale = weapon.transform.localScale;
            var newScale = currentScale;

            if (flip && currentScale.y > 0 || !flip && currentScale.y < 0)
            {
                newScale.y *= -1;
            }

            weapon.transform.localScale = newScale;

            
            var currentPosition = weapon.transform.localPosition;
            var newPosition = currentPosition;

            newPosition.x = flip ? -0.65f : 0.65f;

            weapon.transform.localPosition = newPosition;
        }
    }
}