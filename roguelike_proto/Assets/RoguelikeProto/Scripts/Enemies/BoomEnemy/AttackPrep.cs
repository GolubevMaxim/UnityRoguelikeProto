using System.Collections;
using System.Collections.Generic;
using RoguelikeProto.Scripts.Weapon;
using UnityEngine;

namespace RoguelikeProto.Scripts.Enemies.BoomEnemy
{
    public class AttackPrep : MonoBehaviour
    {
        public void Attack()
        {
            StartCoroutine(AttackCoroutine());
        }
        
        private IEnumerator AttackCoroutine()
        {
            var enemySprite = gameObject.GetComponent<SpriteRenderer>();
            enemySprite.color = Color.blue;
            float boomTime = 1;

            while (boomTime > 0)
            {
                float t = (1 - boomTime) / 1;
                enemySprite.color = Color.Lerp(Color.white, Color.blue, t);
                boomTime -= Time.deltaTime;
                yield return null;
            }
            
            foreach (Transform child in transform)
            {
                if (child.CompareTag("Weapon"))
                {
                    child.GetComponent<EnemyShooting>().Shoot(GameObject.Find("Player").gameObject);
                }   
            }

            transform.GetComponent<Health>().currentHealth = 0;
        }
    }
}
