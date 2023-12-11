namespace EmmetNetSharp.Tests.MathExpression;

/// <summary>
/// Represents a class that contains tests for extractor math expressions functionality.
/// </summary>
public class ExtractTests
{
    private readonly IMathExpressionService _mathExpressionService = new MathExpressionService();

    [Fact]
    public void Test_Basic()
    {
        Assert.Equal((0, 1), _mathExpressionService.Extract("1"));
        Assert.Equal((0, 2), _mathExpressionService.Extract("10"));
        Assert.Equal((0, 3), _mathExpressionService.Extract("123"));
        Assert.Equal((0, 3), _mathExpressionService.Extract("0.1"));
        Assert.Equal((0, 2), _mathExpressionService.Extract(".1"));
        Assert.Equal((0, 4), _mathExpressionService.Extract(".123"));

        // Mixed content
        Assert.Equal((3, 6), _mathExpressionService.Extract("foo123"));
        Assert.Equal((3, 6), _mathExpressionService.Extract(".1.2.3"));
        Assert.Equal((2, 5), _mathExpressionService.Extract("1.2.3"));
        Assert.Equal((3, 14), _mathExpressionService.Extract("foo2 * (3 + 1)"));
        Assert.Equal((4, 17), _mathExpressionService.Extract("bar.(2 * (3 + 1))"));
        Assert.Equal((6, 9), _mathExpressionService.Extract("test: 1+2"));
    }

    [Fact]
    public void Test_LookAhead()
    {
        Assert.Equal((3, 14), _mathExpressionService.Extract("foo2 * (3 + 1)", 13));
        Assert.Equal((4, 17), _mathExpressionService.Extract("bar.(2 * (3 + 1))", 15));
        Assert.Equal((4, 18), _mathExpressionService.Extract("bar.(2 * (3 + 1) )", 15));
    }
}
