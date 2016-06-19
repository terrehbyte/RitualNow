using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomPropertyDrawer(typeof(Item), true)]
public class ItemEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        Rect drawRect = position;
        drawRect.height = EditorGUIUtility.singleLineHeight;

        SerializedProperty spriteProperty = property.FindPropertyRelative("image");
        SerializedProperty categoryProperty = property.FindPropertyRelative("tag");

        //EditorGUI.PropertyField(drawRect, spriteProperty);
        EditorGUI.ObjectField(drawRect, "Sprite", spriteProperty.objectReferenceValue, typeof(Sprite), false);
        drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        EditorGUI.PropertyField(drawRect, categoryProperty);

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing * 2;
    }
}
