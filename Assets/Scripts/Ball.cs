using System;
using System.Collections;
using UnityEngine;

namespace Scripts
{
    public class Ball : MonoBehaviour, ISpawnObject
    {
        [SerializeField]
        private RangeFloat _destroyRange;

        [SerializeField]
        private RangeFloat _massRange;

        [SerializeField]
        private RangeInt _scoreRange;

        [SerializeField]
        private RangeColor _colorsRange;

        [SerializeField]
        private Renderer _renderer;

        [SerializeField]
        private AudioClip _destroyAudio;

        public AudioClip DestroySound => _destroyAudio;

        public event Action<ISpawnObject> DestroyEvent;

        private float _destroyTime;
        private float _lifeTime;

        public void SetData()
        {
            var rigidBody = gameObject.GetComponent<Rigidbody>();
            rigidBody.mass = RandomRange.GetRandomRange(_massRange);
            _destroyTime = RandomRange.GetRandomRange(_destroyRange);
            StartCoroutine(DestroyCoroutine());
        }

        private void Update()
        {
            transform.localScale += Vector3.one * Time.deltaTime;

            _renderer.material.color =
                Color.Lerp(_colorsRange.MinValue, _colorsRange.MaxValue, _lifeTime / _destroyTime);
            _lifeTime += Time.deltaTime;
        }

        private void OnMouseDown()
        {
            GameManager.AddScore(GetScore(1 - _lifeTime / _destroyTime));
            GameManager.Instance.PlaySound(DestroySound);
            Destroy();
        }

        private int GetScore(float delta)
        {
            return (int) ((_scoreRange.MaxValue - _scoreRange.MinValue) * delta) + _scoreRange.MinValue;
        }

        private IEnumerator DestroyCoroutine()
        {
            yield return new WaitForSeconds(_destroyTime);
            GameManager.AddScore(GetScore(1) * -1);
            GameManager.Instance.PlaySound(DestroySound);
            DestroyEvent?.Invoke(this);
            Destroy();
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}