namespace MeetingGrey.Unity.Level.Surfaces {

    using System.Collections;
    using MeetingGrey.Unity.Constants;
    using MeetingGrey.Unity.Player;
    using UnityEngine;

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
        /// Awakes this instance.
        /// </summary>
        protected virtual void Awake() {
            this.gameObject.layer = LayerConstants.SurfaceLayer;
        }

        /// <summary>
        /// Tries the disable collider.
        /// </summary>
        /// <returns>An IEnumerator.</returns>
        private IEnumerator TryDisableCollider() {
            this.gameObject.layer = LayerConstants.NoneLayer;
            yield return new WaitForSeconds(DropPlatform.WaitTime);
            this.gameObject.layer = LayerConstants.SurfaceLayer;
        }
    }
}