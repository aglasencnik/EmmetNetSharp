namespace EmmetNetSharp.Models
{
    /// <summary>
    /// Represents the HTML balanced tag model.
    /// </summary>
    public class HtmlBalancedTag
    {
        /// <summary>
        /// Gets or sets the name of the tag.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the opening tag range.
        /// </summary>
        public (int, int) OpeningTagRange { get; set; }

        /// <summary>
        /// Gets or sets the closing tag range.
        /// </summary>
        public (int, int)? ClosingTagRange { get; set; }
    }
}
