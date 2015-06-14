namespace MeetingGrey.Unity.Levels.Surfaces {

    using MeetingGrey.Unity.Player;

    /// <summary>
    /// Interface for surfaces.
    /// </summary>
    public interface ISurface {

        /// <summary>
        /// Trys to drop through this platform.
        /// </summary>
        void Drop();

        /// <summary>
        /// Lands the specified player on this platform.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <returns>A float indicating the y velocity of the player after landing on this platform.</returns>
        float Land(CharacterController2D player);

        /// <summary>
        /// Leaves the surface.
        /// </summary>
        void LeaveSurface();
    }
}