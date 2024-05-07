using Code.Runtime.Logic;
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
    [CustomEditor(typeof(PlayerSpawner))]
    public class PlayerSpawnerEditor : UnityEditor.Editor
    {
        void OnSceneGUI()
        {
            PlayerSpawner playerSpawner = (PlayerSpawner) target;

            Handles.color = Color.green;
            Handles.DrawWireDisc(playerSpawner.transform.position, new Vector3(0, 1, 0), playerSpawner.Radius);
        }
    }
}