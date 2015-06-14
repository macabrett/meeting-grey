namespace MeetingGrey.Unity.Levels.Builders {

    using System.Collections;
    using System.Collections.Generic;
    using BrettMStory.Unity;
    using UnityEngine;

    /// <summary>
    /// A block.
    /// </summary>
    [RequireComponent(typeof(EdgeCollider2D))]
    public class Block : BaseBehaviour {

        /// <summary>
        /// The tiles owned by this block.
        /// </summary>
        private readonly List<GameObject> _tiles = new List<GameObject>();

        /// <summary>
        /// The bottom center sprite.
        /// </summary>
        [SerializeField]
        private Sprite _bottomCenter;

        /// <summary>
        /// The bottom left sprite.
        /// </summary>
        [SerializeField]
        private Sprite _bottomLeft;

        /// <summary>
        /// The bottom right sprite.
        /// </summary>
        [SerializeField]
        private Sprite _bottomRight;

        /// <summary>
        /// The debug color.
        /// </summary>
        [SerializeField]
        private Color _debugColor = Color.black;

        /// <summary>
        /// The height.
        /// </summary>
        [SerializeField]
        private int _height;

        /// <summary>
        /// The middle center sprite.
        /// </summary>
        [SerializeField]
        private Sprite _middleCenter;

        /// <summary>
        /// The middle left sprite.
        /// </summary>
        [SerializeField]
        private Sprite _middleLeft;

        /// <summary>
        /// The middle right sprite.
        /// </summary>
        [SerializeField]
        private Sprite _middleRight;

        /// <summary>
        /// The top center sprite.
        /// </summary>
        [SerializeField]
        private Sprite _topCenter;

        /// <summary>
        /// The top left sprite.
        /// </summary>
        [SerializeField]
        private Sprite _topLeft;

        /// <summary>
        /// The top right sprite.
        /// </summary>
        [SerializeField]
        private Sprite _topRight;

        /// <summary>
        /// The width.
        /// </summary>
        [SerializeField]
        private float _width;

        /// <summary>
        /// Constructs this instance.
        /// </summary>
        private void Construct() {
            var position = new Vector2(this.transform.position.x + 0.75f, this.transform.position.y + 0.25f);
            this.ConstructBottomRow(position);

            for (int i = 0; i < (this._height) - 2; i++) {
                position += Vector2.up;
                this.ConstructMiddleRow(position);
            }

            position += Vector2.up;
            this.ConstructTopRow(position);
            var collider = this.gameObject.GetComponent<EdgeCollider2D>();
            var leftPoint = new Vector2(0f, this._height);
            var rightPoint = new Vector2(this._width, this._height);
            collider.points = new Vector2[] { leftPoint, rightPoint };

            foreach (var tile in this._tiles) {
                tile.transform.parent = this.transform;
            }
        }

        /// <summary>
        /// Constructs the bottom row.
        /// </summary>
        /// <param name="startPosition">The start position.</param>
        private void ConstructBottomRow(Vector2 startPosition) {
            for (int i = 0; i < this._width; i++) {
                var sprite = this._bottomCenter;

                if (i == 0) {
                    sprite = this._bottomLeft;
                } else if (i == this._width - 1) {
                    sprite = this._bottomRight;
                }

                var spriteGameObject = new GameObject(string.Format("Bottom {0}", sprite.name));
                var spriteRenderer = spriteGameObject.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = sprite;
                spriteGameObject.transform.position = new Vector2(startPosition.x + i, startPosition.y);
                this._tiles.Add(spriteGameObject);
            }
        }

        /// <summary>
        /// Constructs the middle row.
        /// </summary>
        /// <param name="startPosition">The start position.</param>
        private void ConstructMiddleRow(Vector2 startPosition) {
            for (int i = 0; i < this._width; i++) {
                var sprite = this._middleCenter;

                if (i == 0) {
                    sprite = this._middleLeft;
                } else if (i == this._width - 1) {
                    sprite = this._middleRight;
                }

                var spriteGameObject = new GameObject(string.Format("Middle {0}", sprite.name));
                var spriteRenderer = spriteGameObject.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = sprite;
                spriteGameObject.transform.position = new Vector2(startPosition.x + i, startPosition.y);
                this._tiles.Add(spriteGameObject);
            }
        }

        /// <summary>
        /// Constructs the top row.
        /// </summary>
        /// <param name="startPosition">The start position.</param>
        private void ConstructTopRow(Vector2 startPosition) {
            for (int i = 0; i < this._width; i++) {
                var sprite = this._topCenter;

                if (i == 0) {
                    sprite = this._topLeft;
                } else if (i == this._width - 1) {
                    sprite = this._topRight;
                }

                var spriteGameObject = new GameObject(string.Format("Top {0}", sprite.name));
                var spriteRenderer = spriteGameObject.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = sprite;
                spriteGameObject.transform.position = new Vector2(startPosition.x + i, startPosition.y);
                this._tiles.Add(spriteGameObject);
            }
        }

        /// <summary>
        /// Called when [draw gizmos].
        /// </summary>
        private void OnDrawGizmos() {
            var cubePosition = new Vector3(this.transform.position.x + this._width * 0.5f, this.transform.position.y + this._height * 0.5f, this.transform.position.z);
            Gizmos.color = this._debugColor;
            Gizmos.DrawCube(cubePosition, new Vector3(this._width, this._height, 1f));
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        private void Start() {
            this.Construct();
        }
    }
}