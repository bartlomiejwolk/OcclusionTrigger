using UnityEngine;
using System.Collections;

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
        #endregion

        #region PROPERTIES

        public Transform TargetTransform {
            get { return targetTransform; }
            set { targetTransform = value; }
        }

        #endregion

    }
}
