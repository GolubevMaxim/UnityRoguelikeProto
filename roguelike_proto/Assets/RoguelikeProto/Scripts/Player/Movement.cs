using System;
using System.Collections;
using UnityEngine;

namespace RoguelikeProto.Scripts.Player
{
    public class Movement : MonoBehaviour
    {
        //[Tooltip("Player movement speed")]
        //[SerializeField] private float _speed;
        [SerializeField] private PlayerSettingsSo playerSettings;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private GameObject _aim;
        [Tooltip("Time interval player has to wait until new roll use")]
        [SerializeField] private float rollCoolDown;
        [Tooltip("Time interval of one roll")]
        [SerializeField] private float rollingTime;
        [Tooltip("Distance of the roll, 0.4f recommended")]
        [SerializeField] private float rollingDistance;
        
        private Vector3 _rollingDirection;
        private bool _isRolling; // true if player is currently rolling
        private bool _onCoolDown; // true if the roll ended, but cooldown time has not ran out

        private Vector3 RollingDirection
        {
            get => _rollingDirection;
            set
            {
                if (!_isRolling) _rollingDirection = value;
            }
        }
        private void Start()
        {
            this._isRolling = false;
            _onCoolDown = false;
        }

        private void Update()
        {
            if (!_isRolling && !_onCoolDown && Input.GetKeyDown(KeyCode.Space))
            {
                RollingDirection = (_aim.transform.position - transform.position).normalized * rollingDistance;
                _isRolling = true;
                StartCoroutine(RollingCoroutine());
            }
        }

        private void FixedUpdate()
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");
            if (this._isRolling) return;
            var directionVec = Vector2.ClampMagnitude(new Vector2(x, y), 1f);
            _rigidbody2D.velocity = directionVec * playerSettings.speed;
        }

        IEnumerator RollingCoroutine()
        {
            var rollingTimeCounter = rollingTime;
            while (rollingTimeCounter > 0)
            {
                var position = transform.position;
                position = Vector3.Lerp
                    (position, RollingDirection + position, rollingTime - rollingTimeCounter);
                transform.position = position;
                rollingTimeCounter -= Time.deltaTime;
                yield return null;
            }
            this._isRolling = false;
            _onCoolDown = true;
            yield return new WaitForSeconds(rollCoolDown);
            _onCoolDown = false;
        }
    }
}