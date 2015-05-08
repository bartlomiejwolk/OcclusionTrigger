using UnityEditor;
using UnityEngine;

namespace OcclusionTrigger {

    [CustomEditor(typeof (Trigger))]
    public sealed class TriggerEditor : Editor {

        #region FIELDS

        private Trigger Script { get; set; }

        #endregion

        #region SERIALIZED PROPERTIES

        private SerializedProperty targetTransform;
        private SerializedProperty layerMask;
        private SerializedProperty beginOcclusionEvent;
        private SerializedProperty endOcclusionEvent;

        #endregion

        #region UNITY MESSAGES
        private void OnEnable() {
            Script = (Trigger) target;

            targetTransform = serializedObject.FindProperty("targetTransform");
            layerMask = serializedObject.FindProperty("layerMask");
            beginOcclusionEvent =
                serializedObject.FindProperty("beginOcclusionEvent");
            endOcclusionEvent =
                serializedObject.FindProperty("endOcclusionEvent");
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            DrawVersionLabel();
            DrawTargetField();
            DrawLayerMaskDropdown();

            EditorGUILayout.Space();

            DrawBeginOcclusionEventField();
            DrawEndOcclusionEventField();

            serializedObject.ApplyModifiedProperties();
        }

        #endregion

        #region INSPECTOR

        private void DrawVersionLabel() {
            EditorGUILayout.LabelField(
                string.Format(
                    "{0} ({1})",
                    Trigger.VERSION,
                    Trigger.EXTENSION));
        }
 
        private void DrawEndOcclusionEventField() {

            EditorGUILayout.PropertyField(
                endOcclusionEvent,
                new GUIContent(
                    "Occlusion End",
                    "Action to execute when target stops being occluded."));
        }

        private void DrawBeginOcclusionEventField() {

            EditorGUILayout.PropertyField(
                beginOcclusionEvent,
                new GUIContent(
                    "Occlusion Start",
                    "Action to execute when target starts being occluded."));
        }

        private void DrawLayerMaskDropdown() {

            EditorGUILayout.PropertyField(
                layerMask,
                new GUIContent(
                    "Layer Mask",
                    "Layers that can occlude target transform."));
        }

        private void DrawTargetField() {

            EditorGUILayout.PropertyField(
                targetTransform,
                new GUIContent(
                    "Target",
                    "Target transform to check for occlusion."));
        }

        #endregion

        #region METHODS

        [MenuItem("Component/OcclusionTrigger")]
        private static void AddTriggerComponent() {
            if (Selection.activeGameObject != null) {
                Selection.activeGameObject.AddComponent(typeof(Trigger));
            }
        }
 
        #endregion
    }

}

