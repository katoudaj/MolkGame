using UnityEditor;
using UnityEngine;

namespace MolkGame
{
    [CustomEditor(typeof(PinPlacementSystem))]
    public class PinPlacementSystemEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            PinPlacementSystem system = (PinPlacementSystem)target;
            if (GUILayout.Button("Place Pins"))
            {
                system.PlacePins();
                EditorUtility.SetDirty(system);
            }
        }
    }
}
