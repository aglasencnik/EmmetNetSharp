using EmmetNetSharp.Enums;
using EmmetNetSharp.Helpers;
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
    /// Represents the HtmlMatcherService class.
    /// </summary>
    public class HtmlMatcherService : IHtmlMatcherService
    {
        #region Fields

        private readonly Engine _engine;

        #endregion

        #region Ctor

        public HtmlMatcherService()
        {
            _engine = new Engine();

            var code = File.ReadAllText(Path.Combine(PackageDefaults.ScriptsFolderPath, PackageDefaults.HtmlMatcherScriptPath));
            _engine.Execute(code ?? string.Empty);
        }

        #endregion

        #region Methods

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
        public virtual HtmlMatchedTag Match(string source, int position, HtmlMatcherScannerOptions scannerOptions = null)
        {
            if (string.IsNullOrWhiteSpace(source))
                throw new ArgumentNullException(nameof(source));

            if (position < 0)
                throw new ArgumentOutOfRangeException(nameof(position));

            try
            {
                JsValue result;

                var partialOptions = PartialHelper.GetPartialObject(scannerOptions);

                if (partialOptions == null)
                    result = _engine.Invoke("match", source, position);
                else
                    result = _engine.Invoke("match", source, position, partialOptions);

                if (result is null || result.IsUndefined() || result.IsNull())
                    return null;

                var properties = result.AsObject().GetOwnProperties();

                var attributeTokens = new List<HtmlAttributeToken>();
                var attributes = properties.FirstOrDefault(p => p.Key == "attributes").Value?.Value.AsArray();
                if (attributes != null && attributes.Length > 0)
                {
                    foreach (var attribute in attributes)
                    {
                        attributeTokens.Add(new HtmlAttributeToken
                        {
                            Name = attribute.AsObject().Get("name").AsString(),
                            Value = attribute.AsObject().Get("value")?.AsString(),
                            NameStart = (int)attribute.AsObject().Get("nameStart").AsNumber(),
                            NameEnd = (int)attribute.AsObject().Get("nameEnd").AsNumber(),
                            ValueStart = (int)attribute.AsObject().Get("valueStart")?.AsNumber(),
                            ValueEnd = (int)attribute.AsObject().Get("valueEnd")?.AsNumber()
                        });
                    }
                }

                var openTagProperty = properties.FirstOrDefault(p => p.Key == "open").Value.Value.AsArray();
                var openingTagRange = (openTagProperty != null && openTagProperty.Length == 2) 
                    ? ((int)openTagProperty[0].AsNumber(), (int)openTagProperty[1].AsNumber()) : (0, 0);

                var closingTagProperty = properties.FirstOrDefault(p => p.Key == "close").Value?.Value?.AsArray();
                (int, int)? closingTagRange = null;

                if (closingTagProperty != null && closingTagProperty.Length == 2)
                    closingTagRange = ((int)closingTagProperty[0].AsNumber(), (int)closingTagProperty[1].AsNumber());

                return new HtmlMatchedTag
                {
                    Name = properties.FirstOrDefault(p => p.Key == "name").Value.Value.ToString(),
                    Attributes = (attributeTokens.Count > 0) ? attributeTokens.ToArray() : null,
                    OpeningTagRange = openingTagRange,
                    ClosingTagRange = closingTagRange
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error matching HTML.", ex);
            }
        }

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
        public virtual HtmlBalancedTag[] BalanceInward(string source, int position, HtmlMatcherScannerOptions scannerOptions = null)
        {
            if (string.IsNullOrWhiteSpace(source))
                throw new ArgumentNullException(nameof(source));

            if (position < 0)
                throw new ArgumentOutOfRangeException(nameof(position));

            try
            {
                JsValue result;

                var partialOptions = PartialHelper.GetPartialObject(scannerOptions);

                if (partialOptions == null)
                    result = _engine.Invoke("balancedInward", source, position);
                else
                    result = _engine.Invoke("balancedInward", source, position, partialOptions);

                if (result is null || result.IsUndefined() || result.IsNull())
                    return null;

                var balancedTags = new List<HtmlBalancedTag>();

                foreach (var resultObject in result.AsArray())
                {
                    var properties = resultObject.AsObject().GetOwnProperties();

                    var openTagProperty = properties.FirstOrDefault(p => p.Key == "open").Value.Value.AsArray();
                    var openingTagRange = (openTagProperty != null && openTagProperty.Length == 2)
                        ? ((int)openTagProperty[0].AsNumber(), (int)openTagProperty[1].AsNumber()) : (0, 0);

                    var closingTagProperty = properties.FirstOrDefault(p => p.Key == "close").Value?.Value?.AsArray();
                    (int, int)? closingTagRange = null;

                    if (closingTagProperty != null && closingTagProperty.Length == 2)
                        closingTagRange = ((int)closingTagProperty[0].AsNumber(), (int)closingTagProperty[1].AsNumber());

                    balancedTags.Add(new HtmlBalancedTag
                    {
                        Name = properties.FirstOrDefault(x => x.Key == "name").Value.Value.AsString(),
                        OpeningTagRange = openingTagRange,
                        ClosingTagRange = closingTagRange
                    });
                }

                return balancedTags.ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception("Error balancing HTML.", ex);
            }
        }

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
        public virtual HtmlBalancedTag[] BalanceOutward(string source, int position, HtmlMatcherScannerOptions scannerOptions = null)
        {
            if (string.IsNullOrWhiteSpace(source))
                throw new ArgumentNullException(nameof(source));

            if (position < 0)
                throw new ArgumentOutOfRangeException(nameof(position));

            try
            {
                JsValue result;

                var partialOptions = PartialHelper.GetPartialObject(scannerOptions);

                if (partialOptions == null)
                    result = _engine.Invoke("balancedOutward", source, position);
                else
                    result = _engine.Invoke("balancedOutward", source, position, partialOptions);

                if (result is null || result.IsUndefined() || result.IsNull())
                    return null;

                var balancedTags = new List<HtmlBalancedTag>();

                foreach (var resultObject in result.AsArray())
                {
                    var properties = resultObject.AsObject().GetOwnProperties();

                    var openTagProperty = properties.FirstOrDefault(p => p.Key == "open").Value.Value.AsArray();
                    var openingTagRange = (openTagProperty != null && openTagProperty.Length == 2)
                        ? ((int)openTagProperty[0].AsNumber(), (int)openTagProperty[1].AsNumber()) : (0, 0);

                    var closingTagProperty = properties.FirstOrDefault(p => p.Key == "close").Value?.Value?.AsArray();
                    (int, int)? closingTagRange = null;

                    if (closingTagProperty != null && closingTagProperty.Length == 2)
                        closingTagRange = ((int)closingTagProperty[0].AsNumber(), (int)closingTagProperty[1].AsNumber());

                    balancedTags.Add(new HtmlBalancedTag
                    {
                        Name = properties.FirstOrDefault(x => x.Key == "name").Value.Value.AsString(),
                        OpeningTagRange = openingTagRange,
                        ClosingTagRange = closingTagRange
                    });
                }

                return balancedTags.ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception("Error balancing HTML.", ex);
            }
        }

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
        public virtual HtmlAttributeToken[] ParseAttributes(string source, string name = null)
        {
            if (string.IsNullOrWhiteSpace(source))
                throw new ArgumentNullException(nameof(source));

            try
            {
                JsValue result;

                if (string.IsNullOrWhiteSpace(name))
                    result = _engine.Invoke("attributes", source);
                else
                    result = _engine.Invoke("attributes", source, name);

                if (result is null || result.IsUndefined() || result.IsNull())
                    return null;

                var attributeTokens = new List<HtmlAttributeToken>();

                foreach (var resultObject in result.AsArray())
                {
                    var properties = resultObject.AsObject().GetOwnProperties();

                    int? valueStart = null;
                    int? valueEnd = null;

                    var valueStartProperty = properties.FirstOrDefault(x => x.Key == "valueStart").Value;
                    var valueEndProperty = properties.FirstOrDefault(x => x.Key == "valueEnd").Value;

                    if (valueStartProperty != null)
                        valueStart = (int)valueStartProperty.Value.AsNumber();

                    if (valueEndProperty != null)
                        valueEnd = (int)valueEndProperty.Value.AsNumber();

                    attributeTokens.Add(new HtmlAttributeToken
                    {
                        Name = properties.FirstOrDefault(x => x.Key == "name").Value.Value.AsString(),
                        Value = properties.FirstOrDefault(x => x.Key == "value").Value?.Value?.AsString() ?? null,
                        NameStart = (int)properties.FirstOrDefault(x => x.Key == "nameStart").Value.Value.AsNumber(),
                        NameEnd = (int)properties.FirstOrDefault(x => x.Key == "nameEnd").Value.Value.AsNumber(),
                        ValueStart = valueStart,
                        ValueEnd = valueEnd
                    });
                }

                return attributeTokens.ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting HTML attributes.", ex);
            }
        }

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
        public virtual (string, HtmlScannerElementType, int, int)[] Scan(string source, HtmlMatcherScannerOptions scannerOptions = null)
        {
            if (string.IsNullOrWhiteSpace(source))
                throw new ArgumentNullException(nameof(source));

            var scanResults = new List<(string, HtmlScannerElementType, int, int)>();

            Action<JsValue, JsValue, JsValue, JsValue> scanCallback = (name, type, start, end) =>
            {
                var nameStr = name.ToString();
                var typeEnum = (HtmlScannerElementType)((int)type.AsNumber());
                var startInt = (int)start.AsNumber();
                var endInt = (int)end.AsNumber();

                scanResults.Add((nameStr, typeEnum, startInt, endInt));
            };

            try
            {
                var partialOptions = PartialHelper.GetPartialObject(scannerOptions);

                if (partialOptions == null)
                    _engine.Invoke("scan", source, scanCallback);
                else
                    _engine.Invoke("scan", source, scanCallback, partialOptions);

                return scanResults.ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception("Error scanning HTML.", ex);
            }
        }

        #endregion
    }
}
