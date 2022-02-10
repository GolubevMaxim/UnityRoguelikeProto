using System;
using UnityEngine;

namespace RoguelikeProto.Scripts.Player
{
    public class Flipper : MonoBehaviour
    {
        [SerializeField] private GameObject weapon;
        public bool _flip;

        private void Awake()
        {
            _flip = false;
        }

        void FixedUpdate()
        {
            _flip = Screen.width / 2f > Input.mousePosition.x;

            foreach (Transform child in transform)
            {
                if (child.name == "sprite")
                {
                    child.GetComponent<SpriteRenderer>().flipX = _flip;
                }
            }

            var currentScale = weapon.transform.localScale;
            var newScale = currentScale;

            if (_flip && currentScale.y > 0 || !_flip && currentScale.y < 0)
            {
                newScale.y *= -1;
            }

            weapon.transform.localScale = newScale;

            
            var currentPosition = weapon.transform.localPosition;
            var newPosition = currentPosition;

            newPosition.x = _flip ? -0.65f : 0.65f;

            weapon.transform.localPosition = newPosition;
        }
    }
}