using Assets.Scripts.Extensions.Inspector.PercentageInspectorDrawer;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Extensions.Inspector
{
    [CustomPropertyDrawer(typeof(PercentageAttribute))]
    public class PercentageDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.Float)
            {
                EditorGUI.BeginProperty(position, label, property);

                // Convertimos el valor a porcentaje
                float percentage = property.floatValue * 100f;

                // Dibujamos el slider y el campo numérico
                Rect sliderPosition = new Rect(position.x, position.y, position.width - 50, position.height);
                percentage = EditorGUI.Slider(sliderPosition, label, percentage, 0f, 100f);

                

                // Guardamos el valor como un flotante entre 0 y 1
                property.floatValue = Mathf.Clamp01(percentage / 100f);



                EditorGUI.EndProperty();
            }
            else
            {
                EditorGUI.LabelField(position, label.text, "Use [Percentage] with float.");
            }
        }
    }
}
