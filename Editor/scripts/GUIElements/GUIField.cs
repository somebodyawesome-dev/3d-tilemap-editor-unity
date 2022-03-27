using UnityEditor;
using UnityEngine;

namespace Editor.scripts.GUIElements
{
    public class GUIField
    {
        public static void showField(ref int variable, string label, int minLimit = 0, int maxLimit = 999)
        {
            var style = new GUIStyle(GUI.skin.label) {alignment = TextAnchor.MiddleCenter};

            EditorGUILayout.LabelField(label, style, GUILayout.ExpandWidth(true));

            EditorGUI.BeginDisabledGroup(variable <= minLimit);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("-1"))
            {
                variable -= 1;
            }

            EditorGUI.EndDisabledGroup();
            variable = EditorGUILayout.IntField("", variable);

            EditorGUI.BeginDisabledGroup(variable >= maxLimit);
            //EditorGUILayout.LabelField(selectedTileMapIndex.ToString(), style);
            if (GUILayout.Button("+1"))
            {
                variable += 1;
            }

            EditorGUI.EndDisabledGroup();
            GUILayout.EndHorizontal();
        }

        public static void showField(ref float variable, string label, int minLimit = 0, int maxLimit = 999)
        {
            var style = new GUIStyle(GUI.skin.label) {alignment = TextAnchor.MiddleCenter};

            EditorGUILayout.LabelField(label, style, GUILayout.ExpandWidth(true));

            EditorGUI.BeginDisabledGroup(variable <= minLimit);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("-1"))
            {
                variable -= 1;
            }

            EditorGUI.EndDisabledGroup();
            variable = EditorGUILayout.FloatField("", variable);

            EditorGUI.BeginDisabledGroup(variable >= maxLimit);
            //EditorGUILayout.LabelField(selectedTileMapIndex.ToString(), style);
            if (GUILayout.Button("+1"))
            {
                variable += 1;
            }

            EditorGUI.EndDisabledGroup();
            GUILayout.EndHorizontal();
        }
    }
}