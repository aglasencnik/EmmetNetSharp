using System.Collections.Generic;

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
        public bool Xml { get; set; }

        /// <summary>
        /// Gets or sets the list of tags that should have special parsing rules.
        /// </summary>
        public string[] Special { get; set; }

        /// <summary>
        /// Gets or sets the list of elements that should be treated as empty.
        /// </summary>
        public string[] Empty { get; set; }

        /// <summary>
        /// Gets or sets whether it should return all tokens.
        /// </summary>
        public bool AllTokens { get; set; }

        /// <summary>
        /// Converts the object to a JavaScript object.
        /// </summary>
        /// <returns>Dictionary containing the object's properties and values.</returns>
        public Dictionary<string, object> ToJavaScriptObject()
        {
            return new Dictionary<string, object>
            {
                { "xml", Xml },
                { "special", Special },
                { "empty", Empty },
                { "allTokens", AllTokens}
            };
        }
    }
}
