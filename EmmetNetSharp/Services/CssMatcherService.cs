using EmmetNetSharp.Enums;
using EmmetNetSharp.Interfaces;
using EmmetNetSharp.Models;
using Jint;
using Jint.Native;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EmmetNetSharp.Services
{
    /// <summary>
    /// Represents the CssMatcherService class.
    /// </summary>
    public class CssMatcherService : ICssMatcherService
    {
        #region Fields

        private readonly Engine _engine;

        #endregion

        #region Ctor

        public CssMatcherService()
        {
            _engine = new Engine();

            var code = File.ReadAllText(Path.Combine(PackageDefaults.ScriptsFolderPath, PackageDefaults.CssMatcherScriptPath));
            _engine.Execute(code ?? string.Empty);
        }

        #endregion

        #region Methods

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
        public virtual CssMatchResult Match(string source, int position)
        {
            if (string.IsNullOrWhiteSpace(source))
                throw new ArgumentNullException(nameof(source));

            if (position < 0)
                throw new ArgumentOutOfRangeException(nameof(position));

            try
            {
                var result = _engine.Invoke("emmetMatch", source, position);

                if (result is null || result.IsUndefined() || result.IsNull())
                    return null;

                var properties = result.AsObject().GetOwnProperties();

                return new CssMatchResult
                {
                    Type = (CssMatchType)Enum.Parse(typeof(CssMatchType), properties.FirstOrDefault(x => x.Key == "type").Value.Value.AsString(), true),
                    Start = (int)properties.FirstOrDefault(x => x.Key == "start").Value.Value.AsNumber(),
                    End = (int)properties.FirstOrDefault(x => x.Key == "end").Value.Value.AsNumber(),
                    BodyStart = (int)properties.FirstOrDefault(x => x.Key == "bodyStart").Value.Value.AsNumber(),
                    BodyEnd = (int)properties.FirstOrDefault(x => x.Key == "bodyEnd").Value.Value.AsNumber()
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error matching CSS.", ex);
            }
        }

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
        public virtual (int, int)[] BalanceInward(string source, int position)
        {
            if (string.IsNullOrWhiteSpace(source))
                throw new ArgumentNullException(nameof(source));

            if (position < 0)
                throw new ArgumentOutOfRangeException(nameof(position));

            try
            {
                var result = _engine.Invoke("emmetBalancedInward", source, position);

                if (result is null || result.IsUndefined() || result.IsNull())
                    return null;

                return result.AsArray()
                    .Select(item => item.AsArray())
                    .Where(innerArray => innerArray.Length == 2)
                    .Select(innerArray => (
                        (int)innerArray[0].AsNumber(),
                        (int)innerArray[1].AsNumber()
                    ))
                    .ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception("Error balancing CSS.", ex);
            }
        }

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
        public virtual (int, int)[] BalanceOutward(string source, int position)
        {
            if (string.IsNullOrWhiteSpace(source))
                throw new ArgumentNullException(nameof(source));

            if (position < 0)
                throw new ArgumentOutOfRangeException(nameof(position));

            try
            {
                var result = _engine.Invoke("emmetBalancedOutward", source, position);

                if (result is null || result.IsUndefined() || result.IsNull())
                    return null;

                return result.AsArray()
                    .Select(item => item.AsArray())
                    .Where(innerArray => innerArray.Length == 2)
                    .Select(innerArray => (
                        (int)innerArray[0].AsNumber(),
                        (int)innerArray[1].AsNumber()
                    ))
                    .ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception("Error balancing CSS.", ex);
            }
        }

        /// <summary>
        /// Scans the provided source string and identifies various segments based on a predefined criterion.
        /// Each identified segment is represented by its type, start and end positions, and an additional delimiter value.
        /// This method is particularly useful for parsing and analyzing structured text like CSS.
        /// </summary>
        /// <param name="source">The source string to be scanned. This should be a non-null and non-empty string.</param>
        /// <returns>An array of tuples, where each tuple contains a string representing the segment type, and three integers representing the start position, end position, and a delimiter value for each identified segment.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the input 'source' is null, empty, or consists only of white-space characters.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the scanning process. The inner exception contains more details about the error.</exception>
        public virtual (string, int, int, int)[] Scan(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
                throw new ArgumentNullException(nameof(source));

            var scanResults = new List<(string, int, int, int)>();

            Action<JsValue, JsValue, JsValue, JsValue> scanCallback = (type, start, end, delimiter) =>
            {
                var typeStr = type.ToString();
                var startInt = (int)start.AsNumber();
                var endInt = (int)end.AsNumber();
                var delimiterInt = (int)delimiter.AsNumber();

                scanResults.Add((typeStr, startInt, endInt, delimiterInt));
            };

            try
            {
                _engine.Invoke("emmetScan", source, scanCallback);

                return scanResults.ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception("Error during CSS scanning.", ex);
            }
        }

        /// <summary>
        /// Splits the given string value into pairs of integers based on a specific criterion defined in the 'splitValue' function.
        /// This method is particularly useful for processing and splitting CSS values or similar formatted strings.
        /// </summary>
        /// <param name="value">The string value to be split. This should be a non-null and non-empty string.</param>
        /// <param name="offset">An optional parameter specifying the offset at which the split operation should start. If null, the split starts from the beginning of the string.</param>
        /// <returns>An array of tuples, where each tuple contains two integers representing a segment of the split string. Returns null if the split cannot be performed, or if the resulting segments are undefined or invalid.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the input 'value' is null, empty, or consists only of white-space characters.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the splitting process. The inner exception contains more details about the error.</exception>
        public virtual (int, int)[] SplitValue(string value, int? offset = null)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value));

            try
            {
                JsValue result;

                if (offset is null)
                    result = _engine.Invoke("emmetSplitValue", value);
                else
                    result = _engine.Invoke("emmetSplitValue", value, offset);

                if (result is null || result.IsUndefined() || result.IsNull())
                    return null;

                return result.AsArray()
                    .Select(item => item.AsArray())
                    .Where(innerArray => innerArray.Length == 2)
                    .Select(innerArray => (
                        (int)innerArray[0].AsNumber(),
                        (int)innerArray[1].AsNumber()
                    ))
                    .ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception("Error splitting CSS value.", ex);
            }
        }

        #endregion
    }
}
