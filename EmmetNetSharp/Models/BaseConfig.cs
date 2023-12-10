using System.Collections.Generic;
using System.Linq;

namespace EmmetNetSharp.Models
{
    /// <summary>
    /// Represents the BaseConfig class.
    /// </summary>
    public class BaseConfig
    {
        /// <summary>
        /// Gets or sets the syntax type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the abbreviation ouput.
        /// </summary>
        public AbbreviationOptions Options { get; set; }

        /// <summary>
        /// Gets or sets the variables.
        /// </summary>
        public Dictionary<string, string> Variables { get; set; }

        /// <summary>
        /// Gets or sets the snippets.
        /// </summary>
        public Dictionary<string, string> Snippets { get; set; }

        /// <summary>
        /// Converts the object to a JavaScript object.
        /// </summary>
        /// <returns>Dictionary containing the object's properties and values.</returns>
        public Dictionary<string, object> ToJavaScriptObject()
        {
            var properties = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(Type))
                properties.Add("type", Type);

            if (Options != null)
                properties.Add("options", Options.ToJavaScriptObject());

            if (Variables != null && Variables.Any())
                properties.Add("variables", Variables);

            if (Snippets != null && Snippets.Any())
                properties.Add("snippets", Snippets);

            return properties;
        }
    }
}
