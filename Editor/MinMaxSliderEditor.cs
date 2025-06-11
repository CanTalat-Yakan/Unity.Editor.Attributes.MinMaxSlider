using UnityEditor;
using UnityEngine;

namespace UnityEssentials
{
    [CustomPropertyDrawer(typeof(MinMaxSliderAttribute))]
    public class MinMaxSliderDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.Vector2)
            {
                EditorGUI.HelpBox(position, "MinMaxSlider attribute only supports Vector2 fields.", MessageType.Error);
                return;
            }

            var propertyAttribute = this.attribute as MinMaxSliderAttribute;
            var range = property.vector2Value;

            var labelPosition = new Rect(position.x, position.y, EditorGUIUtility.labelWidth, position.height);
            var minFieldPosition = new Rect(position.x + EditorGUIUtility.labelWidth, position.y, 40, position.height);
            var sliderPosition = new Rect(minFieldPosition.xMax + 2, position.y, position.width - EditorGUIUtility.labelWidth - 84, position.height);
            var maxFieldPosition = new Rect(sliderPosition.xMax + 2, position.y, 40, position.height);

            EditorGUI.LabelField(labelPosition, label);

            var minValue = EditorGUI.FloatField(minFieldPosition, range.x);
            var maxValue = EditorGUI.FloatField(maxFieldPosition, range.y);

            EditorGUI.MinMaxSlider(sliderPosition, ref minValue, ref maxValue, propertyAttribute.Min, propertyAttribute.Max);

            minValue = Mathf.Clamp(minValue, propertyAttribute.Min, maxValue);
            maxValue = Mathf.Clamp(maxValue, minValue, propertyAttribute.Max);

            property.vector2Value = new Vector2(minValue, maxValue);
        }
    }
}
