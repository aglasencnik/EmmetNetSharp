using EmmetNetSharp.Models;

namespace EmmetNetSharp.Interfaces
{
    /// <summary>
    /// Interface for CssMatcherService.
    /// </summary>
    public interface ICssMatcherService
    {
        /// <summary>
        /// Matches a specified CSS pattern within the given source string, starting from a specified position.
        /// The method returns detailed match results, including the types of CSS matches and their positions within the source string.
        /// </summary>
        /// <param name="source">The CSS source string in which the match is to be performed. This should be a non-null and non-empty string.</param>
        /// <param name="position">The position in the source string from which the matching process should start. This value must be a non-negative integer.</param>
        /// <returns>A CssMatchResult object containing details of the match, including types and positions. Returns null if no match is found or if the result is undefined or invalid.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the input 'source' is null, empty, or consists only of white-space characters.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the 'position' argument is less than 0.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the matching process. The inner exception contains more details about the error.</exception>
        CssMatchResult Match(string source, int position);

        /// <summary>
        /// Identifies balanced pairs of characters or substrings within the given source string, starting from a specified position.
        /// This method is typically used for balancing pairs of parentheses, brackets, or similar characters in a string.
        /// </summary>
        /// <param name="source">The source string in which to find balanced pairs. This should be a non-null and non-empty string.</param>
        /// <param name="position">The position in the source string from which the search for balanced pairs should start. This value must be a non-negative integer.</param>
        /// <returns>An array of tuple pairs, where each tuple contains the start and end positions of a balanced pair. Returns an empty array if no balanced pairs are found, or null if the result is undefined or invalid.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the input 'source' is null, empty, or consists only of white-space characters.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the 'position' argument is less than 0.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the balancing process. The inner exception contains more details about the error.</exception>
        (int, int)[] BalanceInward(string source, int position);

        /// <summary>
        /// Identifies and returns pairs of characters or substrings within the given source string that form a balanced structure, expanding outward from a specified position. 
        /// This method is typically used for finding balanced pairs of parentheses, brackets, or similar characters in a string, considering a broader scope from the given position.
        /// </summary>
        /// <param name="source">The source string in which to find balanced pairs. This should be a non-null and non-empty string.</param>
        /// <param name="position">The position in the source string from which the search for balanced pairs should commence. This value must be a non-negative integer.</param>
        /// <returns>An array of tuple pairs, where each tuple contains the start and end positions of a balanced pair. Returns an empty array if no balanced pairs are found, or null if the result is undefined or invalid.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the input 'source' is null, empty, or consists only of white-space characters.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the 'position' argument is less than 0.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the outward balancing process. The inner exception contains more details about the error.</exception>
        (int, int)[] BalanceOutward(string source, int position);

        /// <summary>
        /// Scans the provided source string and identifies various segments based on a predefined criterion.
        /// Each identified segment is represented by its type, start and end positions, and an additional delimiter value.
        /// This method is particularly useful for parsing and analyzing structured text like CSS.
        /// </summary>
        /// <param name="source">The source string to be scanned. This should be a non-null and non-empty string.</param>
        /// <returns>An array of tuples, where each tuple contains a string representing the segment type, and three integers representing the start position, end position, and a delimiter value for each identified segment.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the input 'source' is null, empty, or consists only of white-space characters.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the scanning process. The inner exception contains more details about the error.</exception>
        (string, int, int, int)[] Scan(string source);

        /// <summary>
        /// Splits the given string value into pairs of integers based on a specific criterion defined in the 'splitValue' function.
        /// This method is particularly useful for processing and splitting CSS values or similar formatted strings.
        /// </summary>
        /// <param name="value">The string value to be split. This should be a non-null and non-empty string.</param>
        /// <param name="offset">An optional parameter specifying the offset at which the split operation should start. If null, the split starts from the beginning of the string.</param>
        /// <returns>An array of tuples, where each tuple contains two integers representing a segment of the split string. Returns null if the split cannot be performed, or if the resulting segments are undefined or invalid.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the input 'value' is null, empty, or consists only of white-space characters.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the splitting process. The inner exception contains more details about the error.</exception>
        (int, int)[] SplitValue(string value, int? offset = null);
    }
}
