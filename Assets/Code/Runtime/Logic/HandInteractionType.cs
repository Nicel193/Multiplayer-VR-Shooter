using UnityEngine;

namespace Code.Runtime.Logic
{
    public class HandInteractionType : MonoBehaviour
    {
        [field: SerializeField] public HandType HandType { get; private set; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out AttachPositionHandler attachPositionHandler))
            {
                attachPositionHandler.SetAttach(this);
            }
        }
    }

    public enum HandType
    {
        Left,
        Right
    }
}