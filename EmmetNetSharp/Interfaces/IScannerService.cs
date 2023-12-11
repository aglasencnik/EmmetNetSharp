namespace EmmetNetSharp.Interfaces
{
    /// <summary>
    /// Interface for the ScannerService.
    /// </summary>
    public interface IScannerService
    {
        /// <summary>
        /// Determines if a given character code represents an alphabetic character, optionally within a specific range.
        /// This method checks if the character associated with the provided code is an alphabet character and can also validate whether it falls within a specified numeric range.
        /// </summary>
        /// <param name="code">The character code to check. This should be a non-negative integer representing a Unicode character code.</param>
        /// <param name="from">Optional parameter specifying the lower bound of the character code range for validation. If null, no lower bound is enforced.</param>
        /// <param name="to">Optional parameter specifying the upper bound of the character code range for validation. If null, no upper bound is enforced.</param>
        /// <returns>True if the character code represents an alphabetic character and, if specified, falls within the provided range; otherwise, false.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the 'code' parameter is less than 0, indicating an invalid character code.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the validation process. The inner exception provides more details about the error.</exception>
        bool IsAlpha(int code, int? from = null, int? to = null);

        /// <summary>
        /// Checks whether a specified character code represents an alphanumeric character.
        /// This method evaluates if the character associated with the provided Unicode code is either an alphabet letter or a numeric digit.
        /// </summary>
        /// <param name="code">The character code to check. This should be a non-negative integer representing a Unicode character code.</param>
        /// <returns>True if the character code corresponds to an alphanumeric character (either a letter or a digit); otherwise, false.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the 'code' parameter is less than 0, indicating an invalid character code.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the character code evaluation. The inner exception provides more details about the error.</exception>
        bool IsAlphaNumeric(int code);

        /// <summary>
        /// Determines if a given character code is part of an alphanumeric word.
        /// This method evaluates whether the character associated with the provided Unicode code is typically considered part of an alphanumeric word, meaning it could be an alphabet letter or a numeric digit commonly used in word formation.
        /// </summary>
        /// <param name="code">The character code to check. This should be a non-negative integer representing a Unicode character code.</param>
        /// <returns>True if the character code is part of an alphanumeric word (either a letter or a digit commonly used in words); otherwise, false.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the 'code' parameter is less than 0, indicating an invalid character code.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the evaluation process. The inner exception provides more details about the error.</exception>
        bool IsAlphaNumericWord(int code);

        /// <summary>
        /// Determines if a specified character code represents a character typically found in an alphabetic word.
        /// This method checks whether the character associated with the provided Unicode code is an alphabet letter commonly used in word formation.
        /// </summary>
        /// <param name="code">The character code to check. This should be a non-negative integer representing a Unicode character code.</param>
        /// <returns>True if the character code corresponds to an alphabet letter commonly found in words; otherwise, false.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the 'code' parameter is less than 0, indicating an invalid character code.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the character code evaluation process. The inner exception provides more details about the error.</exception>
        bool IsAlphaWord(int code);

        /// <summary>
        /// Checks if a specified character code corresponds to a numeric digit.
        /// This method evaluates whether the character associated with the provided Unicode code is a number (0-9).
        /// </summary>
        /// <param name="code">The character code to check. This should be a non-negative integer representing a Unicode character code.</param>
        /// <returns>True if the character code represents a numeric digit; otherwise, false.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the 'code' parameter is less than 0, indicating an invalid character code.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the evaluation process. The inner exception provides more details about the error.</exception>
        bool IsNumber(int code);

        /// <summary>
        /// Determines if a specified character code corresponds to a quotation mark.
        /// This method evaluates whether the character associated with the provided Unicode code is a quotation mark, such as single quotes ('), double quotes ("), or other variants used in different languages.
        /// </summary>
        /// <param name="code">The character code to check. This should be a non-negative integer representing a Unicode character code.</param>
        /// <returns>True if the character code represents a quotation mark; otherwise, false.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the 'code' parameter is less than 0, indicating an invalid character code.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the evaluation process. The inner exception provides more details about the error.</exception>
        bool IsQuote(int code);

        /// <summary>
        /// Determines if a specified character code represents a space or whitespace character.
        /// This method checks whether the character associated with the provided Unicode code is a form of whitespace, such as a space, tab, line break, or other similar characters.
        /// </summary>
        /// <param name="code">The character code to check. This should be a non-negative integer representing a Unicode character code.</param>
        /// <returns>True if the character code corresponds to a whitespace character; otherwise, false.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the 'code' parameter is less than 0, indicating an invalid character code.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the evaluation process. The inner exception provides more details about the error.</exception>
        bool IsSpace(int code);

        /// <summary>
        /// Checks whether a specified character code represents a letter with an umlaut.
        /// This method evaluates if the character associated with the provided Unicode code is a letter that includes an umlaut, a diacritical mark used in several European languages.
        /// </summary>
        /// <param name="code">The character code to check. This should be a non-negative integer representing a Unicode character code.</param>
        /// <returns>True if the character code corresponds to a letter with an umlaut; otherwise, false.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the 'code' parameter is less than 0, indicating an invalid character code.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the evaluation process. The inner exception provides more details about the error.</exception>
        bool IsUmlaut(int code);

        /// <summary>
        /// Determines if a specified character code corresponds to a whitespace character.
        /// This method evaluates whether the character associated with the provided Unicode code is a whitespace character, such as spaces, tabs, line breaks, or other similar characters used to create space in text.
        /// </summary>
        /// <param name="code">The character code to check. This should be a non-negative integer representing a Unicode character code.</param>
        /// <returns>True if the character code corresponds to a whitespace character; otherwise, false.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the 'code' parameter is less than 0, indicating an invalid character code.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the evaluation process. The inner exception provides more details about the error.</exception>
        bool IsWhiteSpace(int code);
    }
}
