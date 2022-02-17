using SM64BS.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SM64BS.TemplateEditor
{
	[CustomEditor(typeof(EventTester))]
	public class EventTesterEditor : Editor
	{
        private EventTester _eventTester;

        private Color darkSkinHeaderColor = (Color)new Color32(62, 62, 62, 255);
        private Color lightSkinHeaderColor = (Color)new Color32(194, 194, 194, 255);

        private void OnEnable()
        {
            _eventTester = (EventTester)target;
        }

        public override void OnInspectorGUI()
        {
            EditorUtils.DrawScriptHeader("Event Tester");

            GUIStyle foldoutStyle = EditorStyles.foldout;
            FontStyle prevFoldoutFontStyle = foldoutStyle.fontStyle;
            foldoutStyle.fontStyle = FontStyle.Bold;

            EditorGUILayout.Separator();

            if (GUILayout.Button("OnInitialize"))
            {
                _eventTester.InvokeUnityEvent("OnInitialize");
            }
            if (GUILayout.Button("OnDispose"))
            {
                _eventTester.InvokeUnityEvent("OnDispose");
            }

            EditorGUILayout.Separator();

            if (GUILayout.Button("OnNoteCut"))
            {
                _eventTester.InvokeUnityEvent("OnNoteCut");
            }
            if (GUILayout.Button("OnNoteMissed"))
            {
                _eventTester.InvokeUnityEvent("OnNoteMissed");
            }
            if (GUILayout.Button("OnEnergyDidReach0"))
            {
                _eventTester.InvokeUnityEvent("OnEnergyDidReach0");
            }

            serializedObject.ApplyModifiedProperties();
            foldoutStyle.fontStyle = prevFoldoutFontStyle;
        }
    }
}
