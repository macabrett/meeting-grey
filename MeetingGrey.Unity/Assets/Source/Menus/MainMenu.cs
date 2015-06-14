namespace MeetingGrey.Unity.Menus {

    using BrettMStory.Unity;
    using MeetingGrey.Unity.Constants;
    using MeetingGrey.Unity.Wrappers;
    using UnityEngine;

    /// <summary>
    /// The main menu.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class MainMenu : BaseBehaviour {

        /// <summary>
        /// The main menu items.
        /// </summary>
        private enum MainMenuItems {

            /// <summary>
            /// The continue item.
            /// </summary>
            Continue = 0,

            /// <summary>
            /// The new game
            /// </summary>
            NewGame = 1,

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
        }

        /// <summary>
        /// Continues the game.
        /// </summary>
        private void ContinueGame() {
            var lastLevelPlayed = PlayerPrefsWrapper.LastLevelPlayed;
            Application.LoadLevel(SceneConstants.GetLevelName(lastLevelPlayed));
        }

        /// <summary>
        /// Quits the game.
        /// </summary>
        private void QuitGame() {
            Application.Quit();
        }

        /// <summary>
        /// Starts a new game.
        /// </summary>
        private void StartNewGame() {
            Application.LoadLevel(SceneConstants.GetLevelName(0));
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        private void Update() {
            if (Mathf.Abs(Input.GetAxis(InputConstants.Vertical)) > 0.15f) {
                var rawInput = Input.GetAxisRaw(InputConstants.Vertical);

                if (rawInput > 0f) {
                    rawInput = 1f;
                } else if (rawInput < 0f) {
                    rawInput = -1f;
                } else {
                    rawInput = 0f;
                }

                if (rawInput < 0f && rawInput != this._lastFrameVerticalInput) {
                    this.Iterator++;
                } else if (rawInput > 0f && rawInput != this._lastFrameVerticalInput) {
                    this.Iterator--;
                }

                this._lastFrameVerticalInput = rawInput;
            } else {
                this._lastFrameVerticalInput = 0f;
            }

            if (Input.GetButtonDown(InputConstants.Jump) || Input.GetKeyDown(KeyCode.Return)) {
                if (this.Iterator == (int)MainMenuItems.Continue) {
                    this.ContinueGame();
                } else if (this.Iterator == (int)MainMenuItems.NewGame) {
                    this.StartNewGame();
                } else {
                    this.QuitGame();
                }
            }
        }
    }
}