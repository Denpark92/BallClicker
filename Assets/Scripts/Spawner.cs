using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject _spawnObject;

        [SerializeField]
        private RandomRangeVector3 _spawnRange;

        [SerializeField]
        private float _spawnDelay;

        private Coroutine _spawnCoroutine;
        private readonly List<ISpawnObject> _spawnedList = new List<ISpawnObject>();

        private void Spawn(GameObject gameObject, Vector3 spawnPosition)
        {
            var spawned = Instantiate(gameObject, spawnPosition, Quaternion.identity);
            var spawnObject  = spawned.GetComponent<ISpawnObject>();
            if (spawnObject == null) return;
            spawnObject.SetSpawned();
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
        }

        private IEnumerator StartSpawnCoroutine()
        {
            while (true)
            {
                Spawn(_spawnObject, RandomRange.GetRandomRange(_spawnRange));
                yield return new WaitForSeconds(_spawnDelay);
            }
        }
    }
}