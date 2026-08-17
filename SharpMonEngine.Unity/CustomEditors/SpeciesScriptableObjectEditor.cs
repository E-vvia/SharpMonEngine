using SharpMonEngine.Unity.Model;
using UnityEditor;
using UnityEngine;

namespace SharpMonEngine.Unity.CustomEditors
{
    [CustomEditor(typeof(SpeciesScriptableObject))]
    public class SpeciesScriptableObjectEditor : Editor
    {
        private SerializedProperty? _forms;

        private void OnEnable()
        {
            _forms = serializedObject.FindProperty("forms");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawPropertiesExcluding(serializedObject, "forms");

            EditorGUILayout.Space(10);

            EditorGUILayout.LabelField("Forms", EditorStyles.boldLabel);

            for (int i = 0; i < _forms!.arraySize; i++)
            {
                SerializedProperty form = _forms.GetArrayElementAtIndex(i);

                EditorGUILayout.BeginVertical(EditorStyles.helpBox);

                EditorGUILayout.LabelField($"Form {i + 1}", EditorStyles.boldLabel
                );

                DrawForm(form);

                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.Space();

            if (GUILayout.Button("Add Form"))
            {
                int index = _forms.arraySize;
                _forms.InsertArrayElementAtIndex(index);
                SerializedProperty form = _forms.GetArrayElementAtIndex(index);

                form.FindPropertyRelative("Id").intValue = 0;
                form.FindPropertyRelative("Name").stringValue = "SpeciesForm";
                form.FindPropertyRelative("Type1").enumValueIndex = 0;
                form.FindPropertyRelative("Type2").enumValueIndex = 0;
                form.FindPropertyRelative("Height").floatValue = 0f;
                form.FindPropertyRelative("Weight").floatValue = 0f;

                SerializedProperty stats = form.FindPropertyRelative("stats");
                if (stats.arraySize != (int)Stats.Count)
                    stats.arraySize = (int)Stats.Count;

                for (int i = 0; i < stats.arraySize; i++)
                    stats.GetArrayElementAtIndex(i).intValue = 0;
            }

            serializedObject.ApplyModifiedProperties();
        }

        private static void DrawForm(SerializedProperty form)
        {
            DrawProperties(form, "Id", "Name", "Type1", "Type2");

            EditorGUILayout.Space(5);

            EditorGUILayout.LabelField("Base Stats", EditorStyles.boldLabel);

            SerializedProperty stats = form.FindPropertyRelative("stats");

            for (int i = 0; i < (int)Stats.Count; i++)
            {
                EditorGUILayout.PropertyField(stats.GetArrayElementAtIndex(i), new GUIContent(GetStatName((Stats)i)));
            }

            EditorGUILayout.PropertyField(form.FindPropertyRelative("Height"));

            EditorGUILayout.PropertyField(form.FindPropertyRelative("Weight"));
        }

        private static void DrawProperties(SerializedProperty parent, params string[] names)
        {
            foreach (string name in names)
            {
                SerializedProperty property = parent.FindPropertyRelative(name);
                EditorGUILayout.PropertyField(parent.FindPropertyRelative(name));
            }
        }

        private static string GetStatName(Stats stat)
        {
            return stat switch
            {
                Stats.Hp => "HP",
                Stats.Atk => "Attack",
                Stats.Def => "Defense",
                Stats.SpAtk => "Special Attack",
                Stats.SpDef => "Special Defense",
                Stats.Speed => "Speed",
                _ => stat.ToString()
            };
        }
    }
}