using UnityEngine;

namespace RoguelikeProto.Scripts.Camera
{
    [CreateAssetMenu(fileName = "Camera settings", menuName = "Settings/Camera")]
    public class CameraSettingsSo : ScriptableObject
    {
        [field: SerializeField] public float mouseCameraMovementRatio;
        [field: SerializeField] public float smoothingRatio;
        [field: SerializeField] public float distanceBetweenCameraAndWorld;
    }
}
