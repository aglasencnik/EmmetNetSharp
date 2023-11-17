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
    /// Represents the ActionUtilsService class.
    /// </summary>
    public class ActionUtilsService : IActionUtilsService
    {
        #region Fields

        private readonly Engine _engine;

        #endregion

        #region Ctor

        public ActionUtilsService()
        {
            _engine = new Engine();

            var code = File.ReadAllText(Path.Combine(PackageDefaults.ScriptsFolderPath, PackageDefaults.ActionUtilsScriptPath));
            _engine.Execute(code ?? string.Empty);
        }

        #endregion

        #region Methods

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
        public virtual ActionUtilsTagMatch FindTagMatch(string source, int position, ActionUtilsScannerOptions options = null)
        {
            if (string.IsNullOrWhiteSpace(source))
                throw new ArgumentNullException(nameof(source));

            if (position < 0)
                throw new ArgumentOutOfRangeException(nameof(position));

            try
            {
                JsValue result;

                if (options != null)
                    result = _engine.Invoke("findTagMatch", source, position, options);
                else
                    result = _engine.Invoke("findTagMatch", source, position);

                if (result is null || result.IsUndefined() || result.IsNull())
                    return null;

                var properties = result.AsObject().GetOwnProperties();

                var openTagProperty = properties.FirstOrDefault(p => p.Key == "open").Value.Value.AsArray();
                var openingTagRange = (openTagProperty != null && openTagProperty.Length == 2)
                    ? ((int)openTagProperty[0].AsNumber(), (int)openTagProperty[1].AsNumber()) : (0, 0);

                var closingTagProperty = properties.FirstOrDefault(p => p.Key == "close").Value?.Value?.AsArray();
                (int, int)? closingTagRange = null;

                if (closingTagProperty != null && closingTagProperty.Length == 2)
                    closingTagRange = ((int)closingTagProperty[0].AsNumber(), (int)closingTagProperty[1].AsNumber());

                return new ActionUtilsTagMatch
                {
                    Name = properties.FirstOrDefault(p => p.Key == "name").Value.Value.AsString(),
                    OpenRange = openingTagRange,
                    CloseRange = closingTagRange
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error finding tag match.", ex);
            }
        }

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
        public virtual ActionUtilsCssSection GetCssSection(string code, int position, bool? includeProperties = null)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentNullException(nameof(code));

            if (position < 0)
                throw new ArgumentOutOfRangeException(nameof(position));

            try
            {
                JsValue result;

                if (includeProperties.HasValue)
                    result = _engine.Invoke("getCSSSection", code, position, includeProperties);
                else
                    result = _engine.Invoke("getCSSSection", code, position);

                if (result is null || result.IsUndefined() || result.IsNull())
                    return null;

                var properties = result.AsObject().GetOwnProperties();

                var cssProperties = new List<CssProperty>();
                var cssPropertiesObjectArray = properties.FirstOrDefault(p => p.Key == "properties").Value?.Value.AsArray();
                if (cssPropertiesObjectArray != null && cssPropertiesObjectArray.Length > 0)
                {
                    foreach (var cssObject in cssPropertiesObjectArray)
                    {
                        var cssObjectProperties = cssObject.AsObject().GetOwnProperties();

                        var nameProperty = cssObjectProperties.FirstOrDefault(p => p.Key == "name").Value.Value.AsArray();
                        var nameRange = (nameProperty != null && nameProperty.Length == 2)
                            ? ((int)nameProperty[0].AsNumber(), (int)nameProperty[1].AsNumber()) : (0, 0);

                        var valueProperty = cssObjectProperties.FirstOrDefault(p => p.Key == "value").Value.Value.AsArray();
                        var valueRange = (valueProperty != null && valueProperty.Length == 2)
                            ? ((int)valueProperty[0].AsNumber(), (int)valueProperty[1].AsNumber()) : (0, 0);

                        var valueTokensRanges = cssObjectProperties.FirstOrDefault(p => p.Key == "valueTokens").Value.Value.AsArray()
                            .Select(item => item.AsArray())
                            .Where(innerArray => innerArray.Length == 2)
                            .Select(innerArray => (
                                (int)innerArray[0].AsNumber(),
                                (int)innerArray[1].AsNumber()
                            ))
                            .ToArray();

                        cssProperties.Add(new CssProperty
                        {
                            Name = nameRange,
                            Value = valueRange,
                            ValueTokens = valueTokensRanges,
                            Before = (int)cssObjectProperties.FirstOrDefault(p => p.Key == "before").Value.Value.AsNumber(),
                            After = (int)cssObjectProperties.FirstOrDefault(p => p.Key == "after").Value.Value.AsNumber()
                        });
                    }
                }

                return new ActionUtilsCssSection
                {
                    Start = (int)properties.FirstOrDefault(p => p.Key == "start").Value.Value.AsNumber(),
                    End = (int)properties.FirstOrDefault(p => p.Key == "end").Value.Value.AsNumber(),
                    BodyStart = (int)properties.FirstOrDefault(p => p.Key == "bodyStart").Value.Value.AsNumber(),
                    BodyEnd = (int)properties.FirstOrDefault(p => p.Key == "bodyEnd").Value.Value.AsNumber(),
                    Properties = (cssProperties.Count > 0) ? cssProperties.ToArray() : null
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting CSS section.", ex);
            }
        }

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
        public virtual ActionUtilsContextTag GetOpenTag(string code, int position)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentNullException(nameof(code));

            if (position < 0)
                throw new ArgumentOutOfRangeException(nameof(position));

            try
            {
                var result = _engine.Invoke("getOpenTag", code, position);

                if (result is null || result.IsUndefined() || result.IsNull())
                    return null;

                var resultProperties = result.AsObject().GetOwnProperties();

                var attributeTokens = new List<HtmlAttributeToken>();
                var attributes = resultProperties.FirstOrDefault(p => p.Key == "attributes").Value?.Value.AsArray();
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

                return new ActionUtilsContextTag
                {
                    Name = resultProperties.FirstOrDefault(p => p.Key == "name").Value.Value.AsString(),
                    Type = (HtmlScannerElementType)((int)resultProperties.FirstOrDefault(p => p.Key == "type").Value.Value.AsNumber()),
                    Start = (int)resultProperties.FirstOrDefault(p => p.Key == "start").Value.Value.AsNumber(),
                    End = (int)resultProperties.FirstOrDefault(p => p.Key == "end").Value.Value.AsNumber(),
                    Attributes = (attributeTokens.Count > 0) ? attributeTokens.ToArray() : null,
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting open tag.", ex);
            }
        }

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
        public virtual ActionUtilsTagMatch[] GetTagMatches(string code, ActionUtilsScannerOptions options = null)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentNullException(nameof(code));

            try
            {
                JsValue result;

                if (options != null)
                    result = _engine.Invoke("getTagMatches", code, options);
                else
                    result = _engine.Invoke("getTagMatches", code);

                if (result is null || result.IsUndefined() || result.IsNull())
                    return null;

                var tagMatches = new List<ActionUtilsTagMatch>();

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

                    tagMatches.Add(new ActionUtilsTagMatch
                    {
                        Name = properties.FirstOrDefault(p => p.Key == "name").Value.Value.AsString(),
                        OpenRange = openingTagRange,
                        CloseRange = closingTagRange
                    });
                }

                return tagMatches.ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting tag matches.", ex);
            }
        }

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
        public virtual ActionUtilsSelectItemModel SelectItemCss(string code, int position, bool? isPrev = null)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentNullException(nameof(code));

            if (position < 0)
                throw new ArgumentOutOfRangeException(nameof(position));

            try
            {
                JsValue result;

                if (isPrev.HasValue)
                    result = _engine.Invoke("selectItemCSS", code, position, isPrev.Value);
                else
                    result = _engine.Invoke("selectItemCSS", code, position);

                if (result is null || result.IsUndefined() || result.IsNull())
                    return null;

                var properties = result.AsObject().GetOwnProperties();

                var ranges = properties.FirstOrDefault(p => p.Key == "ranges").Value.Value.AsArray()
                    .Select(item => item.AsArray())
                    .Where(innerArray => innerArray.Length == 2)
                    .Select(innerArray => (
                        (int)innerArray[0].AsNumber(),
                        (int)innerArray[1].AsNumber()
                    ))
                    .ToArray();

                return new ActionUtilsSelectItemModel
                {
                    Start = (int)properties.FirstOrDefault(p => p.Key == "start").Value.Value.AsNumber(),
                    End = (int)properties.FirstOrDefault(p => p.Key == "end").Value.Value.AsNumber(),
                    Ranges = ranges
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error selecting CSS item.", ex);
            }
        }

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
        public virtual ActionUtilsSelectItemModel SelectItemHtml(string code, int position, bool? isPrev = null)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentNullException(nameof(code));

            if (position < 0)
                throw new ArgumentOutOfRangeException(nameof(position));

            try
            {
                JsValue result;

                if (isPrev.HasValue)
                    result = _engine.Invoke("selectItemHTML", code, position, isPrev.Value);
                else
                    result = _engine.Invoke("selectItemHTML", code, position);

                if (result is null || result.IsUndefined() || result.IsNull())
                    return null;

                var properties = result.AsObject().GetOwnProperties();

                var ranges = properties.FirstOrDefault(p => p.Key == "ranges").Value.Value.AsArray()
                    .Select(item => item.AsArray())
                    .Where(innerArray => innerArray.Length == 2)
                    .Select(innerArray => (
                        (int)innerArray[0].AsNumber(),
                        (int)innerArray[1].AsNumber()
                    ))
                    .ToArray();

                return new ActionUtilsSelectItemModel
                {
                    Start = (int)properties.FirstOrDefault(p => p.Key == "start").Value.Value.AsNumber(),
                    End = (int)properties.FirstOrDefault(p => p.Key == "end").Value.Value.AsNumber(),
                    Ranges = ranges
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error selecting HTML item.", ex);
            }
        }

        #endregion
    }
}
