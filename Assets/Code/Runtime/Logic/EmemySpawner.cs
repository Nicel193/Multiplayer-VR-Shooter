using Fusion;
using UnityEngine;

namespace Code.Runtime.Logic
{
    public class EmemySpawner : NetworkBehaviour
    {
        public SimpleAI enemyPrefab;
        public Transform spawnPosition;
    
        private Transform _player;

        public void Initialize(Transform player)
        {
            _player = player;
        
            Spawn();
        }

        private void Spawn()
        {
            SimpleAI simpleAI = Runner.Spawn(enemyPrefab, spawnPosition.position, Quaternion.identity);
        
            simpleAI.Initialize(_player);
        }
    }
}