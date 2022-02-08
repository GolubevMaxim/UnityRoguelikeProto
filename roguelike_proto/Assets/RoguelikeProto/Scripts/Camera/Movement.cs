using UnityEngine;

namespace RoguelikeProto.Scripts.Camera
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private GameObject _target;
        [SerializeField] private GameObject aim;
        [SerializeField] private CameraSettingsSo cameraSettings;

        private void FixedUpdate()
        {
            if (_target == null) return;
            var targetPos = _target.transform.position;
            targetPos.z = -cameraSettings.distanceBetweenCameraAndWorld;
            targetPos += Vector3.up; // this is made because technically player's position is in his shadow so camera is moved down a bit
            
            var position = transform.position;
            
            var aimPos = aim.transform.position;
            aimPos.z = -cameraSettings.distanceBetweenCameraAndWorld;
            aimPos = (aimPos - position).normalized * cameraSettings.mouseCameraMovementRatio;
            
            position += (aimPos + targetPos - position) * cameraSettings.smoothingRatio;
            transform.position = position;
        }
    }
}
