using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RTLText))]
public class RtlTextEditor : Editor
{
    private RTLText rtltext;

    void OnEnable()
    {
        // On editor enable assign the current text component to rtl text script
        rtltext = (RTLText)target;
        rtltext.Start();
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Space(15);

        GUILayout.Label("Press convert button to apply RTL format");
        if (GUILayout.Button("Convert", GUILayout.MinHeight(30)))
        {
            // Store the current text value in undo registrey
            Undo.RecordObject(rtltext.textComponent, rtltext.textComponent.text);

            // Convert text to RTL format
            rtltext.Convert();
        }
    }
}