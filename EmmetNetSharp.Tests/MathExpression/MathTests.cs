namespace EmmetNetSharp.Tests.MathExpression;

/// <summary>
/// Represents a class that contains tests for math expressions functionality.
/// </summary>
public class MathTests
{
    private readonly IMathExpressionService _mathExpressionService = new MathExpressionService();

    [Fact]
    public void Test_EvalBasicMath()
    {
        Assert.Equal(3, _mathExpressionService.Evaluate("1+2"));
        Assert.Equal(3, _mathExpressionService.Evaluate("1 + 2"));
        Assert.Equal(6, _mathExpressionService.Evaluate("2 * 3"));
        Assert.Equal(7, _mathExpressionService.Evaluate("2 * 3 + 1"));
        Assert.Equal(-5, _mathExpressionService.Evaluate("-2 * 3 + 1"));
        Assert.Equal(-5, _mathExpressionService.Evaluate("2 * -3 + 1"));
        Assert.Equal(2.5, _mathExpressionService.Evaluate("5 / 2"));
        Assert.Equal(2, _mathExpressionService.Evaluate("5 \\ 2"));
    }

    [Fact]
    public void Test_EvalParenthesesMath()
    {
        Assert.Equal(8, _mathExpressionService.Evaluate("2 * (3 + 1)"));
        Assert.Equal(18, _mathExpressionService.Evaluate("(3 * (1+2)) * 2"));
        Assert.Equal(-9, _mathExpressionService.Evaluate("3 * -(1 + 2)"));
        Assert.Equal(9, _mathExpressionService.Evaluate("(1 + 2) * 3"));
    }

    [Fact]
    public void Test_ParseErrors()
    {
        Assert.ThrowsAny<Exception>(() => _mathExpressionService.Evaluate(""));
        Assert.ThrowsAny<Exception>(() => _mathExpressionService.Evaluate("a+b"));
        Assert.ThrowsAny<Exception>(() => _mathExpressionService.Evaluate("1/b"));
        Assert.ThrowsAny<Exception>(() => _mathExpressionService.Evaluate("(1 + 3"));
    }
}
