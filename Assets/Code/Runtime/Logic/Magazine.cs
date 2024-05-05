using UnityEngine;

namespace Code.Runtime.Logic
{
    public class Magazine : MonoBehaviour
    {
        public int AmmoCount { get; private set; } = 30;

        public void UseAmmo() =>
            AmmoCount--;

        public bool HasAmmo() =>
            AmmoCount > 0;
    }
}