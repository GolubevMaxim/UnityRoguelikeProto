using System.Collections;
using UnityEngine;

namespace RoguelikeProto.Scripts.Enemies.DefaultEnemy
{
    public class DefaultMoving : MonoBehaviour
    {
        public bool inMove;
    
        public void Move(Transform player, Transform npc, EnemySettingsSo enemySettings)
        {
        
            StartCoroutine(MovingCoroutine(player, npc, enemySettings));
        }

        IEnumerator MovingCoroutine(Transform player, Transform npc, EnemySettingsSo EnemySettings)
        {
            inMove = true;
            var moveDirection = (player.transform.position - npc.transform.position).normalized;
            Vector3 orthoVector = (Quaternion.FromToRotation(Vector3.forward, moveDirection) *
                                   (Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.forward) * Vector3.right)).normalized;
            orthoVector *= Random.Range(EnemySettings.missDirection, EnemySettings.missDirection);
            moveDirection += orthoVector;
            moveDirection.Normalize();
            npc.GetComponent<Rigidbody2D>().velocity = moveDirection * EnemySettings.speed;

            yield return new WaitForSeconds(1);
            npc.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            inMove = false;
        } 
    }
}
