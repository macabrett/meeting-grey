namespace MeetingGrey.Unity.Menus {

    using BrettMStory.Unity;
    using MeetingGrey.Unity.Constants;
    using MeetingGrey.Unity.Levels;
    using MeetingGrey.Unity.Wrappers;
    using UnityEngine;

    /// <summary>
    /// The pause menu.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class PauseMenu : BaseBehaviour {

        /// <summary>
        /// Pause menu items.
        /// </summary>
        private enum PauseMenuItems {

            /// <summary>
            /// The play menu item.
            /// </summary>
            Play = 0,

            /// <summary>
            /// The menu menu item.
            /// </summary>
            Menu = 1,

            /// <summary>
            /// The exit menu item.
            /// </summary>
            Exit = 2,
        }

        /// <summary>
        /// The menu sprites;
        /// </summary>
        [SerializeField]
        private Sprite[] _menuSprites;

        /// <summary>
        /// The iterator.
        /// </summary>
        private int _iterator = 0;

        /// <summary>
        /// The vertical input from the last frame.
        /// </summary>
        private float _lastFrameVerticalInput = 0f;

        /// <summary>
        /// The sprite renderer.
        /// </summary>
        private SpriteRenderer _spriteRenderer;

        /// <summary>
        /// Gets the iterator.
        /// </summary>
        /// <value>
        /// The iterator.
        /// </value>
        public int Iterator {
            get {
                return this._iterator;
            }

            private set {
                if (value >= this._menuSprites.Length) {
                    this._iterator = 0;
                } else if (value < 0) {
                    this._iterator = this._menuSprites.Length - 1;
                } else {
                    this._iterator = value;
                }

                AudioWrapper.PlayMenuClip(this.Position2D);
                this._spriteRenderer.sprite = this._menuSprites[this._iterator];
            }
        }

        /// <summary>
        /// Awakes this instance.
        /// </summary>
        private void Awake() {
            this._spriteRenderer = this.GetComponent<SpriteRenderer>();
            this.GameObject.SetActive(false);
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        private void Update() {
            var rawInput = Input.GetAxisRaw(InputConstants.Vertical);

            if (Input.GetButtonDown(InputConstants.Pause)) {
                Level.Instance.Unpause();
            } else if (rawInput < 0f && rawInput != this._lastFrameVerticalInput) {
                this.Iterator++;
            } else if (rawInput > 0f && rawInput != this._lastFrameVerticalInput) {
                this.Iterator--;
            } else if (Input.GetButtonDown(InputConstants.Jump) || Input.GetKeyDown(KeyCode.Return)) {
                if (this.Iterator == (int)PauseMenuItems.Play) {
                    Level.Instance.Unpause();
                } else if (this.Iterator == (int)PauseMenuItems.Menu) {
                    Application.LoadLevel(SceneConstants.MainMenu);
                } else {
                    Application.Quit();
                }
            }

            this._lastFrameVerticalInput = rawInput;
        }
    }
}