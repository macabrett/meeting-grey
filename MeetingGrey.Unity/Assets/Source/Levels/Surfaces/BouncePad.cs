namespace MeetingGrey.Unity.Levels.Surfaces {

    using System.Collections;
    using BrettMStory.Unity;
    using MeetingGrey.Unity.Constants;
    using MeetingGrey.Unity.Player;
    using UnityEngine;

    /// <summary>
    /// A platform that the player will bounce off of.
    /// </summary>
    [RequireComponent(typeof(EdgeCollider2D), typeof(SpriteRenderer))]
    public class BouncePad : BaseBehaviour, ISurface {

        /// <summary>
        /// The animation time.
        /// </summary>
        private const float AnimationTime = 0.25f;

        /// <summary>
        /// The jump boost.
        /// </summary>
        private const float JumpBoost = 1f;

        /// <summary>
        /// The bouncing sprite.
        /// </summary>
        [SerializeField]
        private Sprite _bounceSprite;

        /// <summary>
        /// The idle sprite.
        /// </summary>
        [SerializeField]
        private Sprite _idleSprite;

        /// <summary>
        /// The sprite renderer.
        /// </summary>
        private SpriteRenderer _spriteRenderer;

        /// <summary>
        /// Trys to drop through this platform.
        /// </summary>
        public void Drop() {
            return;
        }

        /// <summary>
        /// Lands the specified player.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <returns></returns>
        public float Land(CharacterController2D player) {
            this.StartCoroutine(this.PlayBounceAnimation());
            var verticalVelocity = Mathf.Abs(player.VerticalVelocity);

            if (Input.GetButton(InputConstants.Jump)) {
                verticalVelocity += BouncePad.JumpBoost;
            }

            return verticalVelocity;
        }

        /// <summary>
        /// Leaves the surface.
        /// </summary>
        public void LeaveSurface() {
            return;
        }

        /// <summary>
        /// Awakes this instance.
        /// </summary>
        private void Awake() {
            this._spriteRenderer = this.GetComponent<SpriteRenderer>();
            if (this._idleSprite == null) {
                this._idleSprite = this._spriteRenderer.sprite;
            } else {
                this._spriteRenderer.sprite = this._idleSprite;
            }

            var collider = this.GetComponent<EdgeCollider2D>();
            collider.points = new Vector2[] { new Vector2(-0.8f, 0f), new Vector2(0.9f, 0f) };
            this.GameObject.layer = LayerConstants.SurfaceLayer;
        }

        /// <summary>
        /// Plays the bounce animation.
        /// </summary>
        /// <returns></returns>
        private IEnumerator PlayBounceAnimation() {
            this._spriteRenderer.sprite = this._bounceSprite;
            yield return new WaitForSeconds(BouncePad.AnimationTime);
            this._spriteRenderer.sprite = this._idleSprite;
        }
    }
}