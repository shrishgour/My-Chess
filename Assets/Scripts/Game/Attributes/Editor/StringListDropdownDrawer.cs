using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Game.Attribute
{
    [CustomPropertyDrawer(typeof(StringListDropdownAttribute))]
    public class StringListDropdownDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            StringListDropdownAttribute dropdownAttribute = attribute as StringListDropdownAttribute;
            // Get the type based on the class name provided in the attribute
            Type classType = Type.GetType(dropdownAttribute.className + ", Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");

            if (classType != null)
            {
                // Get all fields of the class
                FieldInfo[] fields = classType.GetFields();

                // Create a list to hold all the string values
                List<string> stringValues = new List<string>();

                // Add all the string values from the fields of the class
                foreach (FieldInfo field in fields)
                {
                    if (field.FieldType == typeof(string))
                    {
                        stringValues.Add(field.GetValue(null) as string);
                    }
                }

                // Get the index of the currently selected string
                int selectedIndex = Mathf.Max(0, stringValues.IndexOf(property.stringValue));

                // Draw the dropdown list
                selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, stringValues.ToArray());

                // Set the selected string based on the index
                property.stringValue = stringValues[selectedIndex];
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);
            }

            EditorGUI.EndProperty();
        }
    }
}
