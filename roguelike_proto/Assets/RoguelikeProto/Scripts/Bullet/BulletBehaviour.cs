using System.Collections;
using UnityEngine;
using Health = RoguelikeProto.Scripts.Enemies.Health;

namespace RoguelikeProto.Scripts.Bullet
{
    public class BulletBehaviour : MonoBehaviour
    {
        [SerializeField] private BulletSettingsSo bulletSettings;

        public void Init(Vector3 shotDirection)
        {
            Vector3 orthoVector = (Quaternion.FromToRotation(Vector3.forward, shotDirection) *
                                   (Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.forward) * Vector3.right)).normalized;
            orthoVector *= Random.Range(-bulletSettings.random, bulletSettings.random);
            shotDirection += orthoVector;
            shotDirection.Normalize();
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
            if (col.gameObject.CompareTag($"Enemy"))
                col.gameObject.GetComponent<Health>().currentHealth -= bulletSettings.damage;
            if (col.gameObject.CompareTag($"Player"))
                col.gameObject.GetComponent<Health>().currentHealth -= bulletSettings.damage;
            Destroy(gameObject);
        }
    }
}
