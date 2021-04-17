using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Mechanics
{
    public class SpawnPoints : MonoBehaviour
    {
        private bool _leftReachedSpawnPoint = false, _rightReachedSpawnPoint = false;
        private Transform[] _spawnPoints; // 1 is left, 2 is right
        private float _detectSpawnPointRadius = 1f;
        public Transform LeftPlayer, RightPlayer;
        public CharactersController CharactersController;
        
        private void Start()
        {
            _spawnPoints =  GetComponentsInChildren<Transform>();
        }

        void Update()
        {
            float leftDist = (_spawnPoints[1].position - LeftPlayer.position).magnitude;
            float rightDist = (_spawnPoints[2].position - RightPlayer.position).magnitude;

            if (leftDist < _detectSpawnPointRadius)
            {
                _leftReachedSpawnPoint = true;
            }

            if (rightDist < _detectSpawnPointRadius)
            {
                _rightReachedSpawnPoint = true;
            }

            if (_leftReachedSpawnPoint && _rightReachedSpawnPoint)
            {
                CharactersController.LeftCurrentSpawnPoint = _spawnPoints[1].position;
                CharactersController.RightCurrentSpawnPoint = _spawnPoints[2].position;
                enabled = false; // Script no longer needed so we disable it
            }

        }
    }
}