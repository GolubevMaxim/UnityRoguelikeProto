using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RoguelikeProto.Scripts.Room;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RoguelikeProto.Scripts.Enemies
{
    public class Summoner : MonoBehaviour
    {
        [SerializeField] 
        private int wavesCount;
        [SerializeField]
        private int enemiesInWaveCount;
        
        [SerializeField]
        private int summonPointsCount;
        
        [SerializeField] 
        private int sizeX;
        [SerializeField] 
        private int sizeY;
        
        private DoorCloser _doorCloser;

        private List<GameObject> _enemyObjects;
        private Queue<GameObject> _enemyQueue;
        private List<Vector2> _summonPoints;

        private static GameObject GetPrefabByGuid(string guid)
        {
            return AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(guid));
        }
        
        private List<GameObject> LoadEnemyObj()
        {
            var guids = AssetDatabase.FindAssets("t:prefab", new[] {"Assets/RoguelikeProto/Prefabs/Enemies"});
            
            var enemies = new List<GameObject>();
            if (enemies == null) throw new ArgumentNullException(nameof(enemies));

            enemies.AddRange(guids.Select(GetPrefabByGuid));
            return enemies;
        }

        private void Start()
        {
            _doorCloser = transform.GetComponentInChildren<DoorCloser>();
            _enemyObjects = LoadEnemyObj();
            _summonPoints = new List<Vector2>();
            _enemyQueue = new Queue<GameObject>();

            for (int i = 0; i < summonPointsCount; i++)
            {
                var position = transform.position;
                
                var posX = position.x + Random.Range(-sizeX / 2, sizeX / 2);
                var posY = position.y + Random.Range(-sizeY / 2, sizeY / 2);
                
                _summonPoints.Add(new Vector2(posX, posY));
            }

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

        private IEnumerator FightCoroutine()
        {
            while (_enemyQueue.Count != 0)
            {
                if (_doorCloser.aliveEnemiesCount == 0)
                {
                    for (int i = 0; i < enemiesInWaveCount; i++)
                    {
                        var enemy = _enemyQueue.Dequeue();
                        var pos = _summonPoints[Random.Range(0, summonPointsCount)];
                        Instantiate(enemy, pos, Quaternion.identity);
                    }
                }

                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
