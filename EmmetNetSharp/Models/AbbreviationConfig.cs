using System.Collections.Generic;

namespace EmmetNetSharp.Models
{
    /// <summary>
    /// Represents the abbreviation configuration class.
    /// </summary>
    public class AbbreviationConfig
    {
        /// <summary>
        /// Gets or sets the abbreviation options.
        /// </summary>
        public AbbreviationOptions Options { get; set; }

        /// <summary>
        /// Converts the object to a JavaScript object.
        /// </summary>
        /// <returns>Dictionary containing the object's properties and values.</returns>
        public Dictionary<string, object> ToJavaScriptObject()
        {
            var properties = new Dictionary<string, object>();

            if (Options != null)
                properties.Add("options", Options.ToJavaScriptObject());

            return properties;
        }
    }
}
