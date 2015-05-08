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

        #endregion

    }
}
