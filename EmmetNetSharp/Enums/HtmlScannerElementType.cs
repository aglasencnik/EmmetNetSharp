namespace EmmetNetSharp.Enums
{
    /// <summary>
    /// Enumeration of HTML scanner element types.
    /// </summary>
    public enum HtmlScannerElementType
    {
        /// <summary>
        /// Open element type.
        /// </summary>
        Open = 1,

        /// <summary>
        /// Close element type.
        /// </summary>
        Close = 2,

        /// <summary>
        /// Self close element type.
        /// </summary>
        SelfClose = 3,

        /// <summary>
        /// CData element type.
        /// </summary>
        CData = 4,

        /// <summary>
        /// Processing instruction element type.
        /// </summary>
        ProcessingInstruction = 5,

        /// <summary>
        /// Comment element type.
        /// </summary>
        Comment = 6,

        /// <summary>
        /// ERB element type.
        /// </summary>
        ERB = 7
    }
}
