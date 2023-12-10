namespace EmmetNetSharp.Models
{
    /// <summary>
    /// Represents the ExtractedAbbreviation class.
    /// </summary>
    public class ExtractedAbbreviation
    {
        /// <summary>
        /// Gets or sets the abbreviation.
        /// </summary>
        public string Abbreviation { get; set; }

        /// <summary>
        /// Gets or sets the location of the abbreviation.
        /// </summary>
        public int Location { get; set; }

        /// <summary>
        /// Gets or sets the start of the abbreviation.
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// Gets or sets the end of the abbreviation.
        /// </summary>
        public int End { get; set; }
    }
}
