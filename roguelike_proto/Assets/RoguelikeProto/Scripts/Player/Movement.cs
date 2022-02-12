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
        public bool _isRolling; // true if player is currently rolling
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
            _isRolling = false;
            _onCoolDown = false;
            foreach (Transform child in transform)
                if (child.name.Equals("sprite"))
                    _playerSprite = child.GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (!_isRolling && !_onCoolDown && Input.GetKeyDown(KeyCode.Space))
            {
                
                var x = Input.GetAxis("Horizontal");
                var y = Input.GetAxis("Vertical");
                var directionVec = Vector2.ClampMagnitude(new Vector2(x, y), 1f);
                if (!directionVec.Equals(Vector3.zero))
                {
                    directionVec.Normalize();
                    RollingDirection = directionVec * playerSettings.rollDistance;
                }
                else
                    return;
                _isRolling = true;
                _playerSprite.color = Color.black;
                StartCoroutine(RollingCoroutine());
            }
        }

        private void FixedUpdate()
        {
            if (_isRolling) return;
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");
            var directionVec = Vector2.ClampMagnitude(new Vector2(x, y), 1f);
            _rigidbody2D.velocity = directionVec * playerSettings.speed;
        }

        IEnumerator RollingCoroutine()
        {
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.AddForce(RollingDirection, ForceMode2D.Impulse);
            yield return new WaitForSeconds(playerSettings.rollTime);
            _isRolling = false;
            _onCoolDown = true;
            var cooldownTimeCounter = playerSettings.rollCooldown;
            var isVisible = true;
            int counter = 0;
            while (cooldownTimeCounter > 0)
            {
                if (counter >= 10) // every 10 frames sprite visibility changes
                {
                    isVisible = !isVisible;
                    counter = 0;
                }
                float colorChangeStep =
                    (playerSettings.rollCooldown - cooldownTimeCounter) / playerSettings.rollCooldown;
                _playerSprite.color = Color.Lerp(Color.black, Color.white, colorChangeStep);
                _playerSprite.enabled = isVisible; 
                cooldownTimeCounter -= Time.deltaTime;
                counter++;
                yield return null;
            }
            _playerSprite.enabled = true;
            _playerSprite.color = Color.white;
            _onCoolDown = false;
        }
    }
}