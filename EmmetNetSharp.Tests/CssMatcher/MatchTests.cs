namespace EmmetNetSharp.Tests.CssMatcher;

/// <summary>
/// Represents a class that contains tests for match css matcher functionality.
/// </summary>
internal class MatchTests
{
    #region Fields

    private readonly ITestOutputHelper _output;

    private readonly ICssMatcherService _cssMatcherService;

    #endregion

    #region Ctor

    public MatchTests(ITestOutputHelper output)
    {
        _output = output;
        _cssMatcherService = new CssMatcherService();
    }

    #endregion

    #region Methods



    #endregion
}
