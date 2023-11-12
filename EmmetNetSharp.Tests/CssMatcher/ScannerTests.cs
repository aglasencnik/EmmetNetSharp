namespace EmmetNetSharp.Tests.CssMatcher;

/// <summary>
/// Represents a class that contains tests for scanner css matcher functionality.
/// </summary>
public class ScannerTests
{
    private readonly ICssMatcherService _cssMatcherService = new CssMatcherService();

    internal (string, string, int, int, int)[] Tokenize(string source)
    {
        var scanResults = _cssMatcherService.Scan(source);
        var tokenResults = new List<(string, string, int, int, int)>();

        foreach (var (type, start, end, delimiter) in scanResults)
        {
            var value = source.Substring(start, end - start);
            tokenResults.Add((value, type, start, end, delimiter));
        }

        return tokenResults.ToArray();
    }

    [Fact]
    public void Test_Selectors()
    {
        Assert.Equal(new[]
        {
            ("a", "selector", 0, 1, 2),
            ("}", "blockEnd", 3, 4, 3)
        }, Tokenize("a {}"));

        Assert.Equal(new[]
        {
            ("a", "selector", 0, 1, 2),
            ("foo", "propertyName", 4, 7, 7),
            ("bar", "propertyValue", 9, 12, 12),
            ("}", "blockEnd", 14, 15, 14)
        }, Tokenize("a { foo: bar; }"));

        Assert.Equal(new[]
        {
            ("a", "selector", 0, 1, 2),
            ("b", "selector", 4, 5, 5),
            ("}", "blockEnd", 6, 7, 6),
            ("}", "blockEnd", 8, 9, 8)
        }, Tokenize("a { b{} }"));

        Assert.Equal(new[]
        {
            ("a", "selector", 0, 1, 2),
            ("}", "blockEnd", 5, 6, 5)
        }, Tokenize("a {:;}"));

        Assert.Equal(new[]
        {
            ("a + b.class[attr=\"}\"]", "selector", 0, 21, 22),
            ("}", "blockEnd", 24, 25, 24),
        }, Tokenize("a + b.class[attr=\"}\"] { }"));

        Assert.Equal(new[]
        {
            ("a", "selector", 0, 1, 10),
            ("foo", "propertyName", 12, 15, 15),
            ("bar", "propertyValue", 17, 20, 20),
            ("}", "blockEnd", 22, 23, 22)
        }, Tokenize("a /* b */ { foo: bar; }"));
    }

    [Fact]
    public void Test_Property()
    {
        Assert.Equal(new[]
        {
            ("a", "propertyName", 0, 1, -1)
        }, Tokenize("a"));

        Assert.Equal(new[]
        {
            ("a", "propertyName", 0, 1, 1),
            ("b", "propertyValue", 2, 3, -1)
        }, Tokenize("a:b"));

        Assert.Equal(new[]
        {
            ("a", "propertyName", 0, 1, 1),
            ("b", "propertyValue", 2, 3, 3)
        }, Tokenize("a:b;;"));

        Assert.Equal(new[]
        {
            ("a", "selector", 0, 1, 2),
            ("b", "propertyName", 4, 5, 5),
            ("c", "propertyValue", 7, 8, 8),
            ("d", "propertyName", 10, 11, 11),
            ("e", "propertyValue", 13, 14, 14),
            ("}", "blockEnd", 16, 17, 16)
        }, Tokenize("a { b: c; d: e; }"));

        Assert.Equal(new[]
        {
            ("a", "selector", 0, 1, 2),
            ("foo", "propertyName", 4, 7, 7),
            ("bar \"baz}\"", "propertyValue", 9, 19, 20),
            ("}", "blockEnd", 22, 23, 22)
        }, Tokenize("a { foo: bar \"baz}\" ; }"));

        Assert.Equal(new[]
        {
            ("@media (min-width: 900px)", "selector", 0, 25, 26),
            ("}", "blockEnd", 27, 28, 27)
        }, Tokenize("@media (min-width: 900px) {}"));
    }

    [Fact]
    public void Test_PseudoSelectors()
    {
        Assert.Equal(new[]
        {
            ("a:hover", "selector", 1, 8, 9),
            ("foo", "propertyName", 11, 14, 14),
            ("bar \"baz}\"", "propertyValue", 16, 26, 27),
            ("}", "blockEnd", 29, 30, 29)
        }, Tokenize("\na:hover { foo: bar \"baz}\" ; }"));

        Assert.Equal(new[]
        {
            ("a:hover b[title=\"\"]", "selector", 0, 19, 20),
            ("padding", "propertyName", 22, 29, 29),
            ("10px", "propertyValue", 31, 35, 35),
            ("}", "blockEnd", 37, 38, 37)
        }, Tokenize("a:hover b[title=\"\"] { padding: 10px; }"));

        Assert.Equal(new[]
        {
            ("a::before", "selector", 0, 9, 10),
            ("}", "blockEnd", 11, 12, 11)
        }, Tokenize("a::before {}"));

        Assert.Equal(new[]
        {
            ("a", "selector", 0, 1, 2),
            ("&::before", "selector", 4, 13, 14),
            ("}", "blockEnd", 17, 18, 17),
            ("}", "blockEnd", 19, 20, 19)
        }, Tokenize("a { &::before {  } }"));
    }
}
