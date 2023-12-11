namespace EmmetNetSharp.Models
{
    /// <summary>
    /// Represents the HTML attribute token model.
    /// </summary>
    public class HtmlAttributeToken
    {
        /// <summary>
        /// Gets or sets the name of the attribute.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value of the attribute.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the start position of the attribute name.
        /// </summary>
        public int NameStart { get; set; }

        /// <summary>
        /// Gets or sets the end position of the attribute name.
        /// </summary>
        public int NameEnd { get; set; }

        /// <summary>
        /// Gets or sets the start position of the attribute value.
        /// </summary>
        public int? ValueStart { get; set; }

        /// <summary>
        /// Gets or sets the end position of the attribute value.
        /// </summary>
        public int? ValueEnd { get; set; }
    }
}
