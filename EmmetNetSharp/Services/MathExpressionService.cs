using EmmetNetSharp.Interfaces;
using Jint;
using Jint.Native;
using System;
using System.IO;

namespace EmmetNetSharp.Services
{
    /// <summary>
    /// Represents the MathExpressionService class.
    /// </summary>
    public class MathExpressionService : IMathExpressionService
    {
        #region Fields

        private readonly Engine _engine;

        #endregion

        #region Ctor

        public MathExpressionService()
        {
            _engine = new Engine();

            var code = File.ReadAllText(Path.Combine(PackageDefaults.ScriptsFolderPath, PackageDefaults.MathExpressionScriptPath));
            _engine.Execute(code ?? string.Empty);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Evaluates a mathematical expression represented as a string and returns the result.
        /// </summary>
        /// <param name="expression">The mathematical expression to be evaluated. This should be a non-null and non-empty string representing a valid mathematical operation.</param>
        /// <returns>The result of the evaluated expression as a double. If the evaluation cannot be performed or the result is undefined or null, it returns null.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the input 'expression' is null, empty, or consists only of white-space characters.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the evaluation of the expression. The inner exception contains more details about the error.</exception>
        public virtual double? Evaluate(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
                throw new ArgumentNullException(nameof(expression));

            try
            {
                var result = _engine.Invoke("evaluate", expression);

                if (result is null || result.IsUndefined() || result.IsNull())
                    return null;

                return result.AsNumber();
            }
            catch (Exception ex)
            {
                throw new Exception("Error evaluating expression.", ex);
            }
        }

        /// <summary>
        /// Extracts a specific substring based on a mathematical expression within the given text. 
        /// The method evaluates the expression to determine the starting and ending positions of the substring.
        /// </summary>
        /// <param name="text">The text from which the substring will be extracted. It should be a non-null and non-empty string.</param>
        /// <param name="position">An optional parameter specifying the position in the text to start evaluating the expression. If null, the evaluation starts from the beginning of the text.</param>
        /// <returns>A tuple containing two integers representing the start and end positions of the extracted substring. Returns null if the extraction cannot be performed, or if the resulting positions are undefined or invalid.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the input 'text' is null, empty, or consists only of white-space characters.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the extraction process. The inner exception contains more details about the error.</exception>
        public virtual (int, int)? Extract(string text, int? position = null)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentNullException(nameof(text));

            try
            {
                JsValue result;

                if (position is null)
                    result = _engine.Invoke("extract", text);
                else
                    result = _engine.Invoke("extract", text, position);

                if (result is null || result.IsUndefined() || result.IsNull())
                    return null;

                var array = result.AsArray();
                if (array is null || array.Length < 2)
                    return null;

                var start = (int)array.Get(0).AsNumber();
                var end = (int)array.Get(1).AsNumber();

                return (start, end);
            }
            catch (Exception ex)
            {
                throw new Exception("Error extracting expression.", ex);
            }
        }

        #endregion
    }
}
