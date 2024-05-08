using System;
using System.Collections.Generic;
using System.Linq;
using Code.Runtime.UI.Windows;
using UnityEngine;

namespace Code.Runtime.Configs
{
    [CreateAssetMenu(fileName = "WindowsConfig", menuName = "Configs/WindowsConfig")]
    public class WindowsConfig : ScriptableObject
    {
        [SerializeField] private GameObject[] windows;

        private void OnValidate()
        {
            if (windows == null) return;

            foreach (GameObject window in windows)
            {
                if (!window.TryGetComponent(out ICloseWindow closeWindow))
                {
                    Debug.LogError($"{window.name} is not window");
                }
            }
        }

        public Dictionary<Type, GameObject> GetWindows() => windows.ToDictionary(
            k => k.GetComponent<ICloseWindow>().GetType(),
            v => v);
    }
}