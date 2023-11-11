namespace EmmetNetSharp.Tests;

/// <summary>
/// Represents a class that contains tests for expander functionalities.
/// </summary>
internal class ExpandTests
{
    #region Fields

    private readonly ITestOutputHelper _output;

    private readonly IAbbreviationService _abbreviationService;

    #endregion

    #region Ctor

    public ExpandTests(ITestOutputHelper output)
    {
        _output = output;
        _abbreviationService = new AbbreviationService();
    }

    #endregion

    #region Methods



    #endregion
}
