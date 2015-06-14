namespace MeetingGrey.Unity.Wrappers {

    using UnityEngine;

    /// <summary>
    /// Wrapper for audio.
    /// </summary>
    public static class AudioWrapper {

        /// <summary>
        /// The checkpoint path.
        /// </summary>
        private const string CheckpointPath = "Sound/Checkpoint";

        /// <summary>
        /// The coin path.
        /// </summary>
        private const string CoinPath = "Sound/Coin";

        /// <summary>
        /// The jump path.
        /// </summary>
        private const string JumpPath = "Sound/Jump";

        /// <summary>
        /// The checkpoint audio clip.
        /// </summary>
        private static AudioClip _checkpoint;

        /// <summary>
        /// The coin audio clip.
        /// </summary>
        private static AudioClip _coin;

        /// <summary>
        /// The jump audio clip.
        /// </summary>
        private static AudioClip _jump;

        /// <summary>
        /// Gets the checkpoint audio clip.
        /// </summary>
        /// <value>
        /// The checkpoint audio clip.
        /// </value>
        private static AudioClip Checkpoint {
            get {
                return AudioWrapper._checkpoint ?? (AudioWrapper._checkpoint = Resources.Load<AudioClip>(AudioWrapper.CheckpointPath));
            }
        }

        /// <summary>
        /// Gets the coin audio clip.
        /// </summary>
        /// <value>
        /// The coin audio clip.
        /// </value>
        private static AudioClip Coin {
            get {
                return AudioWrapper._coin ?? (AudioWrapper._coin = Resources.Load<AudioClip>(AudioWrapper.CoinPath));
            }
        }

        /// <summary>
        /// Gets the jump audio clip.
        /// </summary>
        /// <value>
        /// The jump audio clip.
        /// </value>
        private static AudioClip Jump {
            get {
                return AudioWrapper._jump ?? (AudioWrapper._jump = Resources.Load<AudioClip>(AudioWrapper.JumpPath));
            }
        }

        /// <summary>
        /// Plays the checkpoint clip.
        /// </summary>
        public static void PlayCheckpointClip(Vector2 position) {
            AudioSource.PlayClipAtPoint(AudioWrapper.Checkpoint, position, 1f);
        }

        /// <summary>
        /// Plays the coin clip.
        /// </summary>
        public static void PlayCoinClip(Vector2 position) {
            AudioSource.PlayClipAtPoint(AudioWrapper.Coin, position, 1f);
        }

        /// <summary>
        /// Plays the jump clip.
        /// </summary>
        public static void PlayJumpClip(Vector2 position) {
            AudioSource.PlayClipAtPoint(AudioWrapper.Jump, position, 0.5f);
        }
    }
}