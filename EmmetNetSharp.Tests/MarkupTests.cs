namespace EmmetNetSharp.Tests;

/// <summary>
/// Represents a class that contains tests for markup functionalities.
/// </summary>
internal class MarkupTests
{
    #region Fields

    private readonly ITestOutputHelper _output;

    private readonly IAbbreviationService _abbreviationService;

    #endregion

    #region Ctor

    public MarkupTests(ITestOutputHelper output)
    {
        _output = output;
        _abbreviationService = new AbbreviationService();
    }

    #endregion

    #region Methods



    #endregion
}
