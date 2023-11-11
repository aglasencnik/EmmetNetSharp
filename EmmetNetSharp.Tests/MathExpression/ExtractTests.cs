namespace EmmetNetSharp.Tests.MathExpression;

/// <summary>
/// Represents a class that contains tests for extractor math expressions functionality.
/// </summary>
internal class ExtractTests
{
    #region Fields

    private readonly ITestOutputHelper _output;

    private readonly IMathExpressionService _mathExpressionService;

    #endregion

    #region Ctor

    public ExtractTests(ITestOutputHelper output)
    {
        _output = output;
        _mathExpressionService = new MathExpressionService();
    }

    #endregion

    #region Methods



    #endregion
}
