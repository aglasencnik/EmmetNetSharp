namespace EmmetNetSharp.Tests;

/// <summary>
/// Represents a class that contains tests for formatter functionalities.
/// </summary>
internal class FormatTests
{
    #region Fields

    private readonly ITestOutputHelper _output;

    private readonly IAbbreviationService _abbreviationService;

    #endregion

    #region Ctor

    public FormatTests(ITestOutputHelper output)
    {
        _output = output;
        _abbreviationService = new AbbreviationService();
    }

    #endregion

    #region Methods



    #endregion
}
