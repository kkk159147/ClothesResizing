using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TransformToJSON))]
public class TransformToJSONButton : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        TransformToJSON generator = (TransformToJSON)target;
        if (GUILayout.Button("Convert"))
        {
            generator.SaveTransformDataToJson();
        }
    }
}