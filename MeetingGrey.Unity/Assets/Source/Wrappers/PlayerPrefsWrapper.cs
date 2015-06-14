namespace MeetingGrey.Unity.Wrappers {

    using UnityEngine;

    /// <summary>
    /// Wrapper for player prefs.
    /// </summary>
    public static class PlayerPrefsWrapper {

        /// <summary>
        /// Gets the last level played.
        /// </summary>
        /// <value>
        /// The last level played.
        /// </value>
        public static int LastLevelPlayed {
            get {
                if (PlayerPrefs.HasKey("LastLevelPlayed")) {
                    return PlayerPrefs.GetInt("LastLevelPlayed");
                }

                PlayerPrefs.SetInt("LastLevelPlayed", 0);
                return 0;
            }

            set {
                PlayerPrefs.SetInt("LastLevelPlayed", value);
            }
        }

        /// <summary>
        /// Saves the level completed.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="coinsCollected">The coins collected.</param>
        public static void SaveLevelCompleted(int level, int coinsCollected) {
            var levelKey = PlayerPrefsWrapper.GetLevelKey(level);
            var coinsKey = PlayerPrefsWrapper.GetLevelCoinsKey(level);

            if (PlayerPrefs.HasKey(levelKey)) {
                if (PlayerPrefs.HasKey(coinsKey)) {
                    var previousCoins = PlayerPrefs.GetInt(coinsKey);

                    if (coinsCollected > previousCoins) {
                        PlayerPrefs.SetInt(coinsKey, coinsCollected);
                    }
                }
            }

            PlayerPrefs.SetInt(levelKey, 1);
        }

        /// <summary>
        /// Gets the level coins key.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <returns>The level coins key.</returns>
        private static string GetLevelCoinsKey(int level) {
            return string.Format("level{0}coins", level);
        }

        /// <summary>
        /// Gets the level key.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <returns>The level key.</returns>
        private static string GetLevelKey(int level) {
            return string.Format("level{0}", level);
        }
    }
}