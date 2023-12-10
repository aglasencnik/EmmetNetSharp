using EmmetNetSharp.Enums;
using EmmetNetSharp.Models;
using Newtonsoft.Json;

namespace EmmetNetSharp.Tests.Abbreviation;

/// <summary>
/// Represents a class that contains tests for extract abbreviation functionalities.
/// </summary>
public class ExtractAbbreviationTests
{
    private readonly IAbbreviationService _abbreviationService = new AbbreviationService();

    public static ExtractedAbbreviation GetResult(string abbreviation, int location, int? start = null)
    {
        start ??= location;

        return new ExtractedAbbreviation
        {
            Abbreviation = abbreviation,
            Location = location,
            Start = start.Value,
            End = location + abbreviation.Length
        };
    }

    public ExtractedAbbreviation Extract(string abbreviation, AbbreviationExtractOptions? options = null)
    {
        int? caretPos = abbreviation.IndexOf('|');
        if (caretPos != -1)
            abbreviation = abbreviation.Remove(caretPos.Value, 1);
        else
            caretPos = null;

        return _abbreviationService.ExtractAbbreviation(abbreviation, caretPos, options);
    }

    [Fact]
    public void Test_Basic()
    {
        Assert.Equal(
            JsonConvert.SerializeObject(GetResult(".bar", 0)),
            JsonConvert.SerializeObject(Extract(".bar"))
        );

        Assert.Equal(
            JsonConvert.SerializeObject(GetResult(".bar", 5)),
            JsonConvert.SerializeObject(Extract(".foo .bar"))
        );

        Assert.Equal(
            JsonConvert.SerializeObject(GetResult("@bar", 5)),
            JsonConvert.SerializeObject(Extract(".foo @bar"))
        );

        Assert.Equal(
            JsonConvert.SerializeObject(GetResult("img/", 5)),
            JsonConvert.SerializeObject(Extract(".foo img/"))
        );

        Assert.Equal(
            JsonConvert.SerializeObject(GetResult("div", 5)),
            JsonConvert.SerializeObject(Extract("текстdiv"))
        );

        Assert.Equal(
            JsonConvert.SerializeObject(GetResult("div[foo=\"текст\" bar=текст2]", 4)),
            JsonConvert.SerializeObject(Extract("foo div[foo=\"текст\" bar=текст2]"))
        );

        Assert.Equal(
            JsonConvert.SerializeObject(GetResult("table>(tr.prefix-intro>td*1)+(tr.prefix-pro-con>th*1+td*3)+(tr.prefix-key-specs>th[colspan=2]*1+td[colspan=2]*3)+(tr.prefix-key-find-online>th[colspan=2]*1+td*2)", 0)),
            JsonConvert.SerializeObject(Extract("table>(tr.prefix-intro>td*1)+(tr.prefix-pro-con>th*1+td*3)+(tr.prefix-key-specs>th[colspan=2]*1+td[colspan=2]*3)+(tr.prefix-key-find-online>th[colspan=2]*1+td*2)"))
        );
    }

    [Fact]
    public void Test_AbbreviationWithOperators()
    {
        Assert.Equal(
            JsonConvert.SerializeObject(GetResult("foo+bar.baz", 2)),
            JsonConvert.SerializeObject(Extract("a foo+bar.baz"))
        );

        Assert.Equal(
            JsonConvert.SerializeObject(GetResult("foo>bar+baz*3", 2)),
            JsonConvert.SerializeObject(Extract("a foo>bar+baz*3"))
        );
    }

    [Fact]
    public void Test_AbbreviationWithAttributes()
    {
        Assert.Equal(
            JsonConvert.SerializeObject(GetResult("foo[bar]", 2)),
            JsonConvert.SerializeObject(Extract("a foo[bar|]"))
        );

        Assert.Equal(
            JsonConvert.SerializeObject(GetResult("foo[bar=\"baz\" a b]", 2)),
            JsonConvert.SerializeObject(Extract("a foo[bar=\"baz\" a b]"))
        );

        Assert.Equal(
            JsonConvert.SerializeObject(GetResult("bar[a]", 4)),
            JsonConvert.SerializeObject(Extract("foo bar[a|] baz"))
        );
    }

    [Fact]
    public void Test_Tag()
    {
        Assert.Equal(
            JsonConvert.SerializeObject(GetResult("bar[a b=\"c\"]>baz", 5)),
            JsonConvert.SerializeObject(Extract("<foo>bar[a b=\"c\"]>baz"))
        );

        Assert.Equal(
            JsonConvert.SerializeObject(GetResult("foo>bar", 0)),
            JsonConvert.SerializeObject(Extract("foo>bar"))
        );

        Assert.Equal(
            JsonConvert.SerializeObject(GetResult("bar", 5)),
            JsonConvert.SerializeObject(Extract("<foo>bar"))
        );

        Assert.Equal(
            JsonConvert.SerializeObject(GetResult("bar[a=\"d\" b=\"c\"]>baz", 5)),
            JsonConvert.SerializeObject(Extract("<foo>bar[a=\"d\" b=\"c\"]>baz"))
        );
    }

    [Fact]
    public void Test_StylesheetAbbreviation()
    {
        Assert.Equal(
            JsonConvert.SerializeObject(GetResult("foo{bar}", 0)),
            JsonConvert.SerializeObject(Extract("foo{bar|}"))
        );

        Assert.Equal(
            JsonConvert.SerializeObject(GetResult("bar", 4)),
            JsonConvert.SerializeObject(Extract("foo{bar|}", new AbbreviationExtractOptions { Type = SyntaxType.Stylesheet }))
        );
    }

    [Fact]
    public void Test_PrefixedExtract()
    {
        Assert.Equal(
            JsonConvert.SerializeObject(GetResult("bar[a b=\"c\"]>baz", 5)),
            JsonConvert.SerializeObject(Extract("<foo>bar[a b=\"c\"]>baz"))
        );

        Assert.Equal(
            JsonConvert.SerializeObject(GetResult("foo>bar[a b=\"c\"]>baz", 1, 0)),
            JsonConvert.SerializeObject(Extract("<foo>bar[a b=\"c\"]>baz", new AbbreviationExtractOptions { Prefix = "<" }))
        );

        Assert.Equal(
            JsonConvert.SerializeObject(GetResult("foo>bar[a b=\"<\"]>baz", 1, 0)),
            JsonConvert.SerializeObject(Extract("<foo>bar[a b=\"<\"]>baz", new AbbreviationExtractOptions { Prefix = "<" }))
        );

        Assert.Equal(
            JsonConvert.SerializeObject(GetResult("foo>bar{<}>baz", 1, 0)),
            JsonConvert.SerializeObject(Extract("<foo>bar{<}>baz", new AbbreviationExtractOptions { Prefix = "<" }))
        );

        // Multiple prefix characters
        Assert.Equal(
            JsonConvert.SerializeObject(GetResult("bar[a b=\"c\"]>baz", 6, 3)),
            JsonConvert.SerializeObject(Extract("foo>>>bar[a b=\"c\"]>baz", new AbbreviationExtractOptions { Prefix = ">>>" }))
        );

        // Absent prefix
        Assert.Null(Extract("<foo>bar[a b=\"c\"]>baz", new AbbreviationExtractOptions { Prefix = "&&" }));
    }

    [Fact]
    public void Test_BracketsInsideCurlyBraces()
    {
        Assert.Equal(
            JsonConvert.SerializeObject(GetResult("div{[}+a{}", 4)),
            JsonConvert.SerializeObject(Extract("foo div{[}+a{}"))
        );

        Assert.Null(Extract("div{}}"));

        Assert.Equal(
            JsonConvert.SerializeObject(GetResult("{}", 4)),
            JsonConvert.SerializeObject(Extract("div{{}"))
        );
    }
}
