using EmmetNetSharp.Enums;

namespace EmmetNetSharp.Tests.HtmlMatcher;

/// <summary>
/// Represents a class that contains tests for scanner html matcher functionality.
/// </summary>
public class ScannerTests
{
    private readonly IHtmlMatcherService _htmlMatcherService = new HtmlMatcherService();

    [Fact]
    public void Test_OpenTag()
    {
        Assert.Equal(new[]
        {
            ("a", HtmlScannerElementType.Open, 0, 3)
        }, _htmlMatcherService.Scan("<a>"));

        Assert.Equal(new[]
        {
            ("a", HtmlScannerElementType.Open, 3, 6)
        }, _htmlMatcherService.Scan("foo<a>"));

        Assert.Equal(new[]
        {
            ("a", HtmlScannerElementType.Open, 0, 3)
        }, _htmlMatcherService.Scan("<a>foo"));

        Assert.Equal(new[]
        {
            ("foo-bar", HtmlScannerElementType.Open, 0, 9)
        }, _htmlMatcherService.Scan("<foo-bar>"));

        Assert.Equal(new[]
        {
            ("foo:bar", HtmlScannerElementType.Open, 0, 9)
        }, _htmlMatcherService.Scan("<foo:bar>"));

        Assert.Equal(new[]
        {
            ("foo_bar", HtmlScannerElementType.Open, 0, 9)
        }, _htmlMatcherService.Scan("<foo_bar>"));

        Assert.Equal(Array.Empty<(string, HtmlScannerElementType, int, int)>(), _htmlMatcherService.Scan("<=>"));
        Assert.Equal(Array.Empty<(string, HtmlScannerElementType, int, int)>(), _htmlMatcherService.Scan("<1>"));

        // Tag with attributes
        Assert.Equal(new[]
        {
            ("a", HtmlScannerElementType.Open, 0, 11)
        }, _htmlMatcherService.Scan("<a href=\"\">"));

        Assert.Equal(new[]
        {
            ("a", HtmlScannerElementType.Open, 0, 11)
        }, _htmlMatcherService.Scan("<a foo bar>"));

        Assert.Equal(new[]
        {
            ("a", HtmlScannerElementType.Open, 0, 12)
        }, _htmlMatcherService.Scan("<a a={test}>"));

        Assert.Equal(new[]
        {
            ("a", HtmlScannerElementType.Open, 0, 19)
        }, _htmlMatcherService.Scan("<a [ng-for]={test}>"));

        Assert.Equal(new[]
        {
            ("a", HtmlScannerElementType.Open, 0, 15)
        }, _htmlMatcherService.Scan("<a a=b c {foo}>"));
    }

    [Fact]
    public void Test_SelfClosingTags()
    {
        Assert.Equal(new[]
        {
            ("a", HtmlScannerElementType.SelfClose, 0, 4)
        }, _htmlMatcherService.Scan("<a/>foo"));

        Assert.Equal(new[]
        {
            ("a", HtmlScannerElementType.SelfClose, 0, 5)
        }, _htmlMatcherService.Scan("<a />foo"));

        Assert.Equal(new[]
        {
            ("a", HtmlScannerElementType.SelfClose, 0, 16)
        }, _htmlMatcherService.Scan("<a a=b c {foo}/>"));
    }

    [Fact]
    public void Test_CloseTag()
    {
        Assert.Equal(new[]
        {
            ("a", HtmlScannerElementType.Close, 3, 7)
        }, _htmlMatcherService.Scan("foo</a>"));

        Assert.Equal(new[]
        {
            ("a", HtmlScannerElementType.Close, 0, 4)
        }, _htmlMatcherService.Scan("</a>foo"));

        Assert.Equal(Array.Empty<(string, HtmlScannerElementType, int, int)>(), _htmlMatcherService.Scan("</a s>"));
        Assert.Equal(Array.Empty<(string, HtmlScannerElementType, int, int)>(), _htmlMatcherService.Scan("</a >"));
    }

    [Fact]
    public void Test_SpecialTags()
    {
        Assert.Equal(new[]
        {
            ("a", HtmlScannerElementType.Open, 0, 3),
            ("a", HtmlScannerElementType.Close, 6, 10),
            ("style", HtmlScannerElementType.Open, 10, 17),
            ("b", HtmlScannerElementType.Open, 17, 20),
            ("style", HtmlScannerElementType.Close, 20, 28),
            ("c", HtmlScannerElementType.Open, 28, 31),
            ("c", HtmlScannerElementType.Close, 34, 38)
        }, _htmlMatcherService.Scan("<a>foo</a><style><b></style><c>bar</c>"));

        Assert.Equal(new[]
        {
            ("script", HtmlScannerElementType.Open, 0, 8),
            ("a", HtmlScannerElementType.Open, 8, 11),
            ("script", HtmlScannerElementType.Close, 11, 20),
            ("script", HtmlScannerElementType.Open, 20, 46),
            ("b", HtmlScannerElementType.Open, 46, 49),
            ("script", HtmlScannerElementType.Close, 49, 58),
            ("script", HtmlScannerElementType.Open, 58, 84),
            ("c", HtmlScannerElementType.Open, 84, 87),
            ("script", HtmlScannerElementType.Close, 87, 96)
        }, _htmlMatcherService.Scan("<script><a></script><script type=\"text/x-foo\"><b></script><script type=\"javascript\"><c></script>"));
    }

    [Fact]
    public void Test_CData()
    {
        Assert.Equal(new[]
        {
            ("a", HtmlScannerElementType.Open, 0, 3),
            ("b", HtmlScannerElementType.Open, 27, 30)
        }, _htmlMatcherService.Scan("<a><![CDATA[<foo /><bar>]]><b>"));

        // Consume unclosed: still a CDATA
        Assert.Equal(new[]
        {
            ("a", HtmlScannerElementType.Open, 0, 3)
        }, _htmlMatcherService.Scan("<a><![CDATA[<foo /><bar><b>"));
    }

    [Fact]
    public void Test_Comments()
    {
        Assert.Equal(new[]
        {
            ("a", HtmlScannerElementType.Open, 0, 3),
            ("b", HtmlScannerElementType.Open, 24, 27)
        }, _htmlMatcherService.Scan("<a><!-- <foo /><bar> --><b>"));

        Assert.Equal(new[]
        {
            ("a", HtmlScannerElementType.Open, 0, 3)
        }, _htmlMatcherService.Scan("<a><!-- <foo /><bar><b>"));
    }
}
