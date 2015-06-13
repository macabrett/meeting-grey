namespace MeetingGrey.Unity.Level.Surfaces {

    using System.Collections;
    using MeetingGrey.Unity.Constants;
    using MeetingGrey.Unity.Player;
    using UnityEngine;

    /// <summary>
    /// A platform that the player will bounce off of.
    /// </summary>
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
    }
}