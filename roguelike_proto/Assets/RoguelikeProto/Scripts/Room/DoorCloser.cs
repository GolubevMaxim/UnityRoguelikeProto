using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoguelikeProto.Scripts.Room
{
    public class DoorCloser : MonoBehaviour
    {
        public int aliveEnemiesCount;

        private List<Transform> GetDoors()
        {
            var doors = new List<Transform>();
            if (doors == null) throw new ArgumentNullException(nameof(doors));
        
            foreach (Transform child in transform.parent.transform)
            {
                if (child.CompareTag($"WallWithDoor"))
                {
                    foreach (Transform grandChild in child.transform)
                    {
                        if (grandChild.CompareTag($"Door"))
                        {
                            doors.Add(grandChild);
                        }
                    }
                }
            }

            return doors;
        }

        private void Start()
        {
            aliveEnemiesCount = 0;
            StartCoroutine(DoorLockCoroutine(GetDoors()));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {

            if (other.gameObject.CompareTag($"Enemy"))
            {
                aliveEnemiesCount++;
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {

            if (other.gameObject.CompareTag($"Enemy"))
            {
                aliveEnemiesCount--;
            }
        }
    

        private IEnumerator DoorLockCoroutine(List<Transform> doors)
        {
            while (true)
            {
                foreach (var door in doors)
                {
                    if (aliveEnemiesCount > 0)
                    {
                        door.GetComponentInChildren<BoxCollider2D>().enabled = true;
                        door.GetComponentInChildren<SpriteRenderer>().color = Color.red;
                    }
                    else
                    {
                        door.GetComponentInChildren<BoxCollider2D>().enabled = false;
                        door.GetComponentInChildren<SpriteRenderer>().color = Color.gray;
                    }
                }
                
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
    