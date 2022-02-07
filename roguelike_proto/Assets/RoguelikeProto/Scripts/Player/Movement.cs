using System;
using System.Collections;
using UnityEngine;

namespace RoguelikeProto.Scripts.Player
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private PlayerSettingsSo playerSettings;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private GameObject _aim;
        
        private Vector3 _rollingDirection;
        private bool _isRolling; // true if player is currently rolling
        private bool _onCoolDown; // true if the roll ended, but cooldown time has not ran out
        private SpriteRenderer _playerSprite;

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
            
            foreach (Transform child in transform)
                if (child.name.Equals("sprite"))
                    _playerSprite = child.GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (!_isRolling && !_onCoolDown && Input.GetKeyDown(KeyCode.Space))
            {
                RollingDirection = (_aim.transform.position - transform.position).normalized * playerSettings.rollDistance;
                _isRolling = true;
                _playerSprite.color = Color.black;
                //_playerSprite.transform.Rotate(new Vector3(0, 0, 90));
                StartCoroutine(RollingCoroutine());
            }
        }

        private void FixedUpdate()
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");
            if (_isRolling) return;
            var directionVec = Vector2.ClampMagnitude(new Vector2(x, y), 1f);
            _rigidbody2D.velocity = directionVec * playerSettings.speed;
            }

        IEnumerator RollingCoroutine()
        {
            _rigidbody2D.AddForce(RollingDirection, ForceMode2D.Impulse);
            yield return new WaitForSeconds(playerSettings.rollTime);
            _isRolling = false;
            //_playerSprite.transform.Rotate(new Vector3(0, 0, -90));
            
            _onCoolDown = true;
            var cooldownTimeCounter = playerSettings.rollCooldown;
            while (cooldownTimeCounter > 0)
            {
                float colorChangeStep =
                    (playerSettings.rollCooldown - cooldownTimeCounter) / playerSettings.rollCooldown;
                _playerSprite.color = Color.Lerp(Color.black, Color.white, colorChangeStep);
                cooldownTimeCounter -= Time.deltaTime;
                yield return null;
            }
            _onCoolDown = false;
        }
    }
}