namespace MeetingGrey.Unity.Text {

    using System.Collections;
    using System.Collections.Generic;
    using Assets.Source.Constants;
    using UnityEngine;

    /// <summary>
    /// Pixel font.
    /// </summary>
    public class PixelFont {

        /// <summary>
        /// The instance.
        /// </summary>
        private static PixelFont _instance = new PixelFont();

        /// <summary>
        /// The letters.
        /// </summary>
        private readonly Dictionary<char, Sprite> _letters = new Dictionary<char, Sprite>();

        /// <summary>
        /// The numbers.
        /// </summary>
        private readonly Dictionary<char, Sprite> _numbers = new Dictionary<char, Sprite>();

        /// <summary>
        /// Initializes the <see cref="PixelFont"/> class.
        /// </summary>
        static PixelFont() {
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="PixelFont"/> class from being created.
        /// </summary>
        private PixelFont() {
            var numbers = Resources.LoadAll(ResourceConstants.NumbersPath);

            foreach (var number in numbers) {
                var sprite = number as Sprite;
                if (sprite != null) {
                    this._numbers.Add(sprite.ToString()[0], sprite);
                }
            }

            var letters = Resources.LoadAll(ResourceConstants.LettersPath);

            foreach (var letter in letters) {
                var sprite = letter as Sprite;
                if (sprite != null) {
                    this._letters.Add(sprite.ToString()[0], sprite);
                }
            }
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static PixelFont Instance {
            get {
                return PixelFont._instance;
            }
        }

        /// <summary>
        /// Gets the character.
        /// </summary>
        /// <returns>The character.</returns>
        /// <param name="character">Character.</param>
        public Sprite GetCharacter(char character) {
            Sprite sprite;

            if (this._letters.TryGetValue(character, out sprite)) {
                return sprite;
            } else if (this._numbers.TryGetValue(character, out sprite)) {
                return sprite;
            }

            return null;
        }
    }
}