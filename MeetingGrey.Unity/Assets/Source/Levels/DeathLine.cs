namespace MeetingGrey.Unity.Levels {

    using System.Collections;
    using BrettMStory.Unity;
    using MeetingGrey.Unity.Constants;
    using UnityEngine;

    /// <summary>
    /// The death line.
    /// </summary>
    public class DeathLine : BaseBehaviour {

        /// <summary>
        /// The singleton instance.
        /// </summary>
        private static DeathLine _instance;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static DeathLine Instance {
            get {
                return DeathLine._instance;
            }
        }

        /// <summary>
        /// Called when [draw gizmos].
        /// </summary>
        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(this.transform.position, this.transform.position + Vector3.right * 1000f);
        }

        /// <summary>
        /// Awakes this instance.
        /// </summary>
        private void Awake() {
            DeathLine._instance = this;
            this.gameObject.tag = TagConstants.DeathLine;
        }
    }
}