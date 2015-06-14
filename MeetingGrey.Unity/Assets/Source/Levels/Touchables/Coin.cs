namespace MeetingGrey.Unity.Levels.Touchables {

    using System.Collections;
    using BrettMStory.Unity;
    using MeetingGrey.Unity.Constants;
    using MeetingGrey.Unity.Levels.Touchables;
    using MeetingGrey.Unity.Player;
    using MeetingGrey.Unity.Wrappers;
    using UnityEngine;

    /// <summary>
    /// A coin.
    /// </summary>
    [RequireComponent(typeof(BoxCollider2D))]
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
                AudioWrapper.PlayCoinClip(this.Position2D);
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