using Fusion;
using TMPro;
using UnityEngine;

namespace Code.Runtime.Logic
{
    public class TestObject : NetworkBehaviour
    {
        public TestNetworked testNetworked;
        public TextMeshProUGUI scoreText;
    
        [field: SerializeField] [Networked] public int Score { get; private set; }

        public override void Spawned()
        {
            scoreText.text = Score.ToString();
        }

        public override void FixedUpdateNetwork()
        {
            scoreText.text = Score.ToString();
        }
    }
}