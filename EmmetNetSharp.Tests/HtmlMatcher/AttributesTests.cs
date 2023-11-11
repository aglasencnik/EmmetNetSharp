namespace EmmetNetSharp.Tests.HtmlMatcher;

/// <summary>
/// Represents a class that contains tests for attributes html matcher functionality.
/// </summary>
internal class AttributesTests
{
    #region Fields

    private readonly ITestOutputHelper _output;

    private readonly IHtmlMatcherService _htmlMatcherService;

    #endregion

    #region Ctor

    public AttributesTests(ITestOutputHelper output)
    {
        _output = output;
        _htmlMatcherService = new HtmlMatcherService();
    }

    #endregion

    #region Methods



    #endregion
}
