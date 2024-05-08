using UnityEngine;

namespace Code.Runtime.Logic
{
    public class PlayerSpawnPosition : MonoBehaviour
    {
        [field: SerializeField] public float Radius { get; private set; }

        public Vector3 GetSpawnPosition()
        {
            Vector2 insideUnitCircle = Random.insideUnitCircle * Radius;
            
            return transform.position + new Vector3(insideUnitCircle.x, 0f, insideUnitCircle.y);
        }
    }
}