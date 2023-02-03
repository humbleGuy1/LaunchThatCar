using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Block))]
public class BlockEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Block block = (Block)target;

        GUILayout.Space(20);
        EditorGUILayout.LabelField("RespawnPoint");
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Add"))
        {
            block.AddRespawnPoint();
        }

        if (GUILayout.Button("Remove"))
        {
            block.RemoveRespawnPoint();
        }

        GUILayout.EndHorizontal();

        EditorGUILayout.LabelField("Ramp");
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Add"))
        {
            block.AddRamp();
        }

        if (GUILayout.Button("Remove"))
        {
            block.RemoveRamp();
        }

        GUILayout.EndHorizontal();
    }
}
