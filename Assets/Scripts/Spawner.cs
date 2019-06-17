using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _spawnObject;

        [SerializeField]
        private RangeVector3 _spawnRange;

        [SerializeField]
        private float _spawnDelay;

        private Coroutine _spawnCoroutine;
        private readonly List<ISpawnObject> _spawnedList = new List<ISpawnObject>();

        private void Spawn(GameObject gameObject, Vector3 spawnPosition)
        {
            var spawned = Instantiate(gameObject, spawnPosition, Quaternion.identity);
            var spawnObject  = spawned.GetComponent<ISpawnObject>();
            if (spawnObject == null) return;
            spawnObject.SetData();
            spawnObject.DestroyEvent += DestroySpawned;
            _spawnedList.Add(spawnObject);
        }

        private void DestroySpawned(ISpawnObject obj)
        {
            obj.DestroyEvent -= DestroySpawned;
            _spawnedList.Remove(obj);
        }

        public void StartSpawn()
        {
            _spawnCoroutine = StartCoroutine(StartSpawnCoroutine());
        }
        
        public void StopSpawn()
        {
            if (_spawnCoroutine == null)
                return;
            
            StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
            DestroySpawnedObjects();
        }

        private void DestroySpawnedObjects()
        {
            foreach (var spawnObject in _spawnedList)
            {
                spawnObject.Destroy();
            }
            
            _spawnedList.Clear();
        }

        private IEnumerator StartSpawnCoroutine()
        {
            while (true)
            {
                Spawn(_spawnObject[Random.Range(0, _spawnObject.Length)], RandomRange.GetRandomRange(_spawnRange));
                yield return new WaitForSeconds(_spawnDelay);
            }
        }
    }
}