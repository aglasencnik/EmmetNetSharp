using System.Collections.Generic;
using System.Linq;

namespace EmmetNetSharp.Models
{
    /// <summary>
    /// Represents the abbreviation options class.
    /// </summary>
    public class AbbreviationOptions
    {
        /// <summary>
        /// Gets or sets a list of inline-level elements.
        /// </summary>
        public string[] InlineElements { get; set; }

        /// <summary>
        /// Gets or sets the indentation string for output markup.
        /// </summary>
        public string OutputIndent { get; set; }

        /// <summary>
        /// Gets or sets the base indentation for output markup.
        /// </summary>
        public string OutputBaseIndent { get; set; }

        /// <summary>
        /// Gets or sets the newline string for output markup.
        /// </summary>
        public string OutputNewLine { get; set; }

        /// <summary>
        /// Gets or sets the tag case for output markup.
        /// </summary>
        public string OutputTagCase { get; set; }

        /// <summary>
        /// Gets or sets the attribute case for output markup.
        /// </summary>
        public string OutputAttributeCase { get; set; }

        /// <summary>
        /// Gets or sets the attribute quotes for output markup.
        /// </summary>
        public string OutputAttributeQuotes { get; set; }

        /// <summary>
        /// Gets or sets whether to output boolean attributes in expanded form.
        /// </summary>
        public bool? OutputFormat { get; set; }

        /// <summary>
        /// Gets or sets whether to automatically add inner line breaks in output markup.
        /// </summary>
        public bool? OutputFormatLeafNode { get; set; }

        /// <summary>
        /// Gets or sets the list of tag names that should be skipped in outoutput markup formatting.
        /// </summary>
        public string[] OutputFormatSkip { get; set; }

        /// <summary>
        /// Gets or sets the list of tag names that should be forced to be formatted in output markup.
        /// </summary>
        public string[] OutputFormatForce { get; set; }

        /// <summary>
        /// Gets or sets how many inline sibling elements should force line break in output markup.
        /// </summary>
        public int? OutputInlineBreak { get; set; }

        /// <summary>
        /// Gets or sets whether to produce compact notation of boolean attributes in output markup.
        /// </summary>
        public bool? OutputCompactBoolean { get; set; }

        /// <summary>
        /// Gets or sets a list of boolean attributes that should be expanded in output markup.
        /// </summary>
        public string[] OutputBooleanAttributes { get; set; }

        /// <summary>
        /// Gets or sets whether to reverse attributes in output markup.
        /// </summary>
        public bool? OutputReverseAttributes { get; set; }

        /// <summary>
        /// Gets or sets the self-closing style for output markup.
        /// </summary>
        public string OutputSelfClosingStyle { get; set; }

        /// <summary>
        /// Gets or sets whether to automatically update value of element's href attribute if inserting URL or email.
        /// </summary>
        public bool? MarkupHref { get; set; }

        /// <summary>
        /// Gets or sets the markup attribute name mappings.
        /// </summary>
        public Dictionary<string, string> MarkupAttrributes { get; set; }

        /// <summary>
        /// Gets or sets the markup attribute value prefix mappings.
        /// </summary>
        public Dictionary<string, string> MarkupValuePrefix { get; set; }

        /// <summary>
        /// Gets or sets whether to enable output of comments in output markup.
        /// </summary>
        public bool? CommentEnabled { get; set; }

        /// <summary>
        /// Gets or sets attributes that trigger comment in output markup.
        /// </summary>
        public string[] CommentTrigger { get; set; }

        /// <summary>
        /// Gets or sets the template string for comment in output markup.
        /// </summary>
        public string CommentBefore { get; set; }

        /// <summary>
        /// Gets or sets the template string for comment in output markup.
        /// </summary>
        public string CommentAfter { get; set; }

        /// <summary>
        /// Gets or sets whether to enable output of BEM-like attributes in output markup.
        /// </summary>
        public bool? BemEnabled { get; set; }

        /// <summary>
        /// Gets or sets the string for seperating BEM elements in output markup.
        /// </summary>
        public string BemElement { get; set; }

        /// <summary>
        /// Gets or sets the string for seperating BEM modifiers in output markup.
        /// </summary>
        public string BemModifier { get; set; }

        /// <summary>
        /// Gets or sets whether to enable JSX syntax in output markup.
        /// </summary>
        public bool? JsxEnabled { get; set; }

        /// <summary>
        /// Gets or sets the globally available keywords for stylesheet properties.
        /// </summary>
        public string[] StylesheetKeywords { get; set; }

        /// <summary>
        /// Gets or sets the list of unitless stylesheet properties.
        /// </summary>
        public string[] StylesheetUnitless { get; set; }

        /// <summary>
        /// Gets or sets whether to use short hex color notation in output stylesheet.
        /// </summary>
        public bool? StylesheetShortHex { get; set; }

        /// <summary>
        /// Gets or sets the string for seperating property name and value in output stylesheet.
        /// </summary>
        public string StylesheetBetween { get; set; }

        /// <summary>
        /// Gets or sets the string after property value in output stylesheet.
        /// </summary>
        public string StylesheetAfter { get; set; }

        /// <summary>
        /// Gets or sets the unit suffix for outputing integer values in stylesheet.
        /// </summary>
        public string StylesheetIntUnit { get; set; }

        /// <summary>
        /// Gets or sets the float point for outputing float values in stylesheet.
        /// </summary>
        public string StylesheetFloatUnit { get; set; }

        /// <summary>
        /// Gets or sets the aliases for custom units in abbreviation.
        /// </summary>
        public Dictionary<string, string> StylesheetUnitAliases { get; set; }

        /// <summary>
        /// Gets or sets whether to output abbreviation as JSON object properties.
        /// </summary>
        public bool? StylesheetJson { get; set; }

        /// <summary>
        /// Gets or sets whether to use double quotes for JSON properties in output stylesheet.
        /// </summary>
        public bool? StylesheetJsonDoubleQuotes { get; set; }

        /// <summary>
        /// Gets or sets a float number between 0 and 1 to pick fuzzy search result in stylesheet.
        /// </summary>
        public float? StylesheetFuzzySearchMinScore { get; set; }

        /// <summary>
        /// Converts the object to a JavaScript object.
        /// </summary>
        /// <returns>Dictionary containing the object's properties and values.</returns>
        public Dictionary<string, object> ToJavaScriptObject()
        {
            var properties = new Dictionary<string, object>();

            if (InlineElements != null && InlineElements.Any())
                properties.Add("inlineElements", InlineElements);

            if (OutputIndent != null)
                properties.Add("output.indent", OutputIndent);

            if (OutputBaseIndent != null)
                properties.Add("output.baseIndent", OutputBaseIndent);

            if (OutputNewLine != null)
                properties.Add("output.newline", OutputNewLine);

            if (OutputTagCase != null)
                properties.Add("output.tagCase", OutputTagCase);

            if (OutputAttributeCase != null)
                properties.Add("output.attributeCase", OutputAttributeCase);

            if (OutputAttributeQuotes != null)
                properties.Add("output.attributeQuotes", OutputAttributeQuotes);

            if (OutputFormat.HasValue)
                properties.Add("output.format", OutputFormat.Value);

            if (OutputFormatLeafNode.HasValue)
                properties.Add("output.formatLeafNode", OutputFormatLeafNode.Value);

            if (OutputFormatSkip != null && OutputFormatSkip.Any())
                properties.Add("output.formatSkip", OutputFormatSkip);

            if (OutputFormatForce != null && OutputFormatForce.Any())
                properties.Add("output.formatForce", OutputFormatForce);

            if (OutputInlineBreak.HasValue)
                properties.Add("output.inlineBreak", OutputInlineBreak.Value);

            if (OutputCompactBoolean.HasValue)
                properties.Add("output.compactBoolean", OutputCompactBoolean.Value);

            if (OutputBooleanAttributes != null && OutputBooleanAttributes.Any())
                properties.Add("output.booleanAttributes", OutputBooleanAttributes);

            if (OutputReverseAttributes.HasValue)
                properties.Add("output.reverseAttributes", OutputReverseAttributes.Value);

            if (OutputSelfClosingStyle != null)
                properties.Add("output.selfClosingStyle", OutputSelfClosingStyle);

            if (MarkupHref.HasValue)
                properties.Add("markup.href", MarkupHref.Value);

            if (MarkupAttrributes != null && MarkupAttrributes.Any())
                properties.Add("markup.attributes", MarkupAttrributes);

            if (MarkupValuePrefix != null && MarkupValuePrefix.Any())
                properties.Add("markup.valuePrefix", MarkupValuePrefix);

            if (CommentEnabled.HasValue)
                properties.Add("comment.enabled", CommentEnabled.Value);

            if (CommentTrigger != null && CommentTrigger.Any())
                properties.Add("comment.trigger", CommentTrigger);

            if (CommentBefore != null)
                properties.Add("comment.before", CommentBefore);

            if (CommentAfter != null)
                properties.Add("comment.after", CommentAfter);

            if (BemEnabled.HasValue)
                properties.Add("bem.enabled", BemEnabled.Value);

            if (BemElement != null)
                properties.Add("bem.element", BemElement);

            if (BemModifier != null)
                properties.Add("bem.modifier", BemModifier);

            if (JsxEnabled.HasValue)
                properties.Add("jsx.enabled", JsxEnabled.Value);

            if (StylesheetKeywords != null && StylesheetKeywords.Any())
                properties.Add("stylesheet.keywords", StylesheetKeywords);

            if (StylesheetUnitless != null && StylesheetUnitless.Any())
                properties.Add("stylesheet.unitless", StylesheetUnitless);

            if (StylesheetShortHex.HasValue)
                properties.Add("stylesheet.shortHex", StylesheetShortHex);

            if (StylesheetBetween != null)
                properties.Add("stylesheet.between", StylesheetBetween);

            if (StylesheetAfter != null)
                properties.Add("stylesheet.after", StylesheetAfter);

            if (StylesheetIntUnit != null)
                properties.Add("stylesheet.intUnit", StylesheetIntUnit);

            if (StylesheetFloatUnit != null)
                properties.Add("stylesheet.floatUnit", StylesheetFloatUnit);

            if (StylesheetUnitAliases != null && StylesheetUnitAliases.Any())
                properties.Add("stylesheet.unitAliases", StylesheetUnitAliases);

            if (StylesheetJson.HasValue)
                properties.Add("stylesheet.json", StylesheetJson.Value);

            if (StylesheetJsonDoubleQuotes.HasValue)
                properties.Add("stylesheet.jsonDoubleQuotes", StylesheetJsonDoubleQuotes.Value);

            if (StylesheetFuzzySearchMinScore.HasValue)
                properties.Add("stylesheet.fuzzySearchMinScore", StylesheetFuzzySearchMinScore.Value);

            return properties;
        }
    }
}
