namespace MeetingGrey.Unity.Levels {

    using System;
    using UnityEngine;

    /// <summary>
    /// Event arguments for respawn.
    /// </summary>
    public class RespawnEventArgs : EventArgs {

        /// <summary>
        /// Initializes a new instance of the <see cref="RespawnEventArgs"/> class.
        /// </summary>
        /// <param name="sequenceNumber">The sequence number.</param>
        /// <param name="respawnPosition">The respawn position.</param>
        public RespawnEventArgs(int sequenceNumber, Vector2 respawnPosition) {
            this.SequenceNumber = sequenceNumber;
            this.RespawnPosition = respawnPosition;
        }

        /// <summary>
        /// Gets the respawn position.
        /// </summary>
        /// <value>
        /// The respawn position.
        /// </value>
        public Vector2 RespawnPosition { get; private set; }

        /// <summary>
        /// Gets the sequence number.
        /// </summary>
        /// <value>
        /// The sequence number.
        /// </value>
        public int SequenceNumber { get; private set; }
    }
}