using System;
using Fusion;
using UnityEngine;

namespace Code.Runtime.Logic
{
    [Serializable]
    public class TestNetworked
    {
        [field: SerializeField] [Networked] public int Score { get; private set; }
    }
}