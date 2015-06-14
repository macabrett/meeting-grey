namespace MeetingGrey.Unity.Levels.Builders {

    using BrettMStory.Unity;
    using MeetingGrey.Unity.Levels.Surfaces;
    using UnityEngine;

    /// <summary>
    /// A platform builder.
    /// </summary>
    [RequireComponent(typeof(EdgeCollider2D), typeof(DropPlatform))]
    public class Platform : BaseBehaviour {

        /// <summary>
        /// The height.
        /// </summary>
        private const float Height = 0.5f;

        /// <summary>
        /// The debug color.
        /// </summary>
        [SerializeField]
        private Color _debugColor = Color.black;

        /// <summary>
        /// The effective width.
        /// </summary>
        private float _effectiveWidth;

        /// <summary>
        /// The left sprite.
        /// </summary>
        [SerializeField]
        private Sprite _leftSprite;

        /// <summary>
        /// The middle sprite.
        /// </summary>
        [SerializeField]
        private Sprite _middleSprite;

        /// <summary>
        /// The right sprite.
        /// </summary>
        [SerializeField]
        private Sprite _rightSprite;

        /// <summary>
        /// The width.
        /// </summary>
        [SerializeField]
        private int _width;

        /// <summary>
        /// Awakes this instance.
        /// </summary>
        private void Awake() {
            this._effectiveWidth = this._width - 0.25f;
        }

        /// <summary>
        /// Constructs this instance.
        /// </summary>
        private void Construct() {
            var position = new Vector2(this.transform.position.x + 0.375f, this.transform.position.y + 0.375f);

            for (int i = 0; i < this._width; i++) {
                var sprite = this._middleSprite;
                if (i == 0) {
                    sprite = this._leftSprite;
                } else if (i == this._width - 1) {
                    sprite = this._rightSprite;
                }

                var spriteGameObject = new GameObject(string.Format("Platform {0}", sprite.name));
                spriteGameObject.transform.position = new Vector2(position.x + i, position.y);
                var spriteRenderer = spriteGameObject.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = sprite;
                spriteGameObject.transform.parent = this.Transform;
            }

            var collider = this.GetComponent<EdgeCollider2D>();
            collider.points = new Vector2[] { new Vector2(0f, Platform.Height), new Vector2(this._effectiveWidth, Platform.Height) };
        }

        /// <summary>
        /// Called when [draw gizmos].
        /// </summary>
        protected void OnDrawGizmos() {
            var cubePosition = new Vector3(this.transform.position.x + (this._width - 0.25f) * 0.5f, this.transform.position.y + Platform.Height * 0.5f, this.transform.position.z);
            Gizmos.color = this._debugColor;
            Gizmos.DrawCube(cubePosition, new Vector3((this._width - 0.25f), Platform.Height, 1f));
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        private void Start() {
            this.Construct();
        }
    }
}