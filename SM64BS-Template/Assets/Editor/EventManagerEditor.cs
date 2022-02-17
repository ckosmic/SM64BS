using SM64BS.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SM64BS.TemplateEditor
{
	[CustomEditor(typeof(EventManager))]
	public class EventManagerEditor : Editor
	{
        private EventManager _eventManager;

        private SerializedProperty OnInitialize;
        private SerializedProperty OnDispose;
        private SerializedProperty OnNoteCut;
        private SerializedProperty OnNoteMissed;
        private SerializedProperty OnEnergyDidReach0;

        private Color darkSkinHeaderColor = (Color)new Color32(62, 62, 62, 255);
        private Color lightSkinHeaderColor = (Color)new Color32(194, 194, 194, 255);

        private void OnEnable()
        {
            _eventManager = (EventManager)target;
            OnInitialize = serializedObject.FindProperty("OnInitialize");
            OnDispose = serializedObject.FindProperty("OnDispose");
            OnNoteCut = serializedObject.FindProperty("OnNoteCut");
            OnNoteMissed = serializedObject.FindProperty("OnNoteMissed");
            OnEnergyDidReach0 = serializedObject.FindProperty("OnEnergyDidReach0");
        }

        public override void OnInspectorGUI()
        {
            EditorUtils.DrawScriptHeader("Event Manager");

            GUIStyle foldoutStyle = EditorStyles.foldout;
            FontStyle prevFoldoutFontStyle = foldoutStyle.fontStyle;
            foldoutStyle.fontStyle = FontStyle.Bold;

            EditorGUILayout.Separator();

            _eventManager.sections[0] = EditorGUILayout.Foldout(_eventManager.sections[0], "General Events");

            if (_eventManager.sections[0])
            {
                EditorGUILayout.PropertyField(OnInitialize);
                EditorGUILayout.PropertyField(OnDispose);
                EditorGUILayout.Separator();
            }

            _eventManager.sections[1] = EditorGUILayout.Foldout(_eventManager.sections[1], "Gameplay Events");

            if (_eventManager.sections[1])
            {
                EditorGUILayout.PropertyField(OnNoteCut);
                EditorGUILayout.PropertyField(OnNoteMissed);
                EditorGUILayout.PropertyField(OnEnergyDidReach0);
                EditorGUILayout.Separator();
            }

            serializedObject.ApplyModifiedProperties();
            foldoutStyle.fontStyle = prevFoldoutFontStyle;
        }
    }
}
