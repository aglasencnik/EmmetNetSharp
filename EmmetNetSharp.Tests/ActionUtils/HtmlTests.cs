using EmmetNetSharp.Enums;
using EmmetNetSharp.Models;
using Newtonsoft.Json;

namespace EmmetNetSharp.Tests.ActionUtils;

/// <summary>
/// Represents a class that contains tests for HTML action utils functionality.
/// </summary>
public class HtmlTests
{
    private readonly IActionUtilsService _actionUtilsService = new ActionUtilsService();

    [Fact]
    public void Test_SelectNextItem()
    {
        var code = File.ReadAllText(Path.Combine("Assets", "actionUtilsSample.html")) ?? string.Empty;

        // `<li class="item item_1">`: select tag name, full attribute, attribute, value and class names
        var case1ActualJson = JsonConvert.SerializeObject(_actionUtilsService.SelectItemHtml(code, 9));
        var case1ExpectedJson = JsonConvert.SerializeObject(new ActionUtilsSelectItemModel
        {
            Start = 9,
            End = 33,
            Ranges = new[]
            {
                (10, 12),
                (13, 32),
                (20, 31),
                (20, 24),
                (25, 31),
            }
        });
        Assert.Equal(case1ExpectedJson, case1ActualJson);

        // <a href="/sample"  title={expr}>
        var case2ActualJson = JsonConvert.SerializeObject(_actionUtilsService.SelectItemHtml(code, 33));
        var case2ExpectedJson = JsonConvert.SerializeObject(new ActionUtilsSelectItemModel
        {
            Start = 42,
            End = 74,
            Ranges = new[]
            {
                (43, 44),
                (45, 59),
                (51, 58),
                (61, 73),
                (68, 72),
            }
        });
        Assert.Equal(case2ExpectedJson, case2ActualJson);
    }

    [Fact]
    public void Test_SelectPreviousItem()
    {
        var code = File.ReadAllText(Path.Combine("Assets", "actionUtilsSample.html")) ?? string.Empty;

        // <a href="/sample"  title={expr}>
        var case1ActualJson = JsonConvert.SerializeObject(_actionUtilsService.SelectItemHtml(code, 80, true));
        var case1ExpectedJson = JsonConvert.SerializeObject(new ActionUtilsSelectItemModel
        {
            Start = 42,
            End = 74,
            Ranges = new[]
            {
                (43, 44),
                (45, 59),
                (51, 58),
                (61, 73),
                (68, 72),
            }
        });
        Assert.Equal(case1ExpectedJson, case1ActualJson);

        // <li class="item item_1">
        var case2ActualJson = JsonConvert.SerializeObject(_actionUtilsService.SelectItemHtml(code, 42, true));
        var case2ExpectedJson = JsonConvert.SerializeObject(new ActionUtilsSelectItemModel
        {
            Start = 9,
            End = 33,
            Ranges = new[]
            {
                (10, 12),
                (13, 32),
                (20, 31),
                (20, 24),
                (25, 31),
            }
        });
        Assert.Equal(case2ExpectedJson, case2ActualJson);
    }

    [Fact]
    public void Test_GetOpenTag()
    {
        var code = File.ReadAllText(Path.Combine("Assets", "actionUtilsSample.html")) ?? string.Empty;

        var case1ActualJson = JsonConvert.SerializeObject(_actionUtilsService.GetOpenTag(code, 60));
        var case1ExpectedJson = JsonConvert.SerializeObject(new ActionUtilsContextTag 
        {
            Name = "a",
            Type = HtmlScannerElementType.Open,
            Start = 42,
            End = 74,
            Attributes = new[]
            {
                new HtmlAttributeToken
                {
                    Name = "href",
                    NameStart = 45,
                    NameEnd = 49,
                    Value = "\"/sample\"",
                    ValueStart = 50,
                    ValueEnd = 59
                },
                new HtmlAttributeToken
                {
                    Name = "title",
                    NameStart = 61,
                    NameEnd = 66,
                    Value = "{expr}",
                    ValueStart = 67,
                    ValueEnd = 73
                }
            }
        });
        Assert.Equal(case1ExpectedJson, case1ActualJson);

        var case2ActualJson = JsonConvert.SerializeObject(_actionUtilsService.GetOpenTag(code, 15));
        var case2ExpectedJson = JsonConvert.SerializeObject(new ActionUtilsContextTag
        {
            Name = "li",
            Type = HtmlScannerElementType.Open,
            Start = 9,
            End = 33,
            Attributes = new[]
            {
                new HtmlAttributeToken
                {
                    Name = "class",
                    NameStart = 13,
                    NameEnd = 18,
                    Value = "\"item item_1\"",
                    ValueStart = 19,
                    ValueEnd = 32
                }
            }
        });
        Assert.Equal(case2ExpectedJson, case2ActualJson);

        Assert.Null(_actionUtilsService.GetOpenTag(code, 74));
    }
}
