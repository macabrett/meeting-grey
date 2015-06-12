namespace MeetingGrey.Unity.Constants {

    using UnityEngine;

    /// <summary>
    /// Layer constants.
    /// </summary>
    public class LayerConstants {

        /// <summary>
        /// The none layer name.
        /// </summary>
        public const string None = "None";

        /// <summary>
        /// The surface layer name.
        /// </summary>
        public const string Surface = "Surface";

        /// <summary>
        /// Gets the none layer.
        /// </summary>
        /// <value>
        /// The none layer.
        /// </value>
        public static int NoneLayer {
            get {
                return LayerMask.NameToLayer(LayerConstants.None);
            }
        }

        /// <summary>
        /// Gets the none layer mask.
        /// </summary>
        /// <value>
        /// The none layer mask.
        /// </value>
        public static int NoneLayerMask {
            get {
                return 1 << LayerMask.NameToLayer(LayerConstants.None);
            }
        }

        /// <summary>
        /// Gets the surface layer.
        /// </summary>
        /// <value>
        /// The surface layer.
        /// </value>
        public static int SurfaceLayer {
            get {
                return LayerMask.NameToLayer(LayerConstants.Surface);
            }
        }

        /// <summary>
        /// Gets the surface layer mask.
        /// </summary>
        /// <value>
        /// The surface layer mask.
        /// </value>
        public static int SurfaceLayerMask {
            get {
                return 1 << LayerMask.NameToLayer(LayerConstants.Surface);
            }
        }
    }
}