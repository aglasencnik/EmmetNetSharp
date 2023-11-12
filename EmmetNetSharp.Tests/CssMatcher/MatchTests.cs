using EmmetNetSharp.Enums;
using EmmetNetSharp.Models;
using Newtonsoft.Json;

namespace EmmetNetSharp.Tests.CssMatcher;

/// <summary>
/// Represents a class that contains tests for match css matcher functionality.
/// </summary>
public class MatchTests
{
    private readonly ICssMatcherService _cssMatcherService = new CssMatcherService();

    [Fact]
    public void Test_MatchSelector()
    {
        var code = File.ReadAllText(Path.Combine("Assets", "cssMatcherSample.scss")) ?? string.Empty;

        var expectedJson1 = JsonConvert.SerializeObject(new CssMatchResult
        {
            Type = CssMatchType.Selector,
            Start = 61,
            End = 198,
            BodyStart = 66,
            BodyEnd = 197
        });

        var expectedJson2 = JsonConvert.SerializeObject(new CssMatchResult
        {
            Type = CssMatchType.Selector,
            Start = 204,
            End = 267,
            BodyStart = 209,
            BodyEnd = 266
        });

        Assert.Equal(expectedJson1, JsonConvert.SerializeObject(_cssMatcherService.Match(code, 63)));
        Assert.Equal(expectedJson2, JsonConvert.SerializeObject(_cssMatcherService.Match(code, 208)));
    }

    [Fact]
    public void Test_Property()
    {
        var code = File.ReadAllText(Path.Combine("Assets", "cssMatcherSample.scss")) ?? string.Empty;

        var expectedJson = JsonConvert.SerializeObject(new CssMatchResult
        {
            Type = CssMatchType.Property,
            Start = 137,
            End = 150,
            BodyStart = 145,
            BodyEnd = 149
        });

        Assert.Equal(expectedJson, JsonConvert.SerializeObject(_cssMatcherService.Match(code, 140)));
    }
}
