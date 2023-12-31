using EmmetNetSharp.Models;
using Newtonsoft.Json;

namespace EmmetNetSharp.Tests.ActionUtils;

/// <summary>
/// Represents a class that contains tests for CSS action utils functionality.
/// </summary>
public class CssTests
{
    private readonly IActionUtilsService _actionUtilsService = new ActionUtilsService();

    [Fact]
    public void Test_SelectNextItem()
    {
        var code = File.ReadAllText(Path.Combine("Assets", "actionUtilsSample.scss")).Replace("\r\n", "\n") ?? string.Empty;

        var case1ActualJson = JsonConvert.SerializeObject(_actionUtilsService.SelectItemCss(code, 0));
        var case1ExpectedJson = JsonConvert.SerializeObject(new ActionUtilsSelectItemModel
        {
            Start = 0,
            End = 2,
            Ranges = new[]
            {
                (0, 2)
            }
        });
        Assert.Equal(case1ExpectedJson, case1ActualJson);

        // `flex: 1 1;`: parse value tokens as well
        var case2ActualJson = JsonConvert.SerializeObject(_actionUtilsService.SelectItemCss(code, 2));
        var case2ExpectedJson = JsonConvert.SerializeObject(new ActionUtilsSelectItemModel
        {
            Start = 9,
            End = 19,
            Ranges = new[]
            {
                (9, 19),
                (15, 18),
                (15, 16),
                (17, 18)
            }
        });
        Assert.Equal(case2ExpectedJson, case2ActualJson);

        // `> li` nested selector
        var case3ActualJson = JsonConvert.SerializeObject(_actionUtilsService.SelectItemCss(code, 143));
        var case3ExpectedJson = JsonConvert.SerializeObject(new ActionUtilsSelectItemModel
        {
            Start = 148,
            End = 152,
            Ranges = new[]
            {
                (148, 152)
            }
        });
        Assert.Equal(case3ExpectedJson, case3ActualJson);

        // `slot[name="controls"]:empty` top-level selector
        var case4ActualJson = JsonConvert.SerializeObject(_actionUtilsService.SelectItemCss(code, 385));
        var case4ExpectedJson = JsonConvert.SerializeObject(new ActionUtilsSelectItemModel
        {
            Start = 387,
            End = 414,
            Ranges = new[]
            {
                (387, 414)
            }
        });
        Assert.Equal(case4ExpectedJson, case4ActualJson);
    }

    [Fact]
    public void Test_SelectPreviousItem()
    {
        var code = File.ReadAllText(Path.Combine("Assets", "actionUtilsSample.scss")).Replace("\r\n", "\n") ?? string.Empty;

        // list-style-type: none;
        var case1ActualJson = JsonConvert.SerializeObject(_actionUtilsService.SelectItemCss(code, 70, true));
        var case1ExpectedJson = JsonConvert.SerializeObject(new ActionUtilsSelectItemModel
        {
            Start = 43,
            End = 65,
            Ranges = new[]
            {
                (43, 65),
                (60, 64)
            }
        });
        Assert.Equal(case1ExpectedJson, case1ActualJson);

        // border-top: 2px solid transparent;
        var case2ActualJson = JsonConvert.SerializeObject(_actionUtilsService.SelectItemCss(code, 206, true));
        var case2ExpectedJson = JsonConvert.SerializeObject(new ActionUtilsSelectItemModel
        {
            Start = 163,
            End = 197,
            Ranges = new[]
            {
                (163, 197),
                (175, 196),
                (175, 178),
                (179, 184),
                (185, 196)
            }
        });
        Assert.Equal(case2ExpectedJson, case2ActualJson);

        // > li
        var case3ActualJson = JsonConvert.SerializeObject(_actionUtilsService.SelectItemCss(code, 163, true));
        var case3ExpectedJson = JsonConvert.SerializeObject(new ActionUtilsSelectItemModel
        {
            Start = 148,
            End = 152,
            Ranges = new[]
            {
                (148, 152)
            }
        });
        Assert.Equal(case3ExpectedJson, case3ActualJson);
    }

    [Fact]
    public void Test_GetSection()
    {
        var code = File.ReadAllText(Path.Combine("Assets", "actionUtilsSample.scss")).Replace("\r\n", "\n") ?? string.Empty;

        var case1ActualJson = JsonConvert.SerializeObject(_actionUtilsService.GetCssSection(code, 260));
        var case1ExpectedJson = JsonConvert.SerializeObject(new ActionUtilsCssSection
        {
            Start = 257,
            End = 377,
            BodyStart = 269,
            BodyEnd = 376,
        });
        Assert.Equal(case1ExpectedJson, case1ActualJson);

        var case2ActualJson = JsonConvert.SerializeObject(_actionUtilsService.GetCssSection(code, 207));
        var case2ExpectedJson = JsonConvert.SerializeObject(new ActionUtilsCssSection
        {
            Start = 148,
            End = 383,
            BodyStart = 154,
            BodyEnd = 382,
        });
        Assert.Equal(case2ExpectedJson, case2ActualJson);
    }

    [Fact]
    public void Test_GetSectionWithProps()
    {
        var code = File.ReadAllText(Path.Combine("Assets", "actionUtilsSample.scss")).Replace("\r\n", "\n") ?? string.Empty;

        // Case 1
        var case1Section = _actionUtilsService.GetCssSection(code, 207, true);
        var case1Properties = case1Section.Properties;
        Assert.Equal(3, case1Properties.Length);

        Assert.Equal("border-top", GetValue(code, "name", case1Properties[0]));
        Assert.Equal("2px solid transparent", GetValue(code, "value", case1Properties[0]));
        Assert.Equal("\n        ", GetValue(code, "before", case1Properties[0]));
        Assert.Equal(";", GetValue(code, "after", case1Properties[0]));
        Assert.Equal(new[]
        {
            "2px",
            "solid",
            "transparent"
        }, case1Properties[0].ValueTokens.Select(range => SubstringFromRange(code, range)).ToArray());


        Assert.Equal("color", GetValue(code, "name", case1Properties[1]));
        Assert.Equal("$ok-gray", GetValue(code, "value", case1Properties[1]));
        Assert.Equal("\n        ", GetValue(code, "before", case1Properties[1]));
        Assert.Equal(";", GetValue(code, "after", case1Properties[1]));
        Assert.Equal(new[]
        {
            "$ok-gray"
        }, case1Properties[1].ValueTokens.Select(range => SubstringFromRange(code, range)).ToArray());

        Assert.Equal("cursor", GetValue(code, "name", case1Properties[2]));
        Assert.Equal("pointer", GetValue(code, "value", case1Properties[2]));
        Assert.Equal("\n        ", GetValue(code, "before", case1Properties[2]));
        Assert.Equal(";", GetValue(code, "after", case1Properties[2]));
        Assert.Equal(new[]
        {
            "pointer"
        }, case1Properties[2].ValueTokens.Select(range => SubstringFromRange(code, range)).ToArray());

        // Case 2
        var case2Section = _actionUtilsService.GetCssSection(code, 450, true);
        var case2Properties = case2Section.Properties;
        Assert.Equal(2, case2Properties.Length);

        Assert.Equal("padding", GetValue(code, "name", case2Properties[0]));
        Assert.Equal("10px", GetValue(code, "value", case2Properties[0]));
        Assert.Equal("\n    ", GetValue(code, "before", case2Properties[0]));
        Assert.Equal(";", GetValue(code, "after", case2Properties[0]));

        Assert.Equal("color", GetValue(code, "name", case2Properties[1]));
        Assert.Equal("#000", GetValue(code, "value", case2Properties[1]));
        Assert.Equal("\n    ", GetValue(code, "before", case2Properties[1]));
        Assert.Equal(";", GetValue(code, "after", case2Properties[1]));
    }

    public static string GetValue(string code, string key, CssProperty property)
    {
        (int, int)? range = null;

        if (key == "before")
            range = (property.Before, property.Name.Item1);
        else if (key == "after")
            range = (property.Value.Item2, property.After);
        else if (key == "name")
            range = property.Name;
        else if (key == "value")
            range = property.Value;

        if (range != null)
            return code[range.Value.Item1..range.Value.Item2];
        else 
            return string.Empty;
    }

    public static string SubstringFromRange(string code, (int, int) range) => code[range.Item1..range.Item2];
}
