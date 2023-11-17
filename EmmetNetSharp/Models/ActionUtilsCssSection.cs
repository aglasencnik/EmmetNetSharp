namespace EmmetNetSharp.Models
{
    /// <summary>
    /// Class that represents a CSS section for the ActionUtils class.
    /// </summary>
    public class ActionUtilsCssSection
    {
        /// <summary>
        /// Gets or sets the start index of the section.
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// Gets or sets the end index of the section.
        /// </summary>
        public int End { get; set; }

        /// <summary>
        /// Gets or sets the start index of the body of the section.
        /// </summary>
        public int BodyStart { get; set; }

        /// <summary>
        /// Gets or sets the end index of the body of the section.
        /// </summary>
        public int BodyEnd { get; set; }

        /// <summary>
        /// Gets or sets the properties of the section.
        /// </summary>
        public CssProperty[] Properties { get; set; }
    }
}
