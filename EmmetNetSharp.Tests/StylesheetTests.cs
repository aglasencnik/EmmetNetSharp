namespace EmmetNetSharp.Tests;

/// <summary>
/// Represents a class that contains tests for stylesheet functionalities.
/// </summary>
internal class StylesheetTests
{
    #region Fields

    private readonly ITestOutputHelper _output;

    private readonly IAbbreviationService _abbreviationService;

    #endregion

    #region Ctor

    public StylesheetTests(ITestOutputHelper output)
    {
        _output = output;
        _abbreviationService = new AbbreviationService();
    }

    #endregion

    #region Methods



    #endregion
}
