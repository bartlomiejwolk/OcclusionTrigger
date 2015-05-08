using UnityEditor;
using UnityEngine;

namespace OcclusionTrigger {

    [CustomEditor(typeof (Trigger))]
    public sealed class TriggerEditor : Editor {

        private Trigger Script { get; set; }

        private SerializedProperty targetTransform;

        private void OnEnable() {
            Script = (Trigger) target;

            targetTransform = serializedObject.FindProperty("targetTransform");
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            EditorGUILayout.PropertyField(
                targetTransform,
                new GUIContent(
                    "Target",
                    "Target transform to check for occlusion."));

            serializedObject.ApplyModifiedProperties();
        }

    }

}

