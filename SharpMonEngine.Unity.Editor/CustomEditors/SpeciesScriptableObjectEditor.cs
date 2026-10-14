#if UNITY_EDITOR
using SharpMonEngine.Unity.Extensions;
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

            DrawForms();

            EditorGUILayout.Space();

            AddForm();

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawForms()
        {
            for (int i = 0; i < _forms!.arraySize; i++)
            {
                SerializedProperty form = _forms.GetArrayElementAtIndex(i);

                EditorGUILayout.BeginVertical(EditorStyles.helpBox);

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField($"Form {i + 1}", EditorStyles.boldLabel);

                GUILayout.FlexibleSpace();

                EditorGUILayout.EndHorizontal();

                if (DeleteForm(i))
                {
                    continue;
                }

                DrawForm(form);

                EditorGUILayout.EndVertical();
            }
        }

        private bool DeleteForm(int index)
        {
            if (!GUILayout.Button("Delete", GUILayout.Width(60)))
            {
                return false;
            }

            if (!EditorUtility.DisplayDialog(
                    "Delete Form",
                    $"Are you sure you want to delete Form {index + 1}?",
                    "Delete",
                    "Cancel"))
            {
                return false;
            }

            _forms!.DeleteArrayElementAtIndex(index);
            return true;
        }

        private static void DrawForm(SerializedProperty form)
        {
            DrawProperties(form, "Id", "Name", "Type1", "Type2");

            EditorGUILayout.Space(5);

            EditorGUILayout.LabelField("Base Stats", EditorStyles.boldLabel);

            SerializedProperty stats = form.FindPropertyRelativeBacking("stats");

            for (int i = 0; i < (int)Stats.Count; i++)
            {
                EditorGUILayout.PropertyField(stats.GetArrayElementAtIndex(i), new GUIContent(GetStatName((Stats)i)));
            }

            EditorGUILayout.PropertyField(form.FindPropertyRelativeBacking("Height"));

            EditorGUILayout.PropertyField(form.FindPropertyRelativeBacking("Weight"));
        }

        private void AddForm()
        {
            if (!GUILayout.Button("Add Form"))
            {
                return;
            }

            int index = _forms!.arraySize;
            _forms.InsertArrayElementAtIndex(index);
            SerializedProperty form = _forms.GetArrayElementAtIndex(index);

            form.FindPropertyRelativeBacking("Id").intValue = 0;
            form.FindPropertyRelativeBacking("Name").stringValue = "SpeciesForm";
            form.FindPropertyRelativeBacking("Type1").enumValueIndex = 0;
            form.FindPropertyRelativeBacking("Type2").enumValueIndex = 0;
            form.FindPropertyRelativeBacking("Height").floatValue = 0f;
            form.FindPropertyRelativeBacking("Weight").floatValue = 0f;

            SerializedProperty stats = form.FindPropertyRelativeBacking("stats");
            if (stats.arraySize != (int)Stats.Count)
                stats.arraySize = (int)Stats.Count;

            for (int i = 0; i < stats.arraySize; i++)
                stats.GetArrayElementAtIndex(i).intValue = 0;
        }

        private static void DrawProperties(SerializedProperty parent, params string[] names)
        {
            foreach (string name in names)
            {
                SerializedProperty property = parent.FindPropertyRelativeBacking(name);
                EditorGUILayout.PropertyField(parent.FindPropertyRelativeBacking(name));
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
#endif