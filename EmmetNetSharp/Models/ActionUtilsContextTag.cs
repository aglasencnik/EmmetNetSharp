using EmmetNetSharp.Enums;

namespace EmmetNetSharp.Models
{
    /// <summary>
    /// Represents the ActionUtilsContextTag class.
    /// </summary>
    public class ActionUtilsContextTag
    {
        /// <summary>
        /// Gets or sets the name of the tag.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the tag.
        /// </summary>
        public HtmlScannerElementType Type { get; set; }

        /// <summary>
        /// Gets or sets the start position of the tag.
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// Gets or sets the end position of the tag.
        /// </summary>
        public int End { get; set; }

        /// <summary>
        /// Gets or sets the attributes of the tag.
        /// </summary>
        public HtmlAttributeToken[] Attributes { get; set; }
    }
}
