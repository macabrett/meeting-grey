namespace MeetingGrey.Unity.Level.Surfaces {

    using System.Collections;
    using MeetingGrey.Unity.Constants;
    using MeetingGrey.Unity.Player;
    using UnityEngine;

    [RequireComponent(typeof(EdgeCollider2D))]
    public class BouncePad : MonoBehaviour, ISurface {

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
            return Mathf.Abs(player.VerticalVelocity);
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
        protected virtual void Awake() {
            this.gameObject.layer = LayerConstants.SurfaceLayer;
        }
    }
}