namespace EmmetNetSharp.Models
{
    /// <summary>
    /// Represents the BaseScannerOptions class.
    /// </summary>
    public abstract class BaseScannerOptions
    {
        /// <summary>
        /// Gets or sets the type of the scanner.
        /// </summary>
        public bool xml { get; set; }

        /// <summary>
        /// Gets or sets the list of tags that should have special parsing rules.
        /// </summary>
        public string[] special { get; set; }

        /// <summary>
        /// Gets or sets the list of elements that should be treated as empty.
        /// </summary>
        public string[] empty { get; set; }

        /// <summary>
        /// Gets or sets whether it should return all tokens.
        /// </summary>
        public bool allTokens { get; set; }
    }
}
