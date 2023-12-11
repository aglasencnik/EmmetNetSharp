using System.Collections.Generic;
using System.Linq;

namespace EmmetNetSharp.Models
{
    /// <summary>
    /// Represents the abbreviation context class.
    /// </summary>
    public class AbbreviationContext
    {
        /// <summary>
        /// Gets or sets the name of the context.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the attributes of the context.
        /// </summary>
        public Dictionary<string, string> Attributes { get; set; }

        /// <summary>
        /// Converts the object to a JavaScript object.
        /// </summary>
        /// <returns>Dictionary containing the object's properties and values.</returns>
        public Dictionary<string, object> ToJavaScriptObject()
        {
            var properties = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(Name))
                properties.Add("name", Name);

            if (Attributes != null && Attributes.Any())
                properties.Add("attributes", Attributes);

            return properties;
        }
    }
}
