namespace EmmetNetSharp.Models
{
    /// <summary>
    /// Class that represents a CSS property.
    /// </summary>
    public class CssProperty
    {
        /// <summary>
        /// Gets or sets the name range.
        /// </summary>
        public (int, int) Name { get; set; }

        /// <summary>
        /// Gets or sets the value range.
        /// </summary>
        public (int, int) Value { get; set; }

        /// <summary>
        /// Gets or sets the value token ranges.
        /// </summary>
        public (int, int)[] ValueTokens { get; set; }

        /// <summary>
        /// Gets or sets the before index.
        /// </summary>
        public int Before { get; set; }

        /// <summary>
        /// Gets or sets the after index.
        /// </summary>
        public int After { get; set; }
    }
}
