namespace EmmetNetSharp.Models
{
    /// <summary>
    /// Represents the action utils tag match model class.
    /// </summary>
    public class ActionUtilsTagMatch
    {
        /// <summary>
        /// Gets or sets the name of the tag.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the open tag range.
        /// </summary>
        public (int, int) OpenRange { get; set; }

        /// <summary>
        /// Gets or sets the close tag range.
        /// </summary>
        public (int, int)? CloseRange { get; set; }
    }
}
