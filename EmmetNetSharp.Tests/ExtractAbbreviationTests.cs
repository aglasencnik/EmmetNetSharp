namespace EmmetNetSharp.Tests;

/// <summary>
/// Represents a class that contains tests for extract abbreviation functionalities.
/// </summary>
internal class ExtractAbbreviationTests
{
    #region Fields

    private readonly ITestOutputHelper _output;

    private readonly IAbbreviationService _abbreviationService;

    #endregion

    #region Ctor

    public ExtractAbbreviationTests(ITestOutputHelper output)
    {
        _output = output;
        _abbreviationService = new AbbreviationService();
    }

    #endregion

    #region Methods



    #endregion
}
