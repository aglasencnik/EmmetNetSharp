using EmmetNetSharp.Models;
using Newtonsoft.Json;

namespace EmmetNetSharp.Tests.HtmlMatcher;

/// <summary>
/// Represents a class that contains tests for attributes html matcher functionality.
/// </summary>
public class AttributesTests
{
    private readonly IHtmlMatcherService _htmlMatcherService = new HtmlMatcherService();

    [Fact]
    public void Test_ParseAttributeString()
    {
        var resultJson = JsonConvert.SerializeObject(_htmlMatcherService.ParseAttributes("foo bar=\"baz\" *ngIf={a == b} a=b "));
        var expectedJson = JsonConvert.SerializeObject(new[]
        {
            new HtmlAttributeToken
            {
                Name = "foo",
                NameStart = 0,
                NameEnd = 3
            },
            new HtmlAttributeToken
            {
                Name = "bar",
                Value = "\"baz\"",
                NameStart = 4,
                NameEnd = 7,
                ValueStart = 8,
                ValueEnd = 13
            },
            new HtmlAttributeToken
            {
                Name = "*ngIf",
                Value = "{a == b}",
                NameStart = 14,
                NameEnd = 19,
                ValueStart = 20,
                ValueEnd = 28
            },
            new HtmlAttributeToken
            {
                Name = "a",
                Value = "b",
                NameStart = 29,
                NameEnd = 30,
                ValueStart = 31,
                ValueEnd = 32
            }
        });

        Assert.Equal(expectedJson, resultJson);
    }
}
