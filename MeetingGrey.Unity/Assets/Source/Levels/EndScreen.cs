namespace MeetingGrey.Unity.Levels {

    using MeetingGrey.Unity.Constants;
    using MeetingGrey.Unity.Wrappers;
    using UnityEngine;

    /// <summary>
    /// The end screen.
    /// </summary>
    public class EndScreen : MonoBehaviour {

        /// <summary>
        /// Awakes this instance.
        /// </summary>
        private void Awake() {
            PlayerPrefsWrapper.LastLevelPlayed = 0;
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        private void Update() {
            if (Input.GetButtonDown(InputConstants.Jump) || Input.GetButtonDown(InputConstants.Pause) || Input.GetKeyDown(KeyCode.Return)) {
                Application.LoadLevel(SceneConstants.MainMenu);
            }
        }
    }
}