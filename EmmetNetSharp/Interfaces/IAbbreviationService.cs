using EmmetNetSharp.Models;

namespace EmmetNetSharp.Interfaces
{
    /// <summary>
    /// Interface for AbbreviationService.
    /// </summary>
    public interface IAbbreviationService
    {
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
        string ExpandAbbreviation(string abbreviation, UserConfig config = null);

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
        ExtractedAbbreviation ExtractAbbreviation(string line, int? position = null, AbbreviationExtractOptions options = null);
    }
}
