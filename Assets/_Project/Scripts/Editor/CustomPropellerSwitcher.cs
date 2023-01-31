using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PropellerSwithcer))]
[CanEditMultipleObjects]
public class CustomPropellerSwitcher : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PropellerSwithcer propellerSwithcer = (PropellerSwithcer)target;

        if (GUILayout.Button("SwitchBlades"))
        {
            propellerSwithcer.SwitchBlades();
        }
    }
}
