#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace UnityEssentials
{
    [CustomPropertyDrawer(typeof(MinMaxSliderAttribute))]
    public class MinMaxSliderDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            bool isVector2 = property.propertyType == SerializedPropertyType.Vector2;
            bool isVector2Int = property.propertyType == SerializedPropertyType.Vector2Int;

            if (!isVector2 && !isVector2Int)
            {
                EditorGUI.HelpBox(position, "MinMaxSlider attribute only supports Vector2 or Vector2Int fields.", MessageType.Error);
                return;
            }

            var propertyAttribute = this.attribute as MinMaxSliderAttribute;

            var labelPosition = new Rect(position.x, position.y, EditorGUIUtility.labelWidth, position.height);
            var minFieldPosition = new Rect(position.x + EditorGUIUtility.labelWidth, position.y, 40, position.height);
            var sliderPosition = new Rect(minFieldPosition.xMax + 2, position.y, position.width - EditorGUIUtility.labelWidth - 84, position.height);
            var maxFieldPosition = new Rect(sliderPosition.xMax + 2, position.y, 40, position.height);

            EditorGUI.LabelField(labelPosition, label);

            if (isVector2)
            {
                var range = property.vector2Value;
                float minValue = range.x;
                float maxValue = range.y;

                minValue = EditorGUI.FloatField(minFieldPosition, minValue);
                maxValue = EditorGUI.FloatField(maxFieldPosition, maxValue);

                EditorGUI.MinMaxSlider(sliderPosition, ref minValue, ref maxValue, propertyAttribute.Min, propertyAttribute.Max);

                minValue = Mathf.Clamp(minValue, propertyAttribute.Min, maxValue);
                maxValue = Mathf.Clamp(maxValue, minValue, propertyAttribute.Max);

                property.vector2Value = new Vector2(minValue, maxValue);
            }
            else if(isVector2Int)
            {
                var range = property.vector2IntValue;
                int minValue = range.x;
                int maxValue = range.y;

                minValue = EditorGUI.IntField(minFieldPosition, minValue);
                maxValue = EditorGUI.IntField(maxFieldPosition, maxValue);

                float fMin = minValue;
                float fMax = maxValue;
                EditorGUI.MinMaxSlider(sliderPosition, ref fMin, ref fMax, propertyAttribute.Min, propertyAttribute.Max);

                minValue = Mathf.Clamp(Mathf.RoundToInt(fMin), Mathf.RoundToInt(propertyAttribute.Min), maxValue);
                maxValue = Mathf.Clamp(Mathf.RoundToInt(fMax), minValue, Mathf.RoundToInt(propertyAttribute.Max));

                property.vector2IntValue = new Vector2Int(minValue, maxValue);
            }
        }
    }
}
#endif