namespace EmmetNetSharp.Tests;

/// <summary>
/// Represents a class that contains tests for lorem ipsum functionalities.
/// </summary>
internal class LoremTests
{
    #region Fields

    private readonly ITestOutputHelper _output;
    
    private readonly IAbbreviationService _abbreviationService;

    #endregion

    #region Ctor

    public LoremTests(ITestOutputHelper output)
    {
        _output = output;
        _abbreviationService = new AbbreviationService();
    }

    #endregion

    #region Methods



    #endregion
}
