namespace MeetingGrey.Unity.Menus {

    using BrettMStory.Unity;
    using MeetingGrey.Unity.Constants;
    using MeetingGrey.Unity.Levels;
    using UnityEngine;

    /// <summary>
    /// The pause menu.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class PauseMenu : BaseBehaviour {

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

                this._spriteRenderer.sprite = this._menuSprites[this._iterator];
            }
        }

        /// <summary>
        /// Awakes this instance.
        /// </summary>
        private void Awake() {
            this._spriteRenderer = this.GetComponent<SpriteRenderer>();
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
                if (this.Iterator == 0) {
                    Level.Instance.Unpause();
                } else if (this.Iterator == 1) {
                    Level.Instance.ReturnToMainMenu();
                } else {
                    Level.Instance.Quit();
                    Debug.Log("QUIIIT");
                }
            }

            this._lastFrameVerticalInput = rawInput;
        }
    }
}