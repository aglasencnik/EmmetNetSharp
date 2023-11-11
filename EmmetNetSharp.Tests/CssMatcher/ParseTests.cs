namespace EmmetNetSharp.Tests.CssMatcher;

/// <summary>
/// Represents a class that contains tests for parse css matcher functionality.
/// </summary>
internal class ParseTests
{
    #region Fields

    private readonly ITestOutputHelper _output;

    private readonly ICssMatcherService _cssMatcherService;

    #endregion

    #region Ctor

    public ParseTests(ITestOutputHelper output)
    {
        _output = output;
        _cssMatcherService = new CssMatcherService();
    }

    #endregion

    #region Methods



    #endregion
}
