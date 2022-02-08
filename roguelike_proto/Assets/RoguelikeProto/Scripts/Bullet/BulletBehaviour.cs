using System;
using System.Collections;
using RoguelikeProto.Scripts.Player;
using UnityEngine;

namespace RoguelikeProto.Scripts.Bullet
{
    public class BulletBehaviour : MonoBehaviour
    {
        //[SerializeField] private Rigidbody2D rigidbody2D;
        [SerializeField] private BulletSettingsSo bulletSettings;

        public void Init(Vector3 shotDirection)
        {
            transform.rotation = Quaternion.Euler(0, 0, Vector3.SignedAngle(Vector3.right, shotDirection, Vector3.forward));
            GetComponent<Rigidbody2D>().velocity = shotDirection * bulletSettings.speed;
            StartCoroutine(BulletLifeCoroutine(bulletSettings.lifetime));
        }

        private IEnumerator BulletLifeCoroutine(float lifetime)
        {
            yield return new WaitForSeconds(lifetime);
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(col.gameObject.CompareTag("Player"))
                col.gameObject.GetComponent<Health>().currentHealth -= bulletSettings.damage;
            Destroy(gameObject);
        }
    }
}
