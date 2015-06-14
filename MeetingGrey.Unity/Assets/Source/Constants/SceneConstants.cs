namespace MeetingGrey.Unity.Constants {

    /// <summary>
    /// Constants used in scene loading.
    /// </summary>
    public static class SceneConstants {

        /// <summary>
        /// The main menu name.
        /// </summary>
        public const string MainMenu = "MainMenu";

        /// <summary>
        /// Gets the name of the level.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <returns></returns>
        public static string GetLevelName(int level) {
            return string.Format("level{0}", level);
        }
    }
}