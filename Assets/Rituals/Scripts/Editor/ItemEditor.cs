using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomPropertyDrawer(typeof(Item))]
public class ItemEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        Rect spriteRect = new Rect(position.x, position.y, 150, 150);
        Rect nameRect   = new Rect(position.x+150, position.y, position.width - 150, position.height);

        var imgProp = property.FindPropertyRelative("image");
        var nameProp = property.FindPropertyRelative("tag");

        EditorGUI.ObjectField(spriteRect, imgProp.objectReferenceValue, typeof(Sprite), false);
        //EditorGUI.PropertyField(spriteRect, imgProp, GUIContent.none);
        EditorGUI.PropertyField(nameRect, nameProp, GUIContent.none);

        EditorGUI.EndProperty();
    }
}
