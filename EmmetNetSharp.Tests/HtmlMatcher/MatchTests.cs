using EmmetNetSharp.Models;
using Newtonsoft.Json;

namespace EmmetNetSharp.Tests.HtmlMatcher;

/// <summary>
/// Represents a class that contains tests for match html matcher functionality.
/// </summary>
public class MatchTests
{
    private readonly IHtmlMatcherService _htmlMatcherService = new HtmlMatcherService();

    private readonly string _html = @"<ul>
   <li><a href="""">text <img src=""foo.png""><link rel=""sample""> <b></b></a></li>
   <li><a href="""">text <b></b></a></li>
</ul>";

    private readonly string _xml = @"<ul>
   <li><a href="""">text
       <img src=""foo.png"">
           <link rel=""sample"" />
       </img>
       <b></b>
   </a></li>
   <li><a href="""">text <b></b></a></li>
</ul>";

    [Fact]
    public void Test_Html()
    {
        var tag1 = _htmlMatcherService.Match(_html, 12);
        Assert.Equal("li", tag1.Name);
        Assert.Null(tag1.Attributes);
        Assert.Equal((9, 13), tag1.OpeningTagRange);
        Assert.Equal((79, 84), tag1.ClosingTagRange);

        // Match `<img>` tag. Since in HTML mode, it should be handled as self-closed
        var tag2 = _htmlMatcherService.Match(_html, 37);
        var tag2AttributesJson = JsonConvert.SerializeObject(tag2.Attributes);
        var tag2ExpectedAttributesJson = JsonConvert.SerializeObject(new[]
        {
            new HtmlAttributeToken
            {
                Name = "src",
                Value = "\"foo.png\"",
                NameStart = 34,
                NameEnd = 37,
                ValueStart = 38,
                ValueEnd = 47
            }
        });
        Assert.Equal("img", tag2.Name);
        Assert.Equal(tag2ExpectedAttributesJson, tag2AttributesJson);
        Assert.Equal((29, 48), tag2.OpeningTagRange);
        Assert.Null(tag2.ClosingTagRange);

        var tag3 = _htmlMatcherService.Match(_html, 116);
        var tag3AttributesJson = JsonConvert.SerializeObject(tag3.Attributes);
        var tag3ExpectedAttributesJson = JsonConvert.SerializeObject(new[]
        {
            new HtmlAttributeToken
            {
                Name = "href",
                Value = "\"\"",
                NameStart = 96,
                NameEnd = 100,
                ValueStart = 101,
                ValueEnd = 103
            }
        });
        Assert.Equal("a", tag3.Name);
        Assert.Equal(tag3ExpectedAttributesJson, tag3AttributesJson);
        Assert.Equal((93, 104), tag3.OpeningTagRange);
        Assert.Equal((116, 120), tag3.ClosingTagRange);
    }

    [Fact]
    public void Test_Xml()
    {
        // Should match <img> tag, since we’re in XML mode, matcher should look for closing `</img>` tag
        var tag1 = _htmlMatcherService.Match(_xml, 42, new HtmlMatcherScannerOptions { Xml = true });
        var tag1AttributesJson = JsonConvert.SerializeObject(tag1.Attributes);
        var tag1ExpectedAttributesJson = JsonConvert.SerializeObject(new[]
        {
            new HtmlAttributeToken
            {
                Name = "src",
                Value = "\"foo.png\"",
                NameStart = 42,
                NameEnd = 45,
                ValueStart = 46,
                ValueEnd = 55
            }
        });
        Assert.Equal("img", tag1.Name);
        Assert.Equal(tag1ExpectedAttributesJson, tag1AttributesJson);
        Assert.Equal((37, 56), tag1.OpeningTagRange);
        Assert.Equal((99, 105), tag1.ClosingTagRange);

        var tag2 = _htmlMatcherService.Match(_xml, 70, new HtmlMatcherScannerOptions { Xml = true });
        var tag2AttributesJson = JsonConvert.SerializeObject(tag2.Attributes);
        var tag2ExpectedAttributesJson = JsonConvert.SerializeObject(new[]
        {
            new HtmlAttributeToken
            {
                Name = "rel",
                Value = "\"sample\"",
                NameStart = 75,
                NameEnd = 78,
                ValueStart = 79,
                ValueEnd = 87
            }
        });
        Assert.Equal("link", tag2.Name);
        Assert.Equal(tag2ExpectedAttributesJson, tag2AttributesJson);
        Assert.Equal((69, 90), tag2.OpeningTagRange);
        Assert.Null(tag2.ClosingTagRange);
    }
}
