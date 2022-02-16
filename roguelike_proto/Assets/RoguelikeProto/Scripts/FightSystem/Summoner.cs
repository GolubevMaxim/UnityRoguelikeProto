using System;
using System.Collections;
using System.Collections.Generic;
using RoguelikeProto.Scripts.Room;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RoguelikeProto.Scripts.FightSystem
{
    public class Summoner : MonoBehaviour
    {
        [SerializeField] 
        private int wavesCount;
        [SerializeField]
        private int enemiesInWaveCount;
        
        [SerializeField] 
        private int sizeX;
        [SerializeField] 
        private int sizeY;

        [SerializeField] private float spawnTime;
        
        private DoorCloser _doorCloser;

        private List<GameObject> _enemyObjects;
        private Queue<GameObject> _enemyQueue;
       
        private static List<GameObject> LoadEnemyObj()
        {
            var enemies = new List<GameObject>(Resources.LoadAll<GameObject>("Prefabs/Enemies"));
            if (enemies == null) throw new ArgumentNullException(nameof(enemies));

            return enemies;
        }

        private static GameObject LoadSpawner()
        {
            return Resources.Load<GameObject>("Prefabs/Spawner/Spawner");
        }

        private void Start()
        {
            _doorCloser = transform.GetComponentInChildren<DoorCloser>();   
            _enemyObjects = LoadEnemyObj();
            _enemyQueue = new Queue<GameObject>();

            for (var i = 0; i < wavesCount * enemiesInWaveCount; i++)
            {
                _enemyQueue.Enqueue(_enemyObjects[Random.Range(0, _enemyObjects.Count)]);
            } 
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(FightCoroutine());
            }
        }

        private Vector3? GenerateEnemySpawnPosition()
        {
            for (var i = 0; i < 100; i++)
            {
                var position = transform.position;
                var positionX = position.x + Random.Range(-sizeX / 2, sizeX / 2);
                var positionY = position.y + Random.Range(-sizeY / 2, sizeY / 2);
                
                var enemySpawnPosition = new Vector3(positionX, positionY, 0);
                var playerPosition = GameObject.FindWithTag("Player").transform.position;

                if (!((playerPosition - enemySpawnPosition).magnitude > 10)) continue;
                
                var enemies = GameObject.FindGameObjectsWithTag("Enemy");
                var isEnemiesFarEnough = true;
                    
                foreach (var enemy in enemies)
                {
                    if ((enemy.transform.position - enemySpawnPosition).magnitude < 5)
                    {
                        isEnemiesFarEnough = false;
                    }
                }
                    
                if (isEnemiesFarEnough) return enemySpawnPosition;
            }

            return null;
        }

        private IEnumerator SpawnEnemyCoroutine(Vector3 spawnPosition, GameObject enemyObject)
        {
            var spawnCircle = Instantiate(LoadSpawner(), spawnPosition, Quaternion.identity);

            for (var i = 0f; i < 100; i++)
            {
                spawnCircle.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.black, Color.red, i / 100);
                yield return new WaitForSeconds(spawnTime / 100);
            }
            
            Destroy(spawnCircle);
            Instantiate(enemyObject, spawnPosition, Quaternion.identity);

            yield return null;
        }

        private IEnumerator FightCoroutine()
        {
            while (_enemyQueue.Count != 0)
            {
                if (_doorCloser.aliveEnemiesCount == 0)
                {
                    for (var i = 0; i < enemiesInWaveCount; i++)
                    {
                        var enemyObject = _enemyQueue.Dequeue();
                        var enemySpawnPoint = GenerateEnemySpawnPosition();

                        if (enemySpawnPoint == null) continue;

                        StartCoroutine(SpawnEnemyCoroutine((Vector3) enemySpawnPoint, enemyObject));
                    }

                    yield return new WaitForSeconds(spawnTime * 2);
                }

                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
