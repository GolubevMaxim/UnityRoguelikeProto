using System.Collections.Generic;
using System.Drawing;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace RoguelikeProto.Scripts.LevelGenerator
{
    public class Room
    {
        public bool Exists;
        public bool[] Doors;

        public Room(bool exists, bool[] doors)
        {
            Exists = exists;
            Doors = doors;
        }
    }
    public class Map
    {
        private List<List<Room>> _map;
        private int _roomsCount;

        public List<List<Room>> GetMap()
        {
            return _map;
        }
        public Map(int roomsCount)
        {
            _roomsCount = roomsCount;
            
            _map = new List<List<Room>>();
            
            for (int i = 0; i < 10; i++)
            {
                _map.Add(new List<Room>());
                for (int j = 0; j < 10; j++)
                {
                    _map[i].Add(new Room(true, new []{false, false, false, false}));
                }
            }

            while (true)
            {
                var roomRemoved = RemoveRandomRoom();
                var curRoomNum = RoomCounter(5, 0);

                if (curRoomNum < _roomsCount)
                {
                    _map[roomRemoved.Y][roomRemoved.X].Exists = true;
                }
                if (curRoomNum == _roomsCount)
                {
                    break;
                }
            }
            RemoveUselessRooms(5, 0);
            SetupRooms(5, 0);
        }

        private Point RemoveRandomRoom()
        {
            var x = Random.Range(0, 10);
            var y = Random.Range(0, 10);

            while (!_map[y][x].Exists || (x == 5 && y == 0))
            {
                
                x = Random.Range(0, 10);
                y = Random.Range(0, 10);
            }

            _map[y][x].Exists = false;
            return new Point(x, y);
        }

        private int RoomCounter(int x, int y)
        {
            var visited = new bool[10][];
            
            for (var i = 0; i < 10; i++)
            {
                visited[i] = new bool[10];
                for (var j = 0; j < 10; j++)
                {
                    visited[i][j] = false;
                }
            }
            var queue = new Queue<Point>();
            queue.Enqueue(new Point(x, y));
            visited[y][x] = true;
            
            var countedRooms = 1;
            while (queue.Count != 0)
            {
                var room = queue.Dequeue();

                foreach (var delta in new[] {new[] {0, 1}, new[] {0, -1}, new[] {1, 0}, new[] {-1, 0}})
                {
                    var dx = delta[0];
                    var dy = delta[1];

                    var newX = room.X + dx;
                    var newY = room.Y + dy;

                    if ((newX is >= 0 and < 10) && newY is >= 0 and < 10)
                    {
                        if (!visited[newY][newX] && _map[newY][newX].Exists)
                        {
                            queue.Enqueue(new Point(newX, newY));
                            visited[newY][newX] = true;

                            countedRooms++;
                        }
                    }
                }
            }

            return countedRooms;
        }

        private void RemoveUselessRooms(int x, int y)
        {
            var visited = new bool[10][];
            
            for (var i = 0; i < 10; i++)
            {
                visited[i] = new bool[10];
                for (var j = 0; j < 10; j++)
                {
                    visited[i][j] = false;
                }
            }
            
            var queue = new Queue<Point>();
            queue.Enqueue(new Point(x, y));
            visited[y][x] = true;
            
            while (queue.Count != 0)
            {
                var room = queue.Dequeue();

                foreach (var delta in new[] {new[] {0, 1}, new[] {0, -1}, new[] {1, 0}, new[] {-1, 0}})
                {
                    var dx = delta[0];
                    var dy = delta[1];

                    var newX = room.X + dx;
                    var newY = room.Y + dy;

                    if ((newX is >= 0 and < 10) && newY is >= 0 and < 10)
                    {
                        if (!visited[newY][newX] && _map[newY][newX].Exists)
                        {
                            queue.Enqueue(new Point(newX, newY));
                            visited[newY][newX] = true;
                        }
                    }
                }
            }

            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    if (!visited[i][j])
                    {
                        _map[i][j].Exists = false;
                    }
                }
            }
        }
        
        private void SetupRooms(int x, int y)
        {
            var visited = new bool[10][];
            
            for (var i = 0; i < 10; i++)
            {
                visited[i] = new bool[10];
                for (var j = 0; j < 10; j++)
                {
                    visited[i][j] = false;
                }
            }
            
            var queue = new Queue<Point>();
            queue.Enqueue(new Point(x, y));
            GenerateRoomAt(x, y);
            visited[y][x] = true;
            
            while (queue.Count != 0)
            {
                var room = queue.Dequeue();

                foreach (var delta in new[] {new[] {0, 1}, new[] {0, -1}, new[] {1, 0}, new[] {-1, 0}})
                {
                    var dx = delta[0];
                    var dy = delta[1];

                    var newX = room.X + dx;
                    var newY = room.Y + dy;

                    if ((newX is >= 0 and < 10) && newY is >= 0 and < 10)
                    {
                        if (!visited[newY][newX] && _map[newY][newX].Exists)
                        {
                            queue.Enqueue(new Point(newX, newY));
                            visited[newY][newX] = true;
                            
                            GenerateRoomAt(newX, newY);
                        }
                    }
                }
            }
        }

        private void GenerateRoomAt(int x, int y)
        {
            _map[y][x].Exists = true;
            
            foreach (var delta in new[] {new[] {0, 1}, new[] {0, -1}, new[] {1, 0}, new[] {-1, 0}})
            {
                var dx = delta[0];
                var dy = delta[1];
                
                var newX = x + dx;
                var newY = y + dy;
                
                if ((newX is >= 0 and < 10) && (newY is >= 0 and < 10))
                {
                    if (_map[newY][newX].Exists)
                    {
                        _map[y][x].Doors[Mathf.Abs(dx) * (-dx + 1) + Mathf.Abs(dy) * (-dy + 2)] = true;
                    }
                }
            }
        }
    }

    public class LevelGenerator : MonoBehaviour
    {
        private static int _roomN = 20;

        private static GameObject GetPrefabByGuid(string guid)
        {
            return AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(guid));
        }
        private static void Generate()
        {
            string[] guids = AssetDatabase.FindAssets("t:prefab", new[] {"Assets/RoguelikeProto/Prefabs/Rooms"});
            List<GameObject> rooms = new List<GameObject>();
            
            foreach (string guid in guids)
            {
                rooms.Add(GetPrefabByGuid(guid));
            }

            var map = new Map(_roomN);
            var roomsMap = map.GetMap();

            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    if (roomsMap[y][x].Exists)
                    {
                        var doors = roomsMap[y][x].Doors;
                        var roomIndex = 0;
                        for (int i = 0; i < 4; i++)
                        {
                            if (doors[i])
                            {
                                roomIndex += (int)Mathf.Pow(2, i);
                            }
                        }
                        var room = Object.Instantiate(rooms[roomIndex - 1], new Vector3((x - 5) * 50, y * 50, 0), Quaternion.identity);
                        
                        if (x == 5 && y == 0)
                        {
                            foreach (Transform child in room.transform)
                            {
                                if (child.CompareTag("Floor"))
                                {
                                    Destroy(child.GetComponent<Collider2D>());
                                }
                            }
                        }
                    }
                }
            } 
        }

        private void Awake()
        {
            Generate();
        }
    }
}
