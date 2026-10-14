using UnityEditor;

namespace SharpMonEngine.Unity.Extensions
{
    public static class SerializedPropertyExtensions
    {
        public static SerializedProperty FindPropertyRelativeBacking(this SerializedProperty form, string name)
        {
            SerializedProperty property = form.FindPropertyRelative(name) ??
                                          form.FindPropertyRelative($"<{name}>k__BackingField");
            return property;
            ;
        }
    }
}