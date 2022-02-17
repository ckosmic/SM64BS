using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SM64BS.TemplateEditor
{
    public class EditorUtils
    {
        public static void DrawScriptHeader(string header)
        {
            GUIStyle labelStyle = EditorStyles.label;
            FontStyle prevLabelFontStyle = labelStyle.fontStyle;
            int prevLabelFontSize = labelStyle.fontSize;
            float prevLabelHeight = labelStyle.fixedHeight;
            TextAnchor prevLabelAlign = labelStyle.alignment;
            labelStyle.alignment = TextAnchor.MiddleCenter;
            labelStyle.fontStyle = FontStyle.Bold;
            labelStyle.fontSize = 20;
            labelStyle.fixedHeight = 25;

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField(header, labelStyle);
            EditorGUILayout.Space(10);

            labelStyle.fontStyle = prevLabelFontStyle;
            labelStyle.fontSize = prevLabelFontSize;
            labelStyle.fixedHeight = prevLabelHeight;
            labelStyle.alignment = prevLabelAlign;
        }
    }
}
