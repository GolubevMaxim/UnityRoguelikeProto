using System;
using UnityEngine;

namespace RoguelikeProto.Scripts.Aim
{
    public class MoveToMousePos : MonoBehaviour
    {
        private void Start()
        {
            MoveAimToMousePosition();
        }

        private void FixedUpdate()
        {
            MoveAimToMousePosition();
        }

        void MoveAimToMousePosition()
        {
            if (UnityEngine.Camera.main != null)
            {
                var newAimPos = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
                newAimPos.z = 0;

                transform.position = newAimPos;
            }
        }
    }
}