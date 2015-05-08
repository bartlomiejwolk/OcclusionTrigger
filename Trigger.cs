// Copyright (c) 2015 Bartlomiej Wolk (bartlomiejwolk@gmail.com)
// 
// This file is part of the OcclusionTrigger extension for Unity. Licensed
// under the MIT license. See LICENSE file in the project root folder.

using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace OcclusionTrigger {

    /// <summary>
    ///     Performs an action when specified transform is occluded.
    /// </summary>
    /// <remarks>Put it on the camera component.</remarks>
    public sealed class Trigger : MonoBehaviour {
        #region CONSTANTS

        public const string EXTENSION = "OcclusionTrigger";
        public const string VERSION = "v0.1.0";

        #endregion CONSTANTS

        #region FIELDS

        [SerializeField]
        private UnityEvent beginOcclusionEvent;

        [SerializeField]
        private UnityEvent endOcclusionEvent;

        [SerializeField]
        private LayerMask layerMask;

        [SerializeField]
        private Transform targetTransform;

        #endregion FIELDS

        #region PROPERTIES

        /// <summary>
        /// Actions executed when target transform gets occluded.
        /// </summary>
        public UnityEvent BeginOcclusionEvent {
            get { return beginOcclusionEvent; }
            set { beginOcclusionEvent = value; }
        }

        /// <summary>
        /// Actions executed when target transform stops being occluded.
        /// </summary>
        public UnityEvent EndOcclusionEvent {
            get { return endOcclusionEvent; }
            set { endOcclusionEvent = value; }
        }

        /// <summary>
        ///     Layers that can occlude target transform.
        /// </summary>
        public LayerMask LayerMask {
            get { return layerMask; }
            set { layerMask = value; }
        }

        /// <summary>
        ///     Transform to check for occlusion.
        /// </summary>
        public Transform TargetTransform {
            get { return targetTransform; }
            set { targetTransform = value; }
        }

        /// <summary>
        ///     Cached transform component.
        /// </summary>
        private Transform MyTransform { get; set; }

        #endregion PROPERTIES

        #region UNITY MESSAGES

        private void Awake() {
            MyTransform = GetComponent<Transform>();
        }

        private void Start() {
            StartCoroutine(HandleExecuteAction());
        }

        #endregion UNITY MESSAGES

        #region METHODS

        /// <summary>
        ///     Checks if target occlusion changed and executes corresponding
        ///     action.
        /// </summary>
        /// <returns></returns>
        private IEnumerator HandleExecuteAction() {
            var prevOccluded = false;

            while (true) {
                var occluded = TargetOccluded();

                if (occluded != prevOccluded) {
                    InvokeOcclusionEvent(occluded);
                }

                prevOccluded = occluded;

                yield return null;
            }
        }

        private void InvokeOcclusionEvent(bool occluded) {
            if (occluded) {
                BeginOcclusionEvent.Invoke();
            }
            else {
                endOcclusionEvent.Invoke();
            }
        }

        private bool TargetOccluded() {
            // Get distance for raycast.
            var tDist =
                (TargetTransform.position - MyTransform.position).magnitude
                    // Ray length decreased by 0.1 to not hit the floor.
                - 0.1f;
            // Get direction for raycast.
            var tDir =
                (TargetTransform.position - MyTransform.position).normalized;

            RaycastHit hit;

            var occluded = Physics.Raycast(
                MyTransform.position,
                tDir,
                out hit,
                tDist,
                LayerMask);

            return occluded;
        }

        #endregion METHODS
    }

}