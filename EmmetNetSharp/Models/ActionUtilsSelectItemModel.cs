namespace EmmetNetSharp.Models
{
    /// <summary>
    /// Represents a select item model for the action utils.
    /// </summary>
    public class ActionUtilsSelectItemModel
    {
        /// <summary>
        /// Gets or sets the start index of the selection.
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// Gets or sets the end index of the selection.
        /// </summary>
        public int End { get; set; }

        /// <summary>
        /// Gets or sets the ranges of the selection.
        /// </summary>
        public (int, int)[] Ranges { get; set; }
    }
}
