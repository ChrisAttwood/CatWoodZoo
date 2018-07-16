using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomPropertyDrawer(typeof(FootPrintData))]
public class FootPrintDrawer : PropertyDrawer
{


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Rect newposition = position;
        newposition.y += 80f;
        SerializedProperty data = property.FindPropertyRelative("rows");

        for (int y = 0; y < 5; y++)
        {
            SerializedProperty row = data.GetArrayElementAtIndex(y).FindPropertyRelative("row");
            newposition.height = 20f;

            if (row.arraySize != 5)
                row.arraySize = 5;

            newposition.width = 20f;

            for (int x = 0; x < 5; x++)
            {
                EditorGUI.PropertyField(newposition, row.GetArrayElementAtIndex(x), GUIContent.none);
                newposition.x += newposition.width;
            }
            newposition.x = position.x;
            newposition.y -= 20f;
        }

       

    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 20f * 5;
    }
}