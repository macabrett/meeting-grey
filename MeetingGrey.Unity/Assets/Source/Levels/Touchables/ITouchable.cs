using MeetingGrey.Unity.Player;

namespace MeetingGrey.Unity.Levels.Touchables {

    /// <summary>
    /// Interface for touchable things.
    /// </summary>
    public interface ITouchable {

        /// <summary>
        /// Touches this instance.
        /// </summary>
        /// <param name="player">The player.</param>
        void Touch(CharacterController2D player);
    }
}