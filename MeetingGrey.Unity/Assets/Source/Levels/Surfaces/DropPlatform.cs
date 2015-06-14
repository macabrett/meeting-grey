namespace MeetingGrey.Unity.Levels.Surfaces {

    using System.Collections;
    using MeetingGrey.Unity.Constants;
    using MeetingGrey.Unity.Player;
    using UnityEngine;

    /// <summary>
    /// A platform that the player can drop through.
    /// </summary>
    [RequireComponent(typeof(EdgeCollider2D))]
    public class DropPlatform : MonoBehaviour, ISurface {

        /// <summary>
        /// Time to disable this platform.
        /// </summary>
        private const float WaitTime = 0.3f;

        /// <summary>
        /// Trys to drop through this platform.
        /// </summary>
        public void Drop() {
            this.StartCoroutine(TryDisableCollider());
        }

        /// <summary>
        /// Lands the specified player on this platform.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <returns>
        /// A float indicating the y velocity of the player after landing on this platform.
        /// </returns>
        public float Land(CharacterController2D player) {
            return 0f;
        }

        /// <summary>
        /// Leaves the surface.
        /// </summary>
        public void LeaveSurface() {
            return;
        }

        /// <summary>
        /// Tries the disable collider.
        /// </summary>
        /// <returns>An IEnumerator.</returns>
        private IEnumerator TryDisableCollider() {
            var originalLayer = this.gameObject.layer;
            this.gameObject.layer = LayerConstants.NoneLayer;
            yield return new WaitForSeconds(DropPlatform.WaitTime);
            this.gameObject.layer = originalLayer;
        }
    }
}