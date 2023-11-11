namespace EmmetNetSharp.Tests.HtmlMatcher;

/// <summary>
/// Represents a class that contains tests for scan html matcher functionality.
/// </summary>
internal class ScanTests
{
    #region Fields

    private readonly ITestOutputHelper _output;

    private readonly IHtmlMatcherService _htmlMatcherService;

    #endregion

    #region Ctor

    public ScanTests(ITestOutputHelper output)
    {
        _output = output;
        _htmlMatcherService = new HtmlMatcherService();
    }

    #endregion

    #region Methods



    #endregion
}
