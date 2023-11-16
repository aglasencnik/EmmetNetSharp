namespace EmmetNetSharp.Models
{
    /// <summary>
    /// Represents the HTML matched tag model.
    /// </summary>
    public class HtmlMatchedTag
    {
        /// <summary>
        /// Gets or sets the name of the tag.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the attributes of the tag.
        /// </summary>
        public HtmlAttributeToken[] Attributes { get; set; }

        /// <summary>
        /// Gets or sets the range of the opening tag.
        /// </summary>
        public (int, int) OpeningTagRange { get; set; }

        /// <summary>
        /// Gets or sets the range of the closing tag.
        /// </summary>
        public (int, int)? ClosingTagRange { get; set; }
    }
}
