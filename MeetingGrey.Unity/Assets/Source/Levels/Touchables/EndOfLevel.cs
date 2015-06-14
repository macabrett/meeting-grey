namespace MeetingGrey.Unity.Levels.Touchables {

    using System.Collections;
    using BrettMStory.Unity;
    using MeetingGrey.Unity.Constants;
    using MeetingGrey.Unity.Player;
    using UnityEngine;

    /// <summary>
    /// The end of a level.
    /// </summary>
    [RequireComponent(typeof(BoxCollider2D))]
    public class EndOfLevel : BaseBehaviour, ITouchable {

        /// <summary>
        /// Touches this instance.
        /// </summary>
        /// <param name="player">The player.</param>
        public void Touch(CharacterController2D player) {
            if (!this.IsBusy && Level.Instance.CoinsGathered >= 3) {
                this.IsBusy = true;
                Level.Instance.EndLevel();
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