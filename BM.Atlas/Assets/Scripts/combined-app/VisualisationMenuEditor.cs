
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Graph;

[CustomEditor(typeof(VisualisationMenu))]
public class VisualisationMenuEditor : Editor
{

    public override void OnInspectorGUI()
    {
        VisualisationMenu targetObj = (VisualisationMenu)target;
        SerializedObject so = new SerializedObject(targetObj);
        so.Update();

        SerializedProperty mapProp = so.FindProperty("maps");
        EditorGUILayout.PropertyField(mapProp, new GUIContent("Maps"), true);
        SerializedProperty visProp = so.FindProperty("firstVisualisationSelectedPerMap");
        EditorGUILayout.PropertyField(visProp, new GUIContent("First Visualisation Selected On Launch, per map"), true);

        targetObj.combinedAtlas = EditorGUILayout.Toggle("Combined Atlas", targetObj.combinedAtlas);

        if (!targetObj.combinedAtlas)
        { 
            SerializedProperty menuProp = so.FindProperty("menus");
            EditorGUILayout.PropertyField(menuProp, new GUIContent("Menus, per map"), true); 
        }

        targetObj.colorMapVisualisation = EditorGUILayout.Toggle("Color Mapping", targetObj.colorMapVisualisation);

        if (targetObj.colorMapVisualisation)
        {
            targetObj.colorMapper = (ColorMapper) EditorGUILayout.ObjectField("Color Mapper Obj", targetObj.colorMapper, typeof(ColorMapper), true);
            targetObj.palette = (ColorPalette)EditorGUILayout.ObjectField("Color Palette Obj", targetObj.palette, typeof(ColorPalette), true);
 
        }
        targetObj.regionVisualisation = EditorGUILayout.Toggle("Regions", targetObj.regionVisualisation);

        so.ApplyModifiedProperties();
    }
}
#endif