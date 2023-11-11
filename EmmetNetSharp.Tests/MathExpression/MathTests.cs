namespace EmmetNetSharp.Tests.MathExpression;

/// <summary>
/// Represents a class that contains tests for math expressions functionality.
/// </summary>
internal class MathTests
{
    #region Fields

    private readonly ITestOutputHelper _output;

    private readonly IMathExpressionService _mathExpressionService;

    #endregion

    #region Ctor

    public MathTests(ITestOutputHelper output)
    {
        _output = output;
        _mathExpressionService = new MathExpressionService();
    }

    #endregion

    #region Methods



    #endregion
}
