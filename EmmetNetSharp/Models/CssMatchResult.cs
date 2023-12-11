using EmmetNetSharp.Enums;

namespace EmmetNetSharp.Models
{
    /// <summary>
    /// Represents the css match result model.
    /// </summary>
    public class CssMatchResult
    {
        /// <summary>
        /// Gets or sets the type of the match.
        /// </summary>
        public CssMatchType Type { get; set; }

        /// <summary>
        /// Gets or sets the start index.
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// Gets or sets the end index.
        /// </summary>
        public int End { get; set; }

        /// <summary>
        /// Gets or sets the body start index.
        /// </summary>
        public int BodyStart { get; set; }

        /// <summary>
        /// Gets or sets the body end index.
        /// </summary>
        public int BodyEnd { get; set; }
    }
}
