using System.Collections.Generic;

namespace EmmetNetSharp.Models
{
    /// <summary>
    /// Represents the abbreviation extract options class.
    /// </summary>
    public class AbbreviationExtractOptions
    {
        /// <summary>
        /// Gets or sets whether to allow parser to look ahead of pos index for searching of missing abbreviation parts.
        /// </summary>
        public bool? LookAhead { get; set; }

        /// <summary>
        /// Gets or sets the type of context syntax of expanded abbreviation.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the string that should precede abbreviation in order to extract it.
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Converts the object to a JavaScript object.
        /// </summary>
        /// <returns>Dictionary containing the object's properties and values.</returns>
        public Dictionary<string, object> ToJavaScriptObject()
        {
            var properties = new Dictionary<string, object>();

            if (LookAhead.HasValue)
                properties.Add("lookAhead", LookAhead.Value);

            if (!string.IsNullOrEmpty(Type))
                properties.Add("type", Type);

            if (!string.IsNullOrEmpty(Prefix))
                properties.Add("prefix", Prefix);

            return properties;
        }
    }
}
