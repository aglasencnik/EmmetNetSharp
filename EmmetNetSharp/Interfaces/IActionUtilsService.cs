using EmmetNetSharp.Models;

namespace EmmetNetSharp.Interfaces
{
    /// <summary>
    /// Interface for the ActionUtilsService.
    /// </summary>
    public interface IActionUtilsService
    {
        /// <summary>
        /// Finds and returns a matching HTML tag at a specified position within the provided HTML source string.
        /// This method scans the source string for an HTML tag that matches a certain criteria based on the given position and optional scanner options. 
        /// It returns an ActionUtilsTagMatch object containing information about the found tag, including its name and the ranges of its opening and closing tags.
        /// </summary>
        /// <param name="source">The HTML source string in which to find the tag match. This should be a valid, non-null, and non-empty string representing HTML content.</param>
        /// <param name="position">The position in the source string at which to start the search for a matching tag. This should be a non-negative integer indicating an index in the source string.</param>
        /// <param name="options">Optional scanner options to customize the tag search process. If null, default settings are used.</param>
        /// <returns>An ActionUtilsTagMatch object containing information about the matched tag, such as its name and the ranges of its opening and closing tags. Returns null if no match is found, or if the result is undefined or invalid.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the 'source' parameter is null, empty, or consists only of white-space characters.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the 'position' parameter is less than 0, indicating an invalid starting position for the search.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the tag matching process. The inner exception provides more details about the error.</exception>
        ActionUtilsTagMatch FindTagMatch(string source, int position, ActionUtilsScannerOptions options = null);

        /// <summary>
        /// Retrieves a specific section of CSS from the provided source string at a specified position.
        /// This method scans the CSS code to identify and return details about a particular section, including its start and end positions, and optionally its properties.
        /// The method can be configured to include detailed information about each CSS property within the selected section.
        /// </summary>
        /// <param name="code">The source string containing CSS code to be analyzed. This should be a valid, non-null, and non-empty string.</param>
        /// <param name="position">The position in the source string at which to start the CSS section extraction. This should be a non-negative integer indicating an index in the source string.</param>
        /// <param name="includeProperties">Optional boolean parameter to specify whether to include details about CSS properties in the section. If true, additional property details are included; if false or null, they are omitted.</param>
        /// <returns>An ActionUtilsCssSection object containing details of the CSS section, such as start and end positions, body start and end positions, and optionally an array of CSS properties. Returns null if no section is found or if the result is undefined or invalid.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the 'code' parameter is null, empty, or consists only of white-space characters.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the 'position' parameter is less than 0, indicating an invalid starting position for the extraction.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the process of retrieving the CSS section. The inner exception provides more details about the error.</exception>
        ActionUtilsCssSection GetCssSection(string code, int position, bool? includeProperties = null);

        /// <summary>
        /// Identifies and returns details of an open HTML tag located at a specific position within the provided source string.
        /// This method scans the source code at the given position to find an opening tag and returns an ActionUtilsContextTag object containing information about the tag.
        /// The returned object includes the tag's name, type, position, and a collection of its attributes.
        /// </summary>
        /// <param name="code">The source string to scan for an open HTML tag. This should be a valid, non-null, and non-empty string representing HTML content.</param>
        /// <param name="position">The position in the source string to begin searching for an open tag. This should be a non-negative integer indicating an index in the source string.</param>
        /// <returns>An ActionUtilsContextTag object containing details about the identified open tag, such as its name, type, position, and attributes. Returns null if no open tag is found or if the result is undefined or invalid.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the 'code' parameter is null, empty, or consists only of white-space characters.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the 'position' parameter is less than 0, indicating an invalid starting position for the search.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the process of identifying the open tag. The inner exception provides more details about the error.</exception>
        ActionUtilsContextTag GetOpenTag(string code, int position);

        /// <summary>
        /// Retrieves an array of matching HTML tags from the provided source string.
        /// This method scans the source code for HTML tags and returns a collection of ActionUtilsTagMatch objects, each representing a found tag.
        /// Each object contains details about the tag, including its name and the ranges of its opening and closing tags.
        /// Optional scanner options can be provided to customize the tag matching process.
        /// </summary>
        /// <param name="code">The source string to scan for HTML tags. This should be a valid, non-null, and non-empty string.</param>
        /// <param name="options">Optional scanner options for customizing the tag matching process. If null, default settings are applied.</param>
        /// <returns>An array of ActionUtilsTagMatch objects, each representing a found HTML tag. Returns null if no matches are found, or if the result is undefined or invalid.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the 'code' parameter is null, empty, or consists only of white-space characters.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the tag matching process. The inner exception provides more details about the error.</exception>
        ActionUtilsTagMatch[] GetTagMatches(string code, ActionUtilsScannerOptions options = null);

        /// <summary>
        /// Selects a CSS item from a specified position in the provided source string. 
        /// This method identifies and returns details of a CSS item, such as a style declaration or selector, located at or near the specified position.
        /// It allows specifying the direction of selection (previous or next item) through an optional boolean parameter.
        /// </summary>
        /// <param name="code">The source string containing CSS code to scan for item selection. This should be a valid, non-null, and non-empty string.</param>
        /// <param name="position">The position in the source string from which the item selection should start. This should be a non-negative integer indicating an index in the source string.</param>
        /// <param name="isPrev">Optional boolean parameter to specify the direction of selection. If true, selects the previous item; if false, selects the next item. If null, the method applies its default selection behavior.</param>
        /// <returns>An ActionUtilsSelectItemModel object containing details of the selected CSS item, such as its start and end positions, and any specific ranges within the item. Returns null if no suitable item is found or if the result is undefined or invalid.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the 'code' parameter is null, empty, or consists only of white-space characters.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the 'position' parameter is less than 0, indicating an invalid starting position for the selection.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the process of selecting the CSS item. The inner exception provides more details about the error.</exception>
        ActionUtilsSelectItemModel SelectItemCss(string code, int position, bool? isPrev = null);

        /// <summary>
        /// Selects an HTML item from a specified position in the provided source string.
        /// This method identifies and returns details of an HTML item, such as a tag or attribute, located at or near the specified position.
        /// It allows specifying the direction of selection (previous or next item) through an optional boolean parameter.
        /// </summary>
        /// <param name="code">The source string containing HTML code to scan for item selection. This should be a valid, non-null, and non-empty string.</param>
        /// <param name="position">The position in the source string from which the item selection should start. This should be a non-negative integer indicating an index in the source string.</param>
        /// <param name="isPrev">Optional boolean parameter to specify the direction of selection. If true, selects the previous item; if false, selects the next item. If null, the method applies its default selection behavior.</param>
        /// <returns>An ActionUtilsSelectItemModel object containing details of the selected HTML item, such as its start and end positions, and any specific ranges within the item. Returns null if no suitable item is found or if the result is undefined or invalid.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the 'code' parameter is null, empty, or consists only of white-space characters.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the 'position' parameter is less than 0, indicating an invalid starting position for the selection.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the process of selecting the HTML item. The inner exception provides more details about the error.</exception>
        ActionUtilsSelectItemModel SelectItemHtml(string code, int position, bool? isPrev = null);
    }
}
