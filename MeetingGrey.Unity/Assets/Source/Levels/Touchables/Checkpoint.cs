namespace MeetingGrey.Unity.Levels.Touchables {

    using BrettMStory.Unity;
    using MeetingGrey.Unity.Constants;
    using MeetingGrey.Unity.Levels.Touchables;
    using MeetingGrey.Unity.Player;
    using UnityEngine;

    /// <summary>
    /// A level checkpoint.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
    public class Checkpoint : BaseBehaviour, ITouchable {

        /// <summary>
        /// The active sprite.
        /// </summary>
        [SerializeField]
        private Sprite _activeSprite;

        /// <summary>
        /// The inactive sprite.
        /// </summary>
        [SerializeField]
        private Sprite _inactiveSprite;

        /// <summary>
        /// The sequence number.
        /// </summary>
        [SerializeField]
        private int _sequenceNumber;

        /// <summary>
        /// The sprite renderer.
        /// </summary>
        private SpriteRenderer _spriteRenderer;

        /// <summary>
        /// Awakes this instance.
        /// </summary>
        private void Awake() {
            this._spriteRenderer = this.GetComponent<SpriteRenderer>();
            this._spriteRenderer.sprite = this._inactiveSprite;
            this.GameObject.layer = LayerConstants.TouchableLayer;
        }

        /// <summary>
        /// Touches this instance.
        /// </summary>
        /// <param name="player">The player.</param>
        public void Touch(CharacterController2D player) {
            if (!this.IsBusy) {
                this.IsBusy = true;
                if (Level.Instance.RegisterCheckpoint(this._sequenceNumber, this.Position2D + Vector2.up)) {
                    this._spriteRenderer.sprite = this._activeSprite;
                    return;
                }

                this.IsBusy = false;
            }
        }
    }
}