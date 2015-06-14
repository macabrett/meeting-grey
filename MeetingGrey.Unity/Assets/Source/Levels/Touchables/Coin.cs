namespace MeetingGrey.Unity.Levels.Touchables {

    using System.Collections;
    using BrettMStory.Unity;
    using MeetingGrey.Unity.Constants;
    using MeetingGrey.Unity.Levels.Touchables;
    using MeetingGrey.Unity.Player;
    using UnityEngine;

    /// <summary>
    /// A coin.
    /// </summary>
    public class Coin : BaseBehaviour, ITouchable {

        /// <summary>
        /// Touches this instance.
        /// </summary>
        /// <param name="player">The player.</param>
        public void Touch(CharacterController2D player) {
            if (!this.IsBusy) {
                this.IsBusy = true;
                Level.Instance.CoinsGathered++;
                this.GameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Awakes this instance.
        /// </summary>
        private void Awake() {
            this.GameObject.layer = LayerConstants.TouchableLayer;
        }
    }
}