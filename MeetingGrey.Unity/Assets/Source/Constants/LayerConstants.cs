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
        /// The surface background layer name.
        /// </summary>
        public const string SurfaceBackground = "SurfaceBackground";

        /// <summary>
        /// The surface foreground layer name.
        /// </summary>
        public const string SurfaceForeground = "SurfaceForeground";

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
                return 1 << LayerConstants.NoneLayer;
            }
        }

        /// <summary>
        /// Gets the surface background layer.
        /// </summary>
        /// <value>
        /// The surface background layer.
        /// </value>
        public static int SurfaceBackgroundLayer {
            get {
                return LayerMask.NameToLayer(LayerConstants.SurfaceBackground);
            }
        }

        /// <summary>
        /// Gets the surface background layer mask.
        /// </summary>
        /// <value>
        /// The surface background layer mask.
        /// </value>
        public static int SurfaceBackgroundLayerMask {
            get {
                return 1 << LayerConstants.SurfaceBackgroundLayer;
            }
        }

        /// <summary>
        /// Gets the surface foreground layer.
        /// </summary>
        /// <value>
        /// The surface foreground layer.
        /// </value>
        public static int SurfaceForegroundLayer {
            get {
                return LayerMask.NameToLayer(LayerConstants.SurfaceForeground);
            }
        }

        /// <summary>
        /// Gets the surface foreground layer mask.
        /// </summary>
        /// <value>
        /// The surface foreground layer mask.
        /// </value>
        public static int SurfaceForegroundLayerMask {
            get {
                return 1 << LayerConstants.SurfaceForegroundLayer;
            }
        }
    }
}