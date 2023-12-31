using EmmetNetSharp.Models;
using Newtonsoft.Json;

namespace EmmetNetSharp.Tests.HtmlMatcher;

/// <summary>
/// Represents a class that contains tests for balance html matcher functionality.
/// </summary>
public class BalanceTests
{
    private readonly IHtmlMatcherService _htmlMatcherService = new HtmlMatcherService();

    [Fact]
    public void Test_Inward()
    {
        var code = File.ReadAllText(Path.Combine("Assets", "htmlMatcherSample.html")).Replace("\r\n", "\n") ?? string.Empty;

        var case1ActualJson = JsonConvert.SerializeObject(_htmlMatcherService.BalanceInward(code, 0));
        var case1ExpectedJson = JsonConvert.SerializeObject(new[]
        {
            new HtmlBalancedTag
            {
                Name = "ul",
                OpeningTagRange = (0, 4),
                ClosingTagRange = (179, 184),
            },
            new HtmlBalancedTag
            {
                Name = "li",
                OpeningTagRange = (6, 10),
                ClosingTagRange = (25, 30),
            },
            new HtmlBalancedTag
            {
                Name = "a",
                OpeningTagRange = (10, 21),
                ClosingTagRange = (21, 25),
            }
        });
        Assert.Equal(case1ExpectedJson, case1ActualJson);

        var case2ActualJson = JsonConvert.SerializeObject(_htmlMatcherService.BalanceInward(code, 1));
        var case2ExpectedJson = JsonConvert.SerializeObject(new[]
        {
            new HtmlBalancedTag
            {
                Name = "ul",
                OpeningTagRange = (0, 4),
                ClosingTagRange = (179, 184),
            },
            new HtmlBalancedTag
            {
                Name = "li",
                OpeningTagRange = (6, 10),
                ClosingTagRange = (25, 30),
            },
            new HtmlBalancedTag
            {
                Name = "a",
                OpeningTagRange = (10, 21),
                ClosingTagRange = (21, 25),
            }
        });
        Assert.Equal(case2ExpectedJson, case2ActualJson);

        var case3ActualJson = JsonConvert.SerializeObject(_htmlMatcherService.BalanceInward(code, 73));
        var case3ExpectedJson = JsonConvert.SerializeObject(new[]
        {
            new HtmlBalancedTag
            {
                Name = "li",
                OpeningTagRange = (71, 75),
                ClosingTagRange = (147, 152),
            },
            new HtmlBalancedTag
            {
                Name = "div",
                OpeningTagRange = (78, 83),
                ClosingTagRange = (121, 127),
            },
            new HtmlBalancedTag
            {
                Name = "img",
                OpeningTagRange = (87, 108)
            }
        });
        Assert.Equal(case3ExpectedJson, case3ActualJson);

        var case4ActualJson = JsonConvert.SerializeObject(_htmlMatcherService.BalanceInward(code, 114));
        var case4ExpectedJson = JsonConvert.SerializeObject(new[]
        {
            new HtmlBalancedTag
            {
                Name = "br",
                OpeningTagRange = (112, 118)
            }
        });
        Assert.Equal(case4ExpectedJson, case4ActualJson);
    }

    [Fact]
    public void Test_Outward()
    {
        var code = File.ReadAllText(Path.Combine("Assets", "htmlMatcherSample.html")).Replace("\r\n", "\n") ?? string.Empty;

        var case1ActualJson = JsonConvert.SerializeObject(_htmlMatcherService.BalanceOutward(code, 0));
        var case1ExpectedJson = JsonConvert.SerializeObject(Array.Empty<HtmlBalancedTag>());
        Assert.Equal(case1ExpectedJson, case1ActualJson);

        var case2ActualJson = JsonConvert.SerializeObject(_htmlMatcherService.BalanceOutward(code, 1));
        var case2ExpectedJson = JsonConvert.SerializeObject(new[]
        {
            new HtmlBalancedTag
            {
                Name = "ul",
                OpeningTagRange = (0, 4),
                ClosingTagRange = (179, 184),
            }
        });
        Assert.Equal(case2ExpectedJson, case2ActualJson);

        var case3ActualJson = JsonConvert.SerializeObject(_htmlMatcherService.BalanceOutward(code, 73));
        var case3ExpectedJson = JsonConvert.SerializeObject(new[]
        {
            new HtmlBalancedTag
            {
                Name = "li",
                OpeningTagRange = (71, 75),
                ClosingTagRange = (147, 152),
            },
            new HtmlBalancedTag
            {
                Name = "ul",
                OpeningTagRange = (0, 4),
                ClosingTagRange = (179, 184),
            }
        });
        Assert.Equal(case3ExpectedJson, case3ActualJson);

        var case4ActualJson = JsonConvert.SerializeObject(_htmlMatcherService.BalanceOutward(code, 114));
        var case4ExpectedJson = JsonConvert.SerializeObject(new[]
        {
            new HtmlBalancedTag
            {
                Name = "br",
                OpeningTagRange = (112, 118)
            },
            new HtmlBalancedTag
            {
                Name = "div",
                OpeningTagRange = (78, 83),
                ClosingTagRange = (121, 127),
            },
            new HtmlBalancedTag
            {
                Name = "li",
                OpeningTagRange = (71, 75),
                ClosingTagRange = (147, 152),
            },
            new HtmlBalancedTag
            {
                Name = "ul",
                OpeningTagRange = (0, 4),
                ClosingTagRange = (179, 184),
            }
        });
        Assert.Equal(case4ExpectedJson, case4ActualJson);
    }
}
