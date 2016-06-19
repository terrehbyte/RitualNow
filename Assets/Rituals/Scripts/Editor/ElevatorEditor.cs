using UnityEngine;
using UnityEditor;
using System.Collections;

using RitualWarehouse;

[CustomEditor(typeof(Elevator))]
public class ElevatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var script = (Elevator)target;

        if(GUILayout.Button("Toggle") && Application.isPlaying)
        {
            script.Toggle();
        }
    }
}
