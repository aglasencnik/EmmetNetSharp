using EmmetNetSharp.Interfaces;
using EmmetNetSharp.Models;
using Jint;
using Jint.Native;
using System;
using System.IO;
using System.Linq;

namespace EmmetNetSharp.Services
{
    /// <summary>
    /// Represents the AbbreviationService class.
    /// </summary>
    public class AbbreviationService : IAbbreviationService
    {
        #region Fields

        private readonly Engine _engine;

        #endregion

        #region Ctor

        public AbbreviationService()
        {
            _engine = new Engine();

            var code = File.ReadAllText(Path.Combine(PackageDefaults.ScriptsFolderPath, PackageDefaults.AbbreviationScriptPath));
            _engine.Execute(code ?? string.Empty);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Expands a given abbreviation into its full form based on the provided user configuration.
        /// </summary>
        /// <param name="abbreviation">The abbreviation to be expanded. Cannot be null or whitespace.</param>
        /// <param name="config">Optional. The user configuration to use in the expansion process. If null, default settings are used.</param>
        /// <returns>
        /// The expanded form of the abbreviation as a string, or null if the expansion process fails or no expansion is found.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when the 'abbreviation' parameter is null or whitespace.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the abbreviation expansion process.</exception>
        public virtual string ExpandAbbreviation(string abbreviation, UserConfig config = null)
        {
            if (string.IsNullOrWhiteSpace(abbreviation))
                throw new ArgumentNullException(nameof(abbreviation));

            try
            {
                JsValue result;

                if (config != null)
                    result = _engine.Invoke("emmetExpandAbbreviation", abbreviation, config.ToJavaScriptObject());
                else
                    result = _engine.Invoke("emmetExpandAbbreviation", abbreviation);

                if (result is null || result.IsUndefined() || result.IsNull())
                    return null;

                return result.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Error expanding abbreviation.", ex);
            }
        }

        /// <summary>
        /// Extracts an abbreviation from a given line of text based on specified position and options.
        /// </summary>
        /// <param name="line">The line of text from which to extract the abbreviation. Cannot be null or whitespace.</param>
        /// <param name="position">Optional. The position in the line to start the extraction. If null, the entire line is considered.</param>
        /// <param name="options">Optional. Additional options influencing how the abbreviation is extracted.</param>
        /// <returns>
        /// An ExtractedAbbreviation object containing details of the extracted abbreviation,
        /// or null if no valid abbreviation is found.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when the 'line' parameter is null or whitespace.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the extraction process.</exception>
        public virtual ExtractedAbbreviation ExtractAbbreviation(string line, int? position = null, AbbreviationExtractOptions options = null)
        {
            if (string.IsNullOrWhiteSpace(line))
                throw new ArgumentNullException(nameof(line));

            try
            {
                JsValue result;

                if (position != null && options != null)
                    result = _engine.Invoke("emmetExtractAbbreviation", line, position, options.ToJavaScriptObject());
                else if (position != null)
                    result = _engine.Invoke("emmetExtractAbbreviation", line, position);
                else if (options != null)
                    result = _engine.Invoke("emmetExtractAbbreviation", line, JsValue.Null, options.ToJavaScriptObject());
                else
                    result = _engine.Invoke("emmetExtractAbbreviation", line);

                if (result is null || result.IsUndefined() || result.IsNull())
                    return null;

                var properties = result.AsObject().GetOwnProperties();

                return new ExtractedAbbreviation
                {
                    Abbreviation = properties.FirstOrDefault(x => x.Key == "abbreviation").Value.Value.ToString(),
                    Location = (int)properties.FirstOrDefault(x => x.Key == "location").Value.Value.AsNumber(),
                    Start = (int)properties.FirstOrDefault(x => x.Key == "start").Value.Value.AsNumber(),
                    End = (int)properties.FirstOrDefault(x => x.Key == "end").Value.Value.AsNumber()
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error extracting abbreviation.", ex);
            }
        }

        /// <summary>
        /// Expands a given markup abbreviation into its full form based on the provided abbreviation configuration.
        /// </summary>
        /// <param name="abbreviation">The markup abbreviation to be expanded. Cannot be null or whitespace.</param>
        /// <param name="config">Optional. The configuration for abbreviation expansion. If null, default settings are used.</param>
        /// <returns>
        /// The expanded form of the markup abbreviation as a string, or null if the expansion process fails or no expansion is found.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when the 'abbreviation' parameter is null or whitespace.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the markup abbreviation expansion process.</exception>
        public virtual string ExpandMarkupAbbreviation(string abbreviation, AbbreviationConfig config = null)
        {
            if (string.IsNullOrWhiteSpace(abbreviation))
                throw new ArgumentNullException(nameof(abbreviation));

            try
            {
                JsValue result;

                if (config != null)
                    result = _engine.Invoke("emmetExpandMarkup", abbreviation, config.ToJavaScriptObject());
                else
                    result = _engine.Invoke("emmetExpandMarkup", abbreviation);

                if (result is null || result.IsUndefined() || result.IsNull())
                    return null;

                return result.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Error expanding markup abbreviation.", ex);
            }
        }

        /// <summary>
        /// Expands a given stylesheet abbreviation into its full form based on the provided abbreviation configuration.
        /// </summary>
        /// <param name="abbreviation">The stylesheet abbreviation to be expanded. Cannot be null or whitespace.</param>
        /// <param name="config">Optional. The configuration for abbreviation expansion. If null, default settings are used.</param>
        /// <returns>
        /// The expanded form of the stylesheet abbreviation as a string, or null if the expansion process fails or no expansion is found.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when the 'abbreviation' parameter is null or whitespace.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the stylesheet abbreviation expansion process.</exception>
        public virtual string ExpandStylesheetAbbreviation(string abbreviation, AbbreviationConfig config = null)
        {
            if (string.IsNullOrWhiteSpace(abbreviation))
                throw new ArgumentNullException(nameof(abbreviation));

            try
            {
                JsValue result;

                if (config != null)
                    result = _engine.Invoke("emmetExpandStylesheet", abbreviation, config.ToJavaScriptObject());
                else
                    result = _engine.Invoke("emmetExpandStylesheet", abbreviation);

                if (result is null || result.IsUndefined() || result.IsNull())
                    return null;

                return result.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Error expanding stylesheet abbreviation.", ex);
            }
        }

        #endregion
    }
}
