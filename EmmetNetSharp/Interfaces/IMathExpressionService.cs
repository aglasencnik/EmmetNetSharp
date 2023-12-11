namespace EmmetNetSharp.Interfaces
{
    /// <summary>
    /// Interface for the MathExpressionService.
    /// </summary>
    public interface IMathExpressionService
    {
        /// <summary>
        /// Evaluates a mathematical expression represented as a string and returns the result.
        /// </summary>
        /// <param name="expression">The mathematical expression to be evaluated. This should be a non-null and non-empty string representing a valid mathematical operation.</param>
        /// <returns>The result of the evaluated expression as a double. If the evaluation cannot be performed or the result is undefined or null, it returns null.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the input 'expression' is null, empty, or consists only of white-space characters.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the evaluation of the expression. The inner exception contains more details about the error.</exception>
        double? Evaluate(string expression);

        /// <summary>
        /// Extracts a specific substring based on a mathematical expression within the given text. 
        /// The method evaluates the expression to determine the starting and ending positions of the substring.
        /// </summary>
        /// <param name="text">The text from which the substring will be extracted. It should be a non-null and non-empty string.</param>
        /// <param name="position">An optional parameter specifying the position in the text to start evaluating the expression. If null, the evaluation starts from the beginning of the text.</param>
        /// <returns>A tuple containing two integers representing the start and end positions of the extracted substring. Returns null if the extraction cannot be performed, or if the resulting positions are undefined or invalid.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the input 'text' is null, empty, or consists only of white-space characters.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the extraction process. The inner exception contains more details about the error.</exception>
        (int, int)? Extract(string text, int? position = null);
    }
}
