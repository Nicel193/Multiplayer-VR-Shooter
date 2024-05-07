using Code.Runtime.Logic;
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
    [CustomEditor(typeof(PlayerSpawnPosition))]
    public class PlayerSpawnerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void DrawCustomGizmo(PlayerSpawnPosition playerSpawnPosition, GizmoType gizmoType)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(playerSpawnPosition.transform.position, playerSpawnPosition.Radius);
        }
    }
}