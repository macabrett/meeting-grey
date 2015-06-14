namespace MeetingGrey.Unity.Levels {

    using System;
    using BrettMStory.Unity;
    using UnityEngine;

    [RequireComponent(typeof(SpriteRenderer))]
    public class CoinProgressThing : BaseBehaviour {

        /// <summary>
        /// The number of coins.
        /// </summary>
        private int _numberOfCoins = 0;

        /// <summary>
        /// The sprite renderer.
        /// </summary>
        private SpriteRenderer _spriteRenderer;

        /// <summary>
        /// The sprites.
        /// </summary>
        [SerializeField]
        private Sprite[] _sprites;

        /// <summary>
        /// Gets the number of coins.
        /// </summary>
        /// <value>
        /// The number of coins.
        /// </value>
        public int NumberOfCoins {
            get {
                return this._numberOfCoins;
            }

            private set {
                this._numberOfCoins = value;

                if (this._numberOfCoins < this._sprites.Length && this._numberOfCoins >= 0) {
                    this._spriteRenderer.sprite = this._sprites[this._numberOfCoins];
                }
            }
        }

        /// <summary>
        /// Awakes this instance.
        /// </summary>
        private void Awake() {
            this._spriteRenderer = this.GetComponent<SpriteRenderer>();
            this.NumberOfCoins = 0;
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        private void Start() {
            Level.Instance.CoinGathered += this.CoinGatheredEventHandler;
        }

        /// <summary>
        /// Coins the gathered event handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CoinGatheredEventHandler(object sender, EventArgs e) {
            this.NumberOfCoins++;
        }
    }
}