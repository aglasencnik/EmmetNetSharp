namespace EmmetNetSharp.Tests.CssMatcher;

/// <summary>
/// Represents a class that contains tests for scan css matcher functionality.
/// </summary>
internal class ScanTests
{
    #region Fields

    private readonly ITestOutputHelper _output;

    private readonly ICssMatcherService _cssMatcherService;

    #endregion

    #region Ctor

    public ScanTests(ITestOutputHelper output)
    {
        _output = output;
        _cssMatcherService = new CssMatcherService();
    }

    #endregion

    #region Methods



    #endregion
}
