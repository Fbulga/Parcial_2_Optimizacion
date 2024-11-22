using System;
using Controllers;
using UnityEngine;

namespace Managers
{
    public class GameManager : CustomBehaviour
    {
        public static GameManager Instance { get; private set; }
        public Action OnBrickDestroyed;
        public Action OnBrickCreated;
        public Action OnBallsUp;
        public Action OnBallsDown;
        public Action OnHealthUp;
        public Action OnRestart;
        [SerializeField] private int points;
        [SerializeField] private int BricksRemaining;
        [SerializeField] private int Health;
        [SerializeField] private int activeBalls;
        [SerializeField] private PlayerController player;
        public bool ballOnBoard;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        private void OnEnable()
        {
            OnBrickDestroyed += PointsUp;
            OnBrickCreated += AddBrick;
            OnRestart += Restart;
            OnBallsUp += ActiveBallsUp;
            OnBallsDown += ActiveBallsDown;
            OnHealthUp += HealthUp;
        }

        private void OnDisable()
        {
            OnBrickDestroyed -= PointsUp;
            OnBrickCreated -= AddBrick;
            OnRestart -= Restart;
            OnBallsUp -= ActiveBallsUp;
            OnBallsDown -= ActiveBallsDown;
            OnHealthUp -= HealthUp;
        }


        private void PointsUp()
        {
            points++;
            BricksRemaining--;
            if (BricksRemaining <= 0)
            {
                Debug.Log("Game Over, ganaste");
            }
        }

        private void AddBrick()
        {
            BricksRemaining++;
        }

        private void Restart()
        {
            points = 0;
            BricksRemaining = 0;
            Health = 3;
            activeBalls = 1;
        }

        private void HealthUp()
        {
            Health++;
        }

        private void HealthDown()
        {
            Health--;
            ActiveBallsUp();
            player.Parent();
            ballOnBoard = true;
            GameOver();
        }

        public void SetPLayerInstance(PlayerController _player)
        {
            player = _player;
        }

        private void ActiveBallsUp()
        {
            activeBalls++;
        }

        private void ActiveBallsDown()
        {
            activeBalls--;
            if (activeBalls <= 0)
            {
                HealthDown();
            }
        }

        private void GameOver()
        {
            if (Health <= 0) Debug.Log("Game Over, perdiste");
        }
    }
}