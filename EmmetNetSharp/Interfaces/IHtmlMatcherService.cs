using EmmetNetSharp.Enums;
using EmmetNetSharp.Models;
using System;

namespace EmmetNetSharp.Interfaces
{
    /// <summary>
    /// Interface for the HtmlMatcherService.
    /// </summary>
    public interface IHtmlMatcherService
    {
        /// <summary>
        /// Matches a specific HTML tag within the given source string, starting from a specified position, and using optional scanner options.
        /// The method returns a detailed HtmlMatchedTag object that includes information about the tag name, attributes, and positions of opening and closing tags.
        /// </summary>
        /// <param name="source">The HTML source string in which the match is to be performed. This should be a non-null and non-empty string.</param>
        /// <param name="position">The position in the source string from which the matching process should start. This value must be a non-negative integer.</param>
        /// <param name="scannerOptions">Optional scanner options to customize the matching process. If null, default settings are used.</param>
        /// <returns>An HtmlMatchedTag object containing details of the matched HTML tag, including attributes and tag ranges. Returns null if no match is found or if the result is undefined or invalid.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the input 'source' is null, empty, or consists only of white-space characters.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the 'position' argument is less than 0.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the HTML matching process. The inner exception contains more details about the error.</exception>
        HtmlMatchedTag Match(string source, int position, HtmlMatcherScannerOptions scannerOptions = null);

        /// <summary>
        /// Analyzes an HTML source string to find all balanced HTML tags from a specified position. 
        /// A balanced tag is one where the opening and closing tags are properly nested and matched. 
        /// This method returns an array of HtmlBalancedTag objects, each representing a balanced tag found within the source. 
        /// The method uses optional scanner options for more customized parsing behavior.
        /// </summary>
        /// <param name="source">The HTML source string to be analyzed. It should be a valid, non-null, and non-empty string representing HTML content.</param>
        /// <param name="position">The starting position in the source string from which the search for balanced tags should begin. This value must be a non-negative integer representing an index in the source string.</param>
        /// <param name="scannerOptions">Optional scanner options to customize the tag balancing process. If null, default settings are used.</param>
        /// <returns>An array of HtmlBalancedTag objects, each representing a balanced HTML tag found in the source. Each HtmlBalancedTag object contains details about the tag's name and the positions of its opening and closing tags. Returns an empty array if no balanced tags are found, or null if the result is undefined or invalid.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the 'source' parameter is null, empty, or consists only of white-space characters.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the 'position' parameter is less than 0, indicating an invalid starting position.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the HTML balancing process. The inner exception contains more details about the error.</exception>
        HtmlBalancedTag[] BalanceInward(string source, int position, HtmlMatcherScannerOptions scannerOptions = null);

        /// <summary>
        /// Scans an HTML source string to identify and return an array of outwardly balanced HTML tags from a specified position.
        /// An outwardly balanced tag includes the widest possible range of nested tags that are properly closed and nested within each other.
        /// This method returns an array of HtmlBalancedTag objects, each representing an outwardly balanced tag found in the source.
        /// It allows the use of optional scanner options for customized parsing.
        /// </summary>
        /// <param name="source">The HTML source string to analyze. It should be a non-null, non-empty string that represents valid HTML content.</param>
        /// <param name="position">The starting position in the source string for the outward balancing process. This should be a non-negative integer indicating an index in the source string.</param>
        /// <param name="scannerOptions">Optional parameters to customize the tag balancing process. If null, default settings are applied.</param>
        /// <returns>An array of HtmlBalancedTag objects, each representing an outwardly balanced HTML tag. Each object includes details such as the tag's name, and the positions of its opening and closing tags. Returns an empty array if no balanced tags are found, or null if the result is undefined or invalid.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the 'source' parameter is null, empty, or only contains white-space characters.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the 'position' parameter is less than 0, which is an invalid starting position for the process.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the HTML balancing process. The inner exception provides more details about the nature of the error.</exception>
        HtmlBalancedTag[] BalanceOutward(string source, int position, HtmlMatcherScannerOptions scannerOptions = null);

        /// <summary>
        /// Retrieves an array of HTML attribute tokens from the provided HTML source string.
        /// This method parses the HTML content and extracts attributes, optionally filtering them by a specific attribute name.
        /// Each attribute token includes details such as the attribute's name, value, and the positions of the attribute's name and value within the source string.
        /// </summary>
        /// <param name="source">The HTML source string from which attributes are to be extracted. This should be a valid, non-null, and non-empty string representing HTML content.</param>
        /// <param name="name">Optional parameter specifying the name of the attribute to filter the results. If null or empty, all attributes from the HTML source are returned.</param>
        /// <returns>An array of HtmlAttributeToken objects, each representing an attribute found in the source. Each object includes detailed information about the attribute. Returns null if no attributes are found, or if the result is undefined or invalid.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the 'source' parameter is null, empty, or consists only of white-space characters.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the process of retrieving HTML attributes. The inner exception provides more details about the error.</exception>
        HtmlAttributeToken[] ParseAttributes(string source, string name = null);

        /// <summary>
        /// Scans an HTML source string and returns an array of tuples, each representing a distinct element found in the HTML.
        /// Each tuple contains the element's name, its type as defined by HtmlScannerElementType, and the start and end positions of the element within the source string.
        /// This method can be configured with optional scanner options to customize the scanning process.
        /// </summary>
        /// <param name="source">The HTML source string to scan. It should be a valid, non-null, and non-empty string representing HTML content.</param>
        /// <param name="scannerOptions">Optional scanner options for customizing the scanning process. If null, default settings are used.</param>
        /// <returns>An array of tuples, where each tuple contains: 
        /// 1) The name of the HTML element as a string, 
        /// 2) The type of the element as an HtmlScannerElementType enumeration, 
        /// 3) The start position of the element in the source string, 
        /// 4) The end position of the element in the source string.
        /// Each tuple represents a unique element found during the scan. Returns an empty array if no elements are found.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the 'source' parameter is null, empty, or consists only of white-space characters.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the HTML scanning process. The inner exception provides more details about the error.</exception>
        (string, HtmlScannerElementType, int, int)[] Scan(string source, HtmlMatcherScannerOptions scannerOptions = null);
    }
}
