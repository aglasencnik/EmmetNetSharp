using System.Collections.Generic;

namespace EmmetNetSharp.Models
{
    /// <summary>
    /// Represents the ResolvedConfig class.
    /// </summary>
    public class ResolvedConfig : BaseConfig
    {
        /// <summary>
        /// Gets or sets the syntax.
        /// </summary>
        public string Syntax { get; set; }

        /// <summary>
        /// Gets or sets the abbreviation context.
        /// </summary>
        public AbbreviationContext Context { get; set; }

        /// <summary>
        /// Gets or sets the text to wrap with abbreviation.
        /// </summary>
        public string[] Text { get; set; }

        /// <summary>
        /// Gets or sets the max amount of repeated elements.
        /// </summary>
        public int? MaxRepeat { get; set; }

        /// <summary>
        /// Converts the object to a JavaScript object.
        /// </summary>
        /// <returns>Dictionary containing the object's properties and values.</returns>
        public new Dictionary<string, object> ToJavaScriptObject()
        {
            var properties = base.ToJavaScriptObject();

            if (!string.IsNullOrEmpty(Syntax))
                properties.Add("syntax", Syntax);

            if (Context != null)
                properties.Add("context", Context.ToJavaScriptObject());

            if (Text != null)
            {
                if (Text.Length == 1)
                    properties.Add("text", Text[0]);
                else
                    properties.Add("text", Text);
            }

            if (MaxRepeat.HasValue)
                properties.Add("maxRepeat", MaxRepeat.Value);

            return properties;
        }
    }
}
