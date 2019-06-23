using System;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

namespace Scripts
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Spawner))]
    public class GameManager : MonoBehaviour
    {
        private GameManager()
        {
        }

        public static GameManager Instance;
        
        [SerializeField]
        private GameObject _playButton;
        
        [SerializeField]
        private GameObject _stopButton;
        
        [SerializeField]
        private TextMeshProUGUI _scoreText;
        
        private Spawner _spawner;
        private AudioSource _audioSource;

        private int _score;
        public static Action<int> AddScore;

        private void Awake()
        {
            if (Instance != null)
                return;

            Instance = this;
        }

        private void OnEnable()
        {
            _spawner = GetComponent<Spawner>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnDestroy()
        {
            Instance = null;
        }
        
        public void PlaySound(AudioClip clip)
        {
            _audioSource.PlayOneShot(clip);
        }

        [UsedImplicitly]
        public void StartGame()
        {
            ResetScore();
            AddScore = SetScore;
            _spawner.StartSpawn();
            SetButtons(false);
        }

        [UsedImplicitly]
        public void StopGame()
        {
            _spawner.StopSpawn();
            SetButtons(true);
        }

        private void SetButtons(bool start)
        {
            _playButton.SetActive(start);
            _stopButton.SetActive(!start);
        }
        
        private void ResetScore()
        {
            _score = 0;
            _scoreText.text = _score.ToString();
        }

        private void SetScore(int points)
        {
            _score += points;
            _scoreText.text = _score.ToString();
        }
    }
}