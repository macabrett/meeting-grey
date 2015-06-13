namespace MeetingGrey.Unity.Depth {

    using System.Collections;
    using BrettMStory.Unity;
    using MeetingGrey.Unity.Constants;
    using UnityEngine;

    /// <summary>
    /// Controls depth.
    /// </summary>
    public class DepthController : BaseBehaviour {

        /// <summary>
        /// Backing field.
        /// </summary>
        private static DepthController _instance;

        /// <summary>
        /// The parent object of all background objects.
        /// </summary>
        private GameObject _backgroundParent;

        /// <summary>
        /// The parent object of all foreground objects.
        /// </summary>
        private GameObject _foregroundParent;

        /// <summary>
        /// The surface layer mask.
        /// </summary>
        private LayerMask[] _surfaceLayerMasks = new LayerMask[2];

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static DepthController Instance {
            get {
                if (DepthController._instance == null) {
                    var depthController = GameObject.Instantiate(new GameObject("DepthController"));
                    DepthController._instance = depthController.AddComponent<DepthController>();
                }

                return DepthController._instance;
            }
        }

        /// <summary>
        /// Gets the surface layer mask.
        /// </summary>
        /// <value>
        /// The surface layer mask.
        /// </value>
        public LayerMask SurfaceLayerMask {
            get {
                return this._surfaceLayerMasks[0];
            }
        }

        /// <summary>
        /// Tries the swap depths.
        /// </summary>
        public void TrySwap() {
            if (this.IsBusy) {
                return;
            }

            var newFirst = this._surfaceLayerMasks[1];
            this._surfaceLayerMasks[1] = this._surfaceLayerMasks[0];
            this._surfaceLayerMasks[0] = newFirst;
        }

        /// <summary>
        /// Awakes this instance.
        /// </summary>
        private void Awake() {
            if (DepthController._instance == null) {
                DepthController._instance = this;
            }

            this._surfaceLayerMasks[0] = LayerConstants.SurfaceForegroundLayerMask;
            this._surfaceLayerMasks[1] = LayerConstants.SurfaceBackgroundLayerMask;

            this._backgroundParent = GameObject.FindGameObjectWithTag(TagConstants.SurfaceBackgroundParent);
            this._foregroundParent = GameObject.FindGameObjectWithTag(TagConstants.SurfaceForegroundParent);
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        private void Update() {
            if (Input.GetButtonDown(InputConstants.Swap)) {
                this.TrySwap();
            }
        }
    }
}