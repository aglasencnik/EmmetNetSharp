namespace EmmetNetSharp.Tests.ActionUtils;

/// <summary>
/// Represents a class that contains tests for HTML action utils functionality.
/// </summary>
internal class HtmlTests
{
    #region Fields

    private readonly ITestOutputHelper _output;

    private readonly IActionUtilsService _actionUtilsService;

    #endregion

    #region Ctor

    public HtmlTests(ITestOutputHelper output)
    {
        _output = output;
        _actionUtilsService = new ActionUtilsService();
    }

    #endregion

    #region Methods



    #endregion
}
