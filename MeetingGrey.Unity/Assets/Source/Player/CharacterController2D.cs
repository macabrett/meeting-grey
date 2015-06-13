namespace MeetingGrey.Unity.Player {

    using System.Collections;
    using BrettMStory.Unity;
    using MeetingGrey.Unity.Constants;
    using MeetingGrey.Unity.Level.Surfaces;
    using UnityEngine;

    /// <summary>
    /// A 2D character controller.
    /// </summary>
    public class CharacterController2D : BaseBehaviour {

        /// <summary>
        /// The joystick dead zone.
        /// </summary>
        private float _deadZone = 0.15f;

        /// <summary>
        /// The gravity (units / second ^ 2)
        /// </summary>
        [SerializeField]
        private float _gravity;

        /// <summary>
        /// The height of the player.
        /// </summary>
        [SerializeField]
        private float _height;

        /// <summary>
        /// The initial velocity during a jump.
        /// </summary>
        [SerializeField]
        private float _jumpVelocity;

        /// <summary>
        /// The max vertical velocity the player can reach.
        /// </summary>
        [SerializeField]
        private float _maxVerticalVelocity;

        /// <summary>
        /// The speed of the character.
        /// </summary>
        [SerializeField]
        private float _speed;

        /// <summary>
        /// The max velocity the player can reach when falling.
        /// </summary>
        [SerializeField]
        private float _terminalVelocity;

        /// <summary>
        /// Half the height of the player. Only here to reduce update calculations.
        /// </summary>
        private float _halfHeight;

        /// <summary>
        /// A value indicating whether or not the player is on the ground.
        /// </summary>
        private bool _isGrounded;

        /// <summary>
        /// The current surface beneath the player.
        /// </summary>
        private ISurface _surfaceBelow;

        /// <summary>
        /// The current vertical velocity of the player.
        /// </summary>
        private float _verticalVelocity;

        /// <summary>
        /// Gets a value indicating whether this instance is grounded.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is grounded; otherwise, <c>false</c>.
        /// </value>
        public bool IsGrounded {
            get {
                return this._isGrounded;
            }
        }

        /// <summary>
        /// Gets the vertical velocity.
        /// </summary>
        /// <value>
        /// The vertical velocity.
        /// </value>
        public float VerticalVelocity {
            get {
                return this._verticalVelocity;
            }
        }

        /// <summary>
        /// Awakes this instance.
        /// </summary>
        private void Awake() {
            this._halfHeight = this._height * 0.5f;
        }

        /// <summary>
        /// Checks if the player is grounded.
        /// </summary>
        /// <param name="hit">The hit.</param>
        /// <returns>A value indicating whether or not this is grounded.</returns>
        private bool CheckIsGrounded(out RaycastHit2D hit) {
            // If we're grounded, we want to make sure we're on the same surface, so we go from bottom up.
            if (this._isGrounded) {
                var start = new Vector2(this.Position2D.x, this.Position2D.y - this._halfHeight);
                hit = Physics2D.Raycast(start, Vector2.up, this._halfHeight, LayerConstants.SurfaceLayerMask);
            } else {
                hit = Physics2D.Raycast(this.Position2D, -Vector2.up, this._halfHeight, LayerConstants.SurfaceLayerMask);
            }

            return hit.collider != null && this._verticalVelocity <= 0f;
        }

        /// <summary>
        /// Gets the horizontal velocity.
        /// </summary>
        /// <returns>0f, 1f, or -1f depending on input.</returns>
        private float GetHorizontalVelocity() {
            var x = Input.GetAxis(InputConstants.Horizontal);

            if (Mathf.Abs(x) > this._deadZone) {
                return Input.GetAxisRaw(InputConstants.Horizontal) * this._speed * Time.deltaTime;
            }

            return 0f;
        }

        /// <summary>
        /// Handles the actions.
        /// </summary>
        private void HandleActions() {
            if (this._isGrounded && Input.GetButtonDown(InputConstants.Jump)) {
                this._verticalVelocity = this._jumpVelocity;
            }
        }

        /// <summary>
        /// Handles the movement.
        /// </summary>
        private void HandleMovement() {
            var velocity = new Vector2(this.GetHorizontalVelocity(), this._verticalVelocity * Time.deltaTime);
            this.Transform.Translate(velocity);
            this._verticalVelocity -= this._gravity * Time.deltaTime;

            RaycastHit2D hit;

            var lastFrameWasGrounded = this._isGrounded;
            this._isGrounded = this.CheckIsGrounded(out hit);

            if (this._isGrounded) {
                this.Position2D = new Vector2(this.Position2D.x, hit.point.y + this._halfHeight);
                var surface = (ISurface)hit.collider.GetComponent(typeof(ISurface));

                if (surface != null) {
                    this._surfaceBelow = surface;
                    this._verticalVelocity = Mathf.Min(surface.Land(this), this._maxVerticalVelocity);
                } else {
                    this._verticalVelocity = 0f;
                }
            } else if (lastFrameWasGrounded && this._surfaceBelow != null) {
                this._surfaceBelow.LeaveSurface();
            }

            if (this._verticalVelocity < this._terminalVelocity) {
                this._verticalVelocity = this._terminalVelocity;
            }
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        private void Update() {
            this.HandleActions();
            this.HandleMovement();
        }
    }
}