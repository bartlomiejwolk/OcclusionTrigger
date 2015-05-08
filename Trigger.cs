using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace OcclusionTrigger {

    /// <summary>
    /// Performs an action when specified transform is occluded.
    /// </summary>
    /// <remarks>
    /// Put it on the camera component.
    /// </remarks>
    public sealed class Trigger : MonoBehaviour {

        #region FIELDS

        [SerializeField]
        private Transform targetTransform;

        [SerializeField]
        private LayerMask layerMask;

        [SerializeField]
        private UnityEvent beginOcclusionEvent;

        [SerializeField]
        private UnityEvent endOcclusionEvent;

        [SerializeField]
        private Transform myTransform;

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Transform to check for occlusion.
        /// </summary>
        public Transform TargetTransform {
            get { return targetTransform; }
            set { targetTransform = value; }
        }

        /// <summary>
        /// Layers that can occlude target transform.
        /// </summary>
        public LayerMask LayerMask {
            get { return layerMask; }
            set { layerMask = value; }
        }

        public UnityEvent BeginOcclusionEvent {
            get { return beginOcclusionEvent; }
            set { beginOcclusionEvent = value; }
        }

        public UnityEvent EndOcclusionEvent {
            get { return endOcclusionEvent; }
            set { endOcclusionEvent = value; }
        }

        /// <summary>
        /// Cached transform component.
        /// </summary>
        private Transform MyTransform {
            get { return myTransform; }
        }

        #endregion

        #region UNITY MESSAGES

        private void Reset() {
            myTransform = GetComponent<Transform>();
        }

        #endregion

        #region METHODS

        private bool CheckTargetTransformOcclusion() {
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

        #endregion

    }
}
