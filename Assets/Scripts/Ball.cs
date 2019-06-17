using System;
using System.Collections;
using UnityEngine;

namespace Scripts
{
    public class Ball : MonoBehaviour, ISpawnObject
    {
        [SerializeField]
        private RandomRangeFloat _destroyRange;

        [SerializeField]
        private RandomRangeFloat _massRange;
        
        [SerializeField]
        private RandomRangeInt _pointsRange;

        public event Action<ISpawnObject> DestroyEvent;

        public void SetSpawned()
        {
            var rigidBody = gameObject.GetComponent<Rigidbody>();
            rigidBody.mass = RandomRange.GetRandomRange(_massRange);
            StartCoroutine(DestroyCoroutine());
        }

        private void Update()
        {
            transform.localScale += Vector3.one * Time.deltaTime;
        }

        private void OnMouseDown()
        {
            GameManager.AddScore(RandomRange.GetRandomRange(_pointsRange));
            DestroyBall();
        }

        private IEnumerator DestroyCoroutine()
        {
            yield return new WaitForSeconds(RandomRange.GetRandomRange(_destroyRange));
            DestroyBall();
        }

        private void DestroyBall()
        {
            DestroyEvent?.Invoke(this);
            Destroy(gameObject);
        }
    }
}