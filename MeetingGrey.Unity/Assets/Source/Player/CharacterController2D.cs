namespace MeetingGrey.Unity.Player {

    using System;
    using System.Collections;
    using Assets.Source.Constants;
    using BrettMStory.Events;
    using BrettMStory.Unity;
    using MeetingGrey.Unity.Constants;
    using MeetingGrey.Unity.Depth;
    using MeetingGrey.Unity.Levels;
    using MeetingGrey.Unity.Levels.Surfaces;
    using MeetingGrey.Unity.Levels.Touchables;
    using MeetingGrey.Unity.Wrappers;
    using UnityEngine;

    /// <summary>
    /// Player state enumeration.
    /// </summary>
    public enum PlayerState {

        /// <summary>
        /// The standing state.
        /// </summary>
        Standing,

        /// <summary>
        /// The walking state.
        /// </summary>
        Walking,

        /// <summary>
        /// The jumping state.
        /// </summary>
        Jumping,
    }

    /// <summary>
    /// A 2D character controller.
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class CharacterController2D : BaseBehaviour {

        /// <summary>
        /// The joystick dead zone.
        /// </summary>
        private const float DeadZone = 0.15f;

        /// <summary>
        /// The singleton instance;
        /// </summary>
        private static CharacterController2D _instance;

        /// <summary>
        /// The animator.
        /// </summary>
        private Animator _animator;

        /// <summary>
        /// The current horizontal direction.
        /// </summary>
        private float _currentHorizontalDirection = 0f;

        /// <summary>
        /// The gravity (units / second ^ 2)
        /// </summary>
        [SerializeField]
        private float _gravity;

        /// <summary>
        /// Half the height of the player. Only here to reduce update calculations.
        /// </summary>
        private float _halfHeight;

        /// <summary>
        /// The height of the player.
        /// </summary>
        [SerializeField]
        private float _height;

        /// <summary>
        /// A value indicating whether or not the player is on the ground.
        /// </summary>
        private bool _isGrounded;

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
        /// The current player state.
        /// </summary>
        private PlayerState _playerState = PlayerState.Standing;

        /// <summary>
        /// The quarter height.
        /// </summary>
        private float _quarterHeight;

        /// <summary>
        /// The speed of the character.
        /// </summary>
        [SerializeField]
        private float _speed;

        /// <summary>
        /// The current surface beneath the player.
        /// </summary>
        private ISurface _surfaceBelow;

        /// <summary>
        /// The max velocity the player can reach when falling.
        /// </summary>
        [SerializeField]
        private float _terminalVelocity;

        /// <summary>
        /// The current vertical velocity of the player.
        /// </summary>
        private float _verticalVelocity;

        /// <summary>
        /// Occurs when [player died].
        /// </summary>
        public event EventHandler PlayerDied;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static CharacterController2D Instance {
            get {
                return CharacterController2D._instance;
            }
        }

        /// <summary>
        /// Gets the current horizontal direction.
        /// </summary>
        /// <value>
        /// The current horizontal direction.
        /// </value>
        public float CurrentHorizontalDirection {
            get {
                return this._currentHorizontalDirection;
            }

            private set {
                if (value != 0f) {
                    this.Scale2D = new Vector2(value, 1f);
                }

                this._currentHorizontalDirection = value;
            }
        }

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
        /// Gets the state of the player.
        /// </summary>
        /// <value>
        /// The state of the player.
        /// </value>
        public PlayerState PlayerState {
            get {
                return this._playerState;
            }

            private set {
                this._playerState = value;

                switch (this._playerState) {
                    case Player.PlayerState.Jumping:
                        this._animator.SetTrigger(PlayerAnimationConstants.JumpTrigger);
                        break;

                    case Player.PlayerState.Standing:
                        this._animator.SetTrigger(PlayerAnimationConstants.StandTrigger);
                        break;

                    case Player.PlayerState.Walking:
                        this._animator.SetTrigger(PlayerAnimationConstants.WalkTrigger);
                        break;
                }
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

            private set {
                if (value < this._terminalVelocity) {
                    this._verticalVelocity = this._terminalVelocity;
                } else {
                    this._verticalVelocity = value;
                }
            }
        }

        /// <summary>
        /// Awakes this instance.
        /// </summary>
        private void Awake() {
            CharacterController2D._instance = this;
            this._halfHeight = this._height * 0.5f;
            this._quarterHeight = this._height * 0.25f;
            this._animator = this.GetComponent<Animator>();
        }

        /// <summary>
        /// Checks if dead.
        /// </summary>
        private void CheckIfDead() {
            if (this.Position2D.y < DeathLine.Instance.Position2D.y - 5f) {
                this.PlayerDied.SafeInvoke(this);
                this.VerticalVelocity = 0f;
                this.CurrentHorizontalDirection = 0f;
            }
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
                hit = Physics2D.Raycast(start, Vector2.up, this._halfHeight, DepthController.Instance.SurfaceLayerMask | LayerConstants.SurfaceLayerMask);
            } else {
                hit = Physics2D.Raycast(this.Position2D - this._quarterHeight * Vector2.up, -Vector2.up, this._quarterHeight, DepthController.Instance.SurfaceLayerMask | LayerConstants.SurfaceLayerMask);
            }

            return hit.collider != null && this._verticalVelocity <= 0f;
        }

        /// <summary>
        /// Gets the horizontal velocity.
        /// </summary>
        /// <returns>0f, 1f, or -1f depending on input.</returns>
        private float GetHorizontalVelocity() {
            var x = Input.GetAxis(InputConstants.Horizontal);

            if (Mathf.Abs(x) > DeadZone) {
                var rawInput = Input.GetAxisRaw(InputConstants.Horizontal);
                this.CurrentHorizontalDirection = rawInput;
                return this._currentHorizontalDirection * this._speed * Time.smoothDeltaTime;
            }

            this.CurrentHorizontalDirection = 0f;
            return 0f;
        }

        /// <summary>
        /// Handles the actions.
        /// </summary>
        private void HandleActions() {
            if (this._isGrounded && Input.GetButtonDown(InputConstants.Jump)) {
                if (this._surfaceBelow == null || (this._surfaceBelow) as BouncePad == null) {
                    this._verticalVelocity = this._jumpVelocity;
                    AudioWrapper.PlayJumpClip(this.Position2D);
                }
            }

            if (this._isGrounded && this._surfaceBelow != null && Input.GetAxisRaw(InputConstants.Vertical) < 0f) {
                this._surfaceBelow.Drop();
            }
        }

        /// <summary>
        /// Handles the animation.
        /// </summary>
        private void HandleAnimation() {
            if (this._isGrounded) {
                if (this._playerState == PlayerState.Jumping) {
                    if (this._currentHorizontalDirection != 0f) {
                        this.PlayerState = Player.PlayerState.Walking;
                    } else {
                        this.PlayerState = Player.PlayerState.Standing;
                    }
                } else if (this._playerState == Player.PlayerState.Walking && this._currentHorizontalDirection == 0f) {
                    this.PlayerState = Player.PlayerState.Standing;
                } else if (this._playerState == Player.PlayerState.Standing && this._currentHorizontalDirection != 0f) {
                    this.PlayerState = Player.PlayerState.Walking;
                }
            } else if (this._playerState == Player.PlayerState.Standing || this._playerState == Player.PlayerState.Walking) {
                this.PlayerState = Player.PlayerState.Jumping;
            }
        }

        /// <summary>
        /// Handles the movement.
        /// </summary>
        private void HandleMovement() {
            var velocity = new Vector2(this.GetHorizontalVelocity(), this._verticalVelocity * Time.smoothDeltaTime);
            this.Transform.Translate(velocity);
            this.VerticalVelocity -= this._gravity * Time.smoothDeltaTime;

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
        /// Handles the touch.
        /// </summary>
        private void HandleTouch() {
            var colliders = Physics2D.OverlapCircleAll(this.Position2D, this._quarterHeight, LayerConstants.TouchableLayerMask);

            for (int i = 0; i < colliders.Length; i++) {
                var touchable = (ITouchable)colliders[i].GetComponent(typeof(ITouchable));

                if (touchable == null) {
                    continue;
                }

                touchable.Touch(this);
            }
        }

        /// <summary>
        /// Respawneds the event handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RespawnEventArgs"/> instance containing the event data.</param>
        private void RespawnedEventHandler(object sender, RespawnEventArgs e) {
            this.Position2D = e.RespawnPosition;
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        private void Start() {
            Level.Instance.Respawned += this.RespawnedEventHandler;
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        private void Update() {
            if (Level.Instance.IsPlayerDead || Level.Instance.IsPaused) {
                return;
            }

            this.HandleActions();
            this.HandleMovement();
            this.HandleTouch();
            this.HandleAnimation();
            this.CheckIfDead();
        }
    }
}