namespace MeetingGrey.Unity.Wrappers {

    using UnityEngine;

    /// <summary>
    /// Wrapper for audio.
    /// </summary>
    public static class AudioWrapper {

        /// <summary>
        /// The bounce path.
        /// </summary>
        private const string BouncePath = "Sound/Bounce";

        /// <summary>
        /// The checkpoint path.
        /// </summary>
        private const string CheckpointPath = "Sound/Checkpoint";

        /// <summary>
        /// The coin path.
        /// </summary>
        private const string CoinPath = "Sound/Coin";

        /// <summary>
        /// The death path.
        /// </summary>
        private const string DeathPath = "Sound/Death";

        /// <summary>
        /// The jump path.
        /// </summary>
        private const string JumpPath = "Sound/Jump";

        /// <summary>
        /// The menu path.
        /// </summary>
        private const string MenuPath = "Sound/Menu";

        /// <summary>
        /// The swap path.
        /// </summary>
        private const string SwapPath = "Sound/Swap";

        /// <summary>
        /// The bounce audio clip.
        /// </summary>
        private static AudioClip _bounce;

        /// <summary>
        /// The checkpoint audio clip.
        /// </summary>
        private static AudioClip _checkpoint;

        /// <summary>
        /// The coin audio clip.
        /// </summary>
        private static AudioClip _coin;

        /// <summary>
        /// The death audio clip.
        /// </summary>
        private static AudioClip _death;

        /// <summary>
        /// The jump audio clip.
        /// </summary>
        private static AudioClip _jump;

        /// <summary>
        /// The menu audio clip.
        /// </summary>
        private static AudioClip _menu;

        /// <summary>
        /// The swap audio clip.
        /// </summary>
        private static AudioClip _swap;

        /// <summary>
        /// Gets the bounce audio clip.
        /// </summary>
        /// <value>
        /// The bounce audio clip.
        /// </value>
        private static AudioClip Bounce {
            get {
                return AudioWrapper._bounce ?? (AudioWrapper._bounce = Resources.Load<AudioClip>(AudioWrapper.BouncePath));
            }
        }

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
        /// Gets the death audio clip.
        /// </summary>
        /// <value>
        /// The death audio clip.
        /// </value>
        private static AudioClip Death {
            get {
                return AudioWrapper._death ?? (AudioWrapper._death = Resources.Load<AudioClip>(AudioWrapper.DeathPath));
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
        /// Gets the menu audio clip.
        /// </summary>
        /// <value>
        /// The menu audio clip.
        /// </value>
        private static AudioClip Menu {
            get {
                return AudioWrapper._menu ?? (AudioWrapper._menu = Resources.Load<AudioClip>(AudioWrapper.MenuPath));
            }
        }

        /// <summary>
        /// Gets the swap audio clip.
        /// </summary>
        /// <value>
        /// The swap audio clip.
        /// </value>
        private static AudioClip Swap {
            get {
                return AudioWrapper._swap ?? (AudioWrapper._swap = Resources.Load<AudioClip>(AudioWrapper.SwapPath));
            }
        }

        /// <summary>
        /// Plays the bounce clip.
        /// </summary>
        /// <param name="position">The position.</param>
        public static void PlayBounceClip(Vector2 position) {
            AudioSource.PlayClipAtPoint(AudioWrapper.Bounce, position, 0.5f);
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
        /// Plays the death clip.
        /// </summary>
        /// <param name="position">The position.</param>
        public static void PlayDeathClip(Vector2 position) {
            AudioSource.PlayClipAtPoint(AudioWrapper.Death, position, 1f);
        }

        /// <summary>
        /// Plays the jump clip.
        /// </summary>
        public static void PlayJumpClip(Vector2 position) {
            AudioSource.PlayClipAtPoint(AudioWrapper.Jump, position, 0.5f);
        }

        /// <summary>
        /// Plays the menu clip.
        /// </summary>
        /// <param name="position">The position.</param>
        public static void PlayMenuClip(Vector2 position) {
            AudioSource.PlayClipAtPoint(AudioWrapper.Menu, position, 0.5f);
        }

        /// <summary>
        /// Plays the swap clip.
        /// </summary>
        /// <param name="position">The position.</param>
        public static void PlaySwapClip(Vector2 position) {
            AudioSource.PlayClipAtPoint(AudioWrapper.Swap, position, 1f);
        }

        public static void StartMusic() {
        }

        public static void StopMusic() {
        }
    }
}