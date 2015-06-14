namespace MeetingGrey.Unity.Levels {

    using System;
    using System.Collections;
    using BrettMStory.Events;
    using MeetingGrey.Unity.Constants;
    using MeetingGrey.Unity.Player;
    using MeetingGrey.Unity.Wrappers;
    using UnityEngine;

    /// <summary>
    /// A level.
    /// </summary>
    public class Level : MonoBehaviour {

        /// <summary>
        /// The singleton instance.
        /// </summary>
        private static Level _instance;

        /// <summary>
        /// The checkpoint sequence number.
        /// </summary>
        private int _checkpointSequence = -1;

        /// <summary>
        /// The coins gathered.
        /// </summary>
        private int _coinsGathered = 0;

        /// <summary>
        /// The death message.
        /// </summary>
        [SerializeField]
        private GameObject _deathMessage;

        /// <summary>
        /// A value indicating whether or not the game is paused.
        /// </summary>
        private bool _isPaused = false;

        /// <summary>
        /// A value indicating whether or not the player is dead.
        /// </summary>
        private bool _isPlayerDead = false;

        /// <summary>
        /// The level.
        /// </summary>
        [SerializeField]
        private int _level;

        /// <summary>
        /// The pause menu.
        /// </summary>
        [SerializeField]
        private GameObject _pauseMenu;

        /// <summary>
        /// The place to respawn the player at if they die.
        /// </summary>
        private Vector2 _respawnPoint;

        /// <summary>
        /// Occurs when [coin gathered].
        /// </summary>
        public event EventHandler CoinGathered;

        /// <summary>
        /// Occurs when [respawned].
        /// </summary>
        public event EventHandler<RespawnEventArgs> Respawned;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static Level Instance {
            get {
                return Level._instance;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is paused.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is paused; otherwise, <c>false</c>.
        /// </value>
        public bool IsPaused {
            get {
                return this._isPaused;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is player dead.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is player dead; otherwise, <c>false</c>.
        /// </value>
        public bool IsPlayerDead {
            get {
                return this._isPlayerDead;
            }
        }

        /// <summary>
        /// Gets or sets the coins gathered.
        /// </summary>
        /// <value>
        /// The coins gathered.
        /// </value>
        public int CoinsGathered {
            get {
                return this._coinsGathered;
            }

            set {
                if (this._coinsGathered < 3) {
                    this._coinsGathered = value > 3 ? 3 : value;
                    this.CoinGathered.SafeInvoke(this);
                }
            }
        }

        /// <summary>
        /// Ends the level.
        /// </summary>
        public void EndLevel() {
            PlayerPrefsWrapper.SaveLevelCompleted(this._level, this._coinsGathered);
            Application.LoadLevel(SceneConstants.GetLevelName(this._level + 1));
        }

        /// <summary>
        /// Registers the checkpoint.
        /// </summary>
        /// <param name="sequenceNumber">The sequence number.</param>
        /// <param name="respawnPoint">The respawn point.</param>
        /// <returns>A value indicating whether or not this checkpoint will be used.</returns>
        public bool RegisterCheckpoint(int sequenceNumber, Vector2 respawnPoint) {
            if (this._checkpointSequence < sequenceNumber) {
                this._checkpointSequence = sequenceNumber;
                this._respawnPoint = respawnPoint;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns to main menu.
        /// </summary>
        public void ReturnToMainMenu() {
            Application.LoadLevel(SceneConstants.MainMenu);
        }

        /// <summary>
        /// Unpauses.
        /// </summary>
        public void Unpause() {
            this._isPaused = false;
            this._pauseMenu.SetActive(false);
        }

        /// <summary>
        /// Awakes this instance.
        /// </summary>
        private void Awake() {
            Level._instance = this;
            PlayerPrefsWrapper.LastLevelPlayed = this._level;
        }

        /// <summary>
        /// Players the died event handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void PlayerDiedEventHandler(object sender, EventArgs e) {
            this._isPlayerDead = true;
            this._deathMessage.SetActive(true);
        }

        /// <summary>
        /// Respawns this instance.
        /// </summary>
        private void Respawn() {
            this.Respawned.SafeInvoke(this, new RespawnEventArgs(this._checkpointSequence, this._respawnPoint));
            this._isPlayerDead = false;
            this._deathMessage.SetActive(false);
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        private void Start() {
            CharacterController2D.Instance.PlayerDied += this.PlayerDiedEventHandler;
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        private void Update() {
            if (this._isPlayerDead && Input.GetButtonDown(InputConstants.Jump)) {
                this.Respawn();
                return;
            } else if (!this._isPlayerDead && Input.GetButtonDown(InputConstants.Pause)) {
                this._isPaused = true;
                this._pauseMenu.SetActive(true);
            }
        }
    }
}