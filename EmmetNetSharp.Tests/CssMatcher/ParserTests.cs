namespace EmmetNetSharp.Tests.CssMatcher;

/// <summary>
/// Represents a class that contains tests for parser css matcher functionality.
/// </summary>
public class ParserTests
{
    private readonly ICssMatcherService _cssMatcherService = new CssMatcherService();

    internal string[] Tokens(string value)
    {
        var splitRanges = _cssMatcherService.SplitValue(value);
        var tokens = new List<string>();

        foreach (var range in splitRanges)
        {
            int start = range.Item1;
            int length = range.Item2 - start;
            tokens.Add(value.Substring(start, length));
        }

        return tokens.ToArray();
    }

    [Fact]
    public void Test_SplitValue()
    {
        Assert.Equal(new[] { "10px", "20px" }, Tokens("10px 20px"));
        Assert.Equal(new[] { "10px", "20px" }, Tokens(" 10px   20px  "));
        Assert.Equal(new[] { "10px", "20px" }, Tokens("10px, 20px"));
        Assert.Equal(new[] { "20px" }, Tokens("20px"));
        Assert.Equal(new[] { "no-repeat", "10px", "5" }, Tokens("no-repeat, 10px - 5"));
        Assert.Equal(new[] { "url(\"foo bar\")", "no-repeat" }, Tokens("url(\"foo bar\") no-repeat"));
        Assert.Equal(new[] { "--my-prop" }, Tokens("--my-prop"));
        Assert.Equal(new[] { "calc(100% - 80px)" }, Tokens("calc(100% - 80px)"));
    }
}
