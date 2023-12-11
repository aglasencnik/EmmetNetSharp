using EmmetNetSharp.Enums;
using EmmetNetSharp.Models;

namespace EmmetNetSharp.Tests.Abbreviation;

/// <summary>
/// Represents a class that contains tests for stylesheet functionalities.
/// </summary>
public class StylesheetTests
{
    private readonly IAbbreviationService _abbreviationService = new AbbreviationService();

    private readonly UserConfig _config = new UserConfig
    {
        Type = SyntaxType.Stylesheet,
        Snippets = new Dictionary<string, string>
        {
            { "mten", "margin: 10px;" },
            { "fsz", "font-size" },
            { "gt", "grid-template: repeat(2,auto) / repeat(auto-fit, minmax(250px, 1fr))" },
            { "bxsh", "box-shadow: var(--bxsh-${1})" }
        }
    };

    [Fact]
    public void Test_Keyword()
    {
        Assert.Equal("border: 1px solid;", _abbreviationService.ExpandAbbreviation("bd1-s", _config));
        Assert.Equal("display: inline-block;", _abbreviationService.ExpandAbbreviation("dib", _config));
        Assert.Equal("box-sizing: border-box;", _abbreviationService.ExpandAbbreviation("bxsz", _config));
        Assert.Equal("box-sizing: border-box;", _abbreviationService.ExpandAbbreviation("bxz", _config));
        Assert.Equal("box-sizing: content-box;", _abbreviationService.ExpandAbbreviation("bxzc", _config));
        Assert.Equal("float: left;", _abbreviationService.ExpandAbbreviation("fl", _config));
        Assert.Equal("float: left;", _abbreviationService.ExpandAbbreviation("fll", _config));

        Assert.Equal("position: relative;", _abbreviationService.ExpandAbbreviation("pos", _config));
        Assert.Equal("position: absolute;", _abbreviationService.ExpandAbbreviation("poa", _config));
        Assert.Equal("position: relative;", _abbreviationService.ExpandAbbreviation("por", _config));
        Assert.Equal("position: fixed;", _abbreviationService.ExpandAbbreviation("pof", _config));
        Assert.Equal("position: absolute;", _abbreviationService.ExpandAbbreviation("pos-a", _config));

        Assert.Equal("margin: ;", _abbreviationService.ExpandAbbreviation("m", _config));
        Assert.Equal("margin: 0;", _abbreviationService.ExpandAbbreviation("m0", _config));

        // use `auto` as global keyword
        Assert.Equal("margin: 0 auto;", _abbreviationService.ExpandAbbreviation("m0-a", _config));
        Assert.Equal("margin: auto;", _abbreviationService.ExpandAbbreviation("m-a", _config));

        Assert.Equal("background: #000;", _abbreviationService.ExpandAbbreviation("bg", _config));

        Assert.Equal("border: 1px solid #000;", _abbreviationService.ExpandAbbreviation("bd", _config));
        Assert.Equal("border: 0 solid #fc0;", _abbreviationService.ExpandAbbreviation("bd0-s#fc0", _config));
        Assert.Equal("border: 0 dot-dash #fc0;", _abbreviationService.ExpandAbbreviation("bd0-dd#fc0", _config));
        Assert.Equal("border: 0 hidden #fc0;", _abbreviationService.ExpandAbbreviation("bd0-h#fc0", _config));

        Assert.Equal("transform: translate(x, y);", _abbreviationService.ExpandAbbreviation("trf-trs", _config));

        Assert.Equal("grid-template-columns: repeat();", _abbreviationService.ExpandAbbreviation("gtc", _config));
        Assert.Equal("grid-template-rows: repeat();", _abbreviationService.ExpandAbbreviation("gtr", _config));

        Assert.Equal("list-style: none;", _abbreviationService.ExpandAbbreviation("lis:n", _config));
        Assert.Equal("list-style-type: none;", _abbreviationService.ExpandAbbreviation("list:n", _config));
        Assert.Equal("border-top: none;", _abbreviationService.ExpandAbbreviation("bdt:n", _config));
        Assert.Equal("background-image: none;", _abbreviationService.ExpandAbbreviation("bgi:n", _config));
        Assert.Equal("quotes: none;", _abbreviationService.ExpandAbbreviation("q:n", _config));

        Assert.Equal("box-shadow: var(--bxsh-);", _abbreviationService.ExpandAbbreviation("bxsh", _config));
    }

    [Fact]
    public void Test_Numeric()
    {
        Assert.Equal("padding: 0;", _abbreviationService.ExpandAbbreviation("p0", _config));
        Assert.Equal("padding: 10px;", _abbreviationService.ExpandAbbreviation("p10", _config));
        Assert.Equal("padding: 0.4em;", _abbreviationService.ExpandAbbreviation("p.4", _config));
        Assert.Equal("font-size: 10px;", _abbreviationService.ExpandAbbreviation("fz10", _config));
        Assert.Equal("font-size: 1em;", _abbreviationService.ExpandAbbreviation("fz1.", _config));
        Assert.Equal("padding: 10%;", _abbreviationService.ExpandAbbreviation("p10p", _config));
        Assert.Equal("z-index: 10;", _abbreviationService.ExpandAbbreviation("z10", _config));
        Assert.Equal("padding: 10rem;", _abbreviationService.ExpandAbbreviation("p10r", _config));
        Assert.Equal("margin: 10px;", _abbreviationService.ExpandAbbreviation("mten", _config));

        Assert.Equal("font-size: ;", _abbreviationService.ExpandAbbreviation("fz", _config));
        Assert.Equal("font-size: 12px;", _abbreviationService.ExpandAbbreviation("fz12", _config));
        Assert.Equal("font-size: ;", _abbreviationService.ExpandAbbreviation("fsz", _config));
        Assert.Equal("font-size: 12px;", _abbreviationService.ExpandAbbreviation("fsz12", _config));
        Assert.Equal("font-style: italic;", _abbreviationService.ExpandAbbreviation("fs", _config));

        Assert.Equal("user-select: none;", _abbreviationService.ExpandAbbreviation("us", _config));

        Assert.Equal("opacity: 1;", _abbreviationService.ExpandAbbreviation("opa1", _config));
        Assert.Equal("opacity: 0.1;", _abbreviationService.ExpandAbbreviation("opa.1", _config));
        Assert.Equal("opacity: .a;", _abbreviationService.ExpandAbbreviation("opa.a", _config));
    }

    [Fact]
    public void Test_NumericWithFormat()
    {
        var config = new UserConfig
        {
            Type = SyntaxType.Stylesheet,
            Options = new AbbreviationOptions
            {
                StylesheetIntUnit = "pt",
                StylesheetFloatUnit = "vh",
                StylesheetUnitAliases = new Dictionary<string, string>
                {
                    { "e", "em" },
                    { "p", "%" },
                    { "x", "ex" },
                    { "r", " / @rem" }
                }
            }
        };

        Assert.Equal("padding: 0;", _abbreviationService.ExpandAbbreviation("p0", config));
        Assert.Equal("padding: 10pt;", _abbreviationService.ExpandAbbreviation("p10", config));
        Assert.Equal("padding: 0.4vh;", _abbreviationService.ExpandAbbreviation("p.4", config));
        Assert.Equal("padding: 10%;", _abbreviationService.ExpandAbbreviation("p10p", config));
        Assert.Equal("z-index: 10;", _abbreviationService.ExpandAbbreviation("z10", config));
        Assert.Equal("padding: 10 / @rem;", _abbreviationService.ExpandAbbreviation("p10r", config));
    }

    [Fact]
    public void Test_Important()
    {
        Assert.Equal("!important", _abbreviationService.ExpandAbbreviation("!", _config));
        Assert.Equal("padding:  !important;", _abbreviationService.ExpandAbbreviation("p!", _config));
        Assert.Equal("padding: 0 !important;", _abbreviationService.ExpandAbbreviation("p0!", _config));
    }

    [Fact]
    public void Test_Color()
    {
        Assert.Equal("color: #000;", _abbreviationService.ExpandAbbreviation("c", _config));
        Assert.Equal("color: #000;", _abbreviationService.ExpandAbbreviation("c#", _config));
        Assert.Equal("color: rgba(255, 255, 255, 0.5);", _abbreviationService.ExpandAbbreviation("c#f.5", _config));
        Assert.Equal("color: rgba(255, 255, 255, 0.5) !important;", _abbreviationService.ExpandAbbreviation("c#f.5!", _config));
        Assert.Equal("background-color: #fff;", _abbreviationService.ExpandAbbreviation("bgc", _config));
        Assert.Equal("background-color: #f0f0f0;", _abbreviationService.ExpandAbbreviation("bgc#f0", _config));
        Assert.Equal("background-color: #f1f1f1;", _abbreviationService.ExpandAbbreviation("bgc#f1", _config));
    }

    [Fact]
    public void Test_Snippets()
    {
        Assert.Equal("@keyframes identifier {\n\t\n}", _abbreviationService.ExpandAbbreviation("@k", _config));
        Assert.Equal("@media screen {\n\t\n}", _abbreviationService.ExpandAbbreviation("@", _config));

        Assert.Equal("@keyframes name {\n\t\n}", _abbreviationService.ExpandAbbreviation("@k-name", _config));
        Assert.Equal("@keyframes name {\n\t10\n}", _abbreviationService.ExpandAbbreviation("@k-name10", _config));
        Assert.Equal("grid-template: repeat(2, auto) / repeat(auto-fit, minmax(250px, 1fr));", _abbreviationService.ExpandAbbreviation("gt", _config));
    }

    [Fact]
    public void Test_MultipleProperties()
    {
        Assert.Equal("padding: 10px;\nmargin: 10px 20px;", _abbreviationService.ExpandAbbreviation("p10+m10-20", _config));
        Assert.Equal("padding: ;\nborder: 1px solid #000;", _abbreviationService.ExpandAbbreviation("p+bd", _config));
    }

    [Fact]
    public void Test_Functions()
    {
        Assert.Equal("transform: scale(2, y);", _abbreviationService.ExpandAbbreviation("trf-s(2)", _config));
        Assert.Equal("transform: scale(2, 3);", _abbreviationService.ExpandAbbreviation("trf-s(2, 3)", _config));
    }

    [Fact]
    public void Test_CaseInsensitiveMatch()
    {
        Assert.Equal("transform: rotateX(angle);", _abbreviationService.ExpandAbbreviation("trf:rx", _config));
    }

    [Fact]
    public void Test_GradientResolver()
    {
        Assert.Equal("background-image: linear-gradient();", _abbreviationService.ExpandAbbreviation("lg", _config));
        Assert.Equal("background-image: linear-gradient(to right, #000, rgba(255, 0, 0, 0.5));", _abbreviationService.ExpandAbbreviation("lg(to right, #0, #f00.5)", _config));
    }

    [Fact]
    public void Test_CssInJs()
    {
        var config = new UserConfig
        {
            Type = SyntaxType.Stylesheet,
            Options = new AbbreviationOptions
            {
                StylesheetJson = true,
                StylesheetBetween = ": "
            }
        };

        Assert.Equal("padding: 10,\nmarginTop: \'10px 20px\',", _abbreviationService.ExpandAbbreviation("p10+mt10-20", config));
        Assert.Equal("backgroundColor: \'#fff\',", _abbreviationService.ExpandAbbreviation("bgc", config));
    }

    [Fact]
    public void Test_ResolveContextValue()
    {
        var config = new UserConfig
        {
            Type = SyntaxType.Stylesheet,
            Context = new AbbreviationContext
            {
                Name = "align-content"
            }
        };

        Assert.Equal("start", _abbreviationService.ExpandAbbreviation("s", config));
        Assert.Equal("auto", _abbreviationService.ExpandAbbreviation("a", config));
    }

    [Fact]
    public void Test_LimitSnippetsByScope()
    {
        var sectionScope = new UserConfig
        {
            Type = SyntaxType.Stylesheet,
            Context = new AbbreviationContext
            {
                Name = "@@section"
            },
            Snippets = new Dictionary<string, string>
            {
                { "mten", "margin: 10px;" },
                { "fsz", "font-size" },
                { "myCenterAwesome", "body {\n\tdisplay: grid;\n}" }
            }
        };

        var propertyScope = new UserConfig
        {
            Type = SyntaxType.Stylesheet,
            Context = new AbbreviationContext
            {
                Name = "@@property"
            },
            Snippets = new Dictionary<string, string>
            {
                { "mten", "margin: 10px;" },
                { "fsz", "font-size" },
                { "myCenterAwesome", "body {\n\tdisplay: grid;\n}" }
            }
        };

        Assert.Equal("body {\n\tdisplay: grid;\n}", _abbreviationService.ExpandAbbreviation("m", sectionScope));
        Assert.Equal("", _abbreviationService.ExpandAbbreviation("b", sectionScope));
        Assert.Equal("margin: ;", _abbreviationService.ExpandAbbreviation("m", propertyScope));
    }
}
