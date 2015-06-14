namespace MeetingGrey.Unity.Player {

    using System;
    using System.Collections;
    using BrettMStory.Events;
    using BrettMStory.Unity;
    using BrettMStory.Unity.Camera;
    using MeetingGrey.Unity.Constants;
    using MeetingGrey.Unity.Levels;
    using UnityEngine;

    /// <summary>
    /// A 2D camera which follows a target.
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public class Camera2D : BaseBehaviour {

        /// <summary>
        /// The camera.
        /// </summary>
        private Camera _camera;

        /// <summary>
        /// The death line.
        /// </summary>
        private DeathLine _deathLine;

        /// <summary>
        /// Half of the world height.
        /// </summary>
        private float _halfWorldHeight;

        /// <summary>
        /// Half of the world width.
        /// </summary>
        private float _halfWorldWidth;

        /// <summary>
        /// The lerp amount.
        /// </summary>
        [SerializeField]
        private float _lerpAmount;

        /// <summary>
        /// The minimum world height.
        /// </summary>
        [SerializeField]
        private float _minimumWorldHeight;

        /// <summary>
        /// The minimum world width.
        /// </summary>
        [SerializeField]
        private float _minimumWorldWidth;

        /// <summary>
        /// The max horizontal offset before following.
        /// </summary>
        [SerializeField]
        private float _maxHorizontalOffset;

        /// <summary>
        /// The screen height.
        /// </summary>
        private int _screenHeight;

        /// <summary>
        /// The screen width.
        /// </summary>
        private int _screenWidth;

        /// <summary>
        /// The screen height in world units.
        /// </summary>
        private float _screenWorldHeight;

        /// <summary>
        /// The screen width in world units.
        /// </summary>
        private float _screenWorldWidth;

        /// <summary>
        /// The target to follow.
        /// </summary>
        [SerializeField]
        private Transform _target;

        /// <summary>
        /// The offset from the target.
        /// </summary>
        [SerializeField]
        private Vector2 _targetOffset;

        /// <summary>
        /// Called when [screen size changed].
        /// </summary>
        public event EventHandler<ScreenSizeChangedEventArgs> ScreenSizeChanged;

        /// <summary>
        /// The bottom left corner of the screen in world position.
        /// </summary>
        public Vector2 BottomLeftCorner {
            get {
                return new Vector2(this.Position2D.x - this._halfWorldWidth, this.Position2D.y - this._halfWorldHeight);
            }
        }

        /// <summary>
        /// The bottom right corner of the screen in world position.
        /// </summary>
        public Vector2 BottomRightCorner {
            get {
                return new Vector2(this.Position2D.x + this._halfWorldWidth, this.Position2D.y - this._halfWorldHeight);
            }
        }

        /// <summary>
        /// Gets the height of the screen world.
        /// </summary>
        /// <value>
        /// The height of the screen world.
        /// </value>
        public float ScreenWorldHeight {
            get {
                return this._screenWorldHeight;
            }
        }

        /// <summary>
        /// Gets the width of the screen world.
        /// </summary>
        /// <value>
        /// The width of the screen world.
        /// </value>
        public float ScreenWorldWidth {
            get {
                return this._screenWorldWidth;
            }
        }

        /// <summary>
        /// Gets or sets the target offset.
        /// </summary>
        public Vector2 TargetOffset {
            get {
                return this._targetOffset;
            }

            set {
                this._targetOffset = value;
            }
        }

        /// <summary>
        /// The top left corner of the screen in world position.
        /// </summary>
        public Vector2 TopLeftCorner {
            get {
                return new Vector2(this.Position2D.x - this._halfWorldWidth, this.Position2D.y + this._halfWorldHeight);
            }
        }

        /// <summary>
        /// The top right corner of the screen in world position.
        /// </summary>
        public Vector2 TopRightCorner {
            get {
                return new Vector2(this.Position2D.x + this._halfWorldWidth, this.Position2D.y + this._halfWorldHeight);
            }
        }

        /// <summary>
        /// Adjusts the camera to a new resolution.
        /// </summary>
        private void Adjust() {
            this._camera.orthographicSize = this._minimumWorldHeight * 0.5f;
            var worldWidth = this._camera.ScreenToWorldPoint(new Vector2(Screen.width, 0f)).x - this._camera.ScreenToWorldPoint(Vector2.zero).x;

            if (worldWidth < this._minimumWorldWidth) {
                this._camera.orthographicSize *= this._minimumWorldWidth / worldWidth;
            }

            this._screenHeight = Screen.height;
            this._screenWidth = Screen.width;

            this._screenWorldWidth = this._camera.ScreenToWorldPoint(new Vector2(Screen.width, 0f)).x - this._camera.ScreenToWorldPoint(Vector2.zero).x;
            this._halfWorldWidth = this._screenWorldWidth / 2f;

            this._screenWorldHeight = this._camera.ScreenToWorldPoint(new Vector2(0f, Screen.height)).y - this._camera.ScreenToWorldPoint(Vector2.zero).y;
            this._halfWorldHeight = this._screenWorldHeight / 2f;

            this.Position2D = new Vector2(this._target.position.x, this._deathLine.transform.position.y + this._halfWorldHeight);

            this.ScreenSizeChanged.SafeInvoke(this, new ScreenSizeChangedEventArgs {
                WorldHeight = this._screenWorldHeight,
                WorldWidth = this._screenWorldWidth
            });
        }

        /// <summary>
        /// Awakes this instance.
        /// </summary>
        private void Awake() {
            this._camera = this.GetComponent<Camera>();

            this._deathLine = GameObject.FindObjectOfType<DeathLine>();

            if (this._target == null) {
                var targetObject = GameObject.FindGameObjectWithTag(TagConstants.Player);

                if (targetObject == null) {
                    Debug.LogError("No game object with tag 'Player' has been defined.");
                }

                this._target = targetObject.transform;
            }

            this.StartCoroutine(this.CheckScreenSizeChanged());
        }

        /// <summary>
        /// Checks if the screen size has changed.
        /// </summary>
        /// <returns>An enumerator.</returns>
        private IEnumerator CheckScreenSizeChanged() {
            while (true) {
                if (Screen.width != this._screenWidth || Screen.height != this._screenHeight) {
                    this.Adjust();
                }

                yield return new WaitForSeconds(0.25f);
            }
        }

        /// <summary>
        /// Follows the target.
        /// </summary>
        private void FollowTarget() {
            var horizontalDistance = this.Position2D.x - this._target.position.x;
            var x = this.Position2D.x;

            if (horizontalDistance < -this._maxHorizontalOffset) {
                x = this._target.position.x - this._maxHorizontalOffset;
            } else if (horizontalDistance > this._maxHorizontalOffset) {
                x = this._target.position.x + this._maxHorizontalOffset;
            }

            this.Position2D = new Vector2(x, this.Position2D.y);
        }

        /// <summary>
        /// Late update.
        /// </summary>
        private void LateUpdate() {
            this.FollowTarget();
        }

        /// <summary>
        /// Respawneds the event handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RespawnEventArgs"/> instance containing the event data.</param>
        private void RespawnedEventHandler(object sender, RespawnEventArgs e) {
            this.Position2D = new Vector2(e.RespawnPosition.x, this.Position2D.y);
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        private void Start() {
            Level.Instance.Respawned += this.RespawnedEventHandler;
        }
    }
}