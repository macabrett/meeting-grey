namespace MeetingGrey.Unity.Depth {

    using System.Collections;
    using BrettMStory.Unity;
    using MeetingGrey.Unity.Constants;
    using MeetingGrey.Unity.Player;
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
        /// The offset of the background.
        /// </summary>
        private Vector3 _backgroundOffset = new Vector3(-0.5f, 0.5f, 1.0f);

        /// <summary>
        /// The parent object of all background objects.
        /// </summary>
        private GameObject _backgroundParent;

        /// <summary>
        /// The background screen.
        /// </summary>
        private GameObject _backgroundScreen;

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

            if (this._surfaceLayerMasks[0] == LayerConstants.SurfaceForegroundLayerMask) {
                this._backgroundParent.transform.position += this._backgroundOffset;
                this._foregroundParent.transform.position -= this._backgroundOffset;
            } else {
                this._backgroundParent.transform.position -= this._backgroundOffset;
                this._foregroundParent.transform.position += this._backgroundOffset;
            }
        }

        /// <summary>
        /// Awakes this instance.
        /// </summary>
        private void Awake() {
            if (DepthController._instance == null) {
                DepthController._instance = this;
            }

            this._backgroundScreen = GameObject.FindGameObjectWithTag(TagConstants.BackgroundScreen);

            this._surfaceLayerMasks[0] = LayerConstants.SurfaceForegroundLayerMask;
            this._surfaceLayerMasks[1] = LayerConstants.SurfaceBackgroundLayerMask;

            this._backgroundParent = GameObject.FindGameObjectWithTag(TagConstants.SurfaceBackgroundParent);
            this._foregroundParent = GameObject.FindGameObjectWithTag(TagConstants.SurfaceForegroundParent);

            if (this._backgroundParent == null || this._foregroundParent == null) {
                Debug.LogError("There must be one game object that has the background parent tag and one with the foreground parent tag.");
            }

            this._backgroundParent.transform.position += this._backgroundOffset;

            var backgroundSurfaceColliders = this._backgroundParent.GetComponentsInChildren<EdgeCollider2D>();
            for (int i = 0; i < backgroundSurfaceColliders.Length; i++) {
                backgroundSurfaceColliders[i].gameObject.layer = LayerConstants.SurfaceBackgroundLayer;
            }

            var foregroundSurfaceColliders = this._foregroundParent.GetComponentsInChildren<EdgeCollider2D>();
            for (int i = 0; i < foregroundSurfaceColliders.Length; i++) {
                foregroundSurfaceColliders[i].gameObject.layer = LayerConstants.SurfaceForegroundLayer;
            }
        }

        /// <summary>
        /// Screens the size changed event handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="BrettMStory.Unity.Camera.ScreenSizeChangedEventArgs"/> instance containing the event data.</param>
        private void ScreenSizeChangedEventHandler(object sender, BrettMStory.Unity.Camera.ScreenSizeChangedEventArgs e) {
            this._backgroundScreen.transform.localScale = new Vector3(e.WorldWidth, e.WorldHeight, 1f);
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        private void Start() {
            var camera = GameObject.FindObjectOfType<Camera2D>();
            camera.ScreenSizeChanged += this.ScreenSizeChangedEventHandler;
            this._backgroundScreen.transform.localScale = new Vector3(camera.ScreenWorldWidth, camera.ScreenWorldHeight, 1f);
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