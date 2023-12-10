using EmmetNetSharp.Enums;
using EmmetNetSharp.Models;

namespace EmmetNetSharp.Tests.Abbreviation;

/// <summary>
/// Represents a class that contains tests for expander functionalities.
/// </summary>
public class ExpandTests
{
    private readonly IAbbreviationService _abbreviationService = new AbbreviationService();

    [Fact]
    public void Test_Basic()
    {
        Assert.Equal(
            "<input type=\"text\" value=\"text1\"><input type=\"text\" value=\"text2\">",
            _abbreviationService.ExpandAbbreviation("input[value=\"text$\"]*2")
        );

        Assert.Equal(
            "<ul>\n\t<li class=\"item1\"></li>\n\t<li class=\"item2\"></li>\n</ul>",
            _abbreviationService.ExpandAbbreviation("ul>.item$*2")
        );

        // insert text into abbreviation
        Assert.Equal(
            "<ul>\n\t<li class=\"item1\">foo</li>\n\t<li class=\"item2\">bar</li>\n</ul>",
            _abbreviationService.ExpandAbbreviation("ul>.item$*", new UserConfig { Text = new[] { "foo", "", "bar", "", "" } })
        );

        // insert TextMate-style fields/tabstops in output
        //Assert.Equal(
        //    "<ul>\n\t<li class=\"item1\">${1}</li>\n\t<li class=\"item2\">${2}</li>\n</ul>",
        //    _abbreviationService.ExpandAbbreviation("ul>.item$*2", new UserConfig { Options = new AbbreviationOptions { outputfield!!! } })
        //);
    }

    [Fact]
    public void Test_Abbreviations()
    {
        var snippets = new Dictionary<string, string>
        {
            { "test", "test[!foo bar. baz={}]" }
        };
        var opt = new UserConfig { Snippets = snippets };
        var reverse = new UserConfig
        {
            Snippets = snippets,
            Options = new AbbreviationOptions 
            { 
                OutputReverseAttributes = true 
            }
        };

        Assert.Equal(
            "<a href=\"\" class=\"test\"></a>",
            _abbreviationService.ExpandAbbreviation("a.test")
        );

        Assert.Equal(
            "<a class=\"test\" href=\"\"></a>",
            _abbreviationService.ExpandAbbreviation("a.test", reverse)
        );

        Assert.Equal(
            "<test bar=\"bar\" baz={}></test>",
            _abbreviationService.ExpandAbbreviation("test", opt)
        );

        Assert.Equal(
            "<test bar=\"bar\" baz={}></test>",
            _abbreviationService.ExpandAbbreviation("test[foo]", opt)
        );

        Assert.Equal(
            "<test foo=\"1\" bar=\"bar\" baz={a}></test>",
            _abbreviationService.ExpandAbbreviation("test[baz=a foo=1]", opt)
        );

        // Apply attributes in reverse order
        Assert.Equal(
            "<test bar=\"bar\" baz={}></test>",
            _abbreviationService.ExpandAbbreviation("test", reverse)
        );

        Assert.Equal(
            "<test bar=\"bar\" baz={}></test>",
            _abbreviationService.ExpandAbbreviation("test[foo]", reverse)
        );

        Assert.Equal(
            "<test baz={a} foo=\"1\" bar=\"bar\"></test>",
            _abbreviationService.ExpandAbbreviation("test[baz=a foo=1]", reverse)
        );
    }

    [Fact]
    public void Test_Expressions()
    {
        Assert.Equal(
            "<span>{foo}</span>",
            _abbreviationService.ExpandAbbreviation("span{{foo}}")
        );

        Assert.Equal(
            "<span>foo</span>",
            _abbreviationService.ExpandAbbreviation("span{foo}")
        );

        Assert.Equal(
            "<span foo={bar}></span>",
            _abbreviationService.ExpandAbbreviation("span[foo={bar}]")
        );

        Assert.Equal(
            "<span foo={{bar}}></span>",
            _abbreviationService.ExpandAbbreviation("span[foo={{bar}}]")
        );
    }

    [Fact]
    public void Test_Numbering()
    {
        Assert.Equal(
            "<ul>\n\t<li class=\"item5\"></li>\n\t<li class=\"item4\"></li>\n\t<li class=\"item3\"></li>\n\t<li class=\"item2\"></li>\n\t<li class=\"item1\"></li>\n</ul>",
            _abbreviationService.ExpandAbbreviation("ul>li.item$@-*5")
        );
    }

    [Fact]
    public void Test_Syntax()
    {
        Assert.Equal(
            "<ul>\n\t<li class=\"item1\"></li>\n\t<li class=\"item2\"></li>\n</ul>",
            _abbreviationService.ExpandAbbreviation("ul>.item$*2", new UserConfig { Syntax = "html" })
        );

        Assert.Equal(
            "ul\n\tli.item1 \n\tli.item2 ",
            _abbreviationService.ExpandAbbreviation("ul>.item$*2", new UserConfig { Syntax = "slim" })
        );

        Assert.Equal(
            "<xsl:variable name=\"a\">\n\t<div></div>\n</xsl:variable>",
            _abbreviationService.ExpandAbbreviation("xsl:variable[name=a select=b]>div", new UserConfig { Syntax = "xsl" })
        );
    }

    [Fact]
    public void Test_CustomProfile()
    {
        Assert.Equal(
            "<img src=\"\" alt=\"\">",
            _abbreviationService.ExpandAbbreviation("img")
        );

        Assert.Equal(
            "<img src=\"\" alt=\"\" />",
            _abbreviationService.ExpandAbbreviation("img", new UserConfig { Options = new AbbreviationOptions { OutputSelfClosingStyle = SelfClosingStyle.Xhtml } })
        );
    }

    [Fact]
    public void Test_CustomVariables()
    {
        Assert.Equal(
            "<div charset=\"UTF-8\">UTF-8</div>",
            _abbreviationService.ExpandAbbreviation("[charset=${charset}]{${charset}}")
        );

        Assert.Equal(
            "<div charset=\"ru-RU\">ru-RU</div>",
            _abbreviationService.ExpandAbbreviation("[charset=${charset}]{${charset}}", new UserConfig { Variables = new Dictionary<string, string> { { "charset", "ru-RU" } } })
        );
    }

    [Fact]
    public void Test_CustomSnippets()
    {
        var config = new UserConfig
        {
            Snippets = new Dictionary<string, string>
            {
                { "link", "link[foo=bar href]/" },
                { "foo", ".foo[bar=baz]" },
                { "repeat", "div>ul>li{Hello World}*3" }
            }
        };

        Assert.Equal(
            "<div class=\"foo\" bar=\"baz\"></div>",
            _abbreviationService.ExpandAbbreviation("foo", config)
        );

        // `link:css` depends on `link` snippet so changing it will result in altered `link:css` result
        Assert.Equal(
            "<link rel=\"stylesheet\" href=\"style.css\">",
            _abbreviationService.ExpandAbbreviation("link:css")
        );

        Assert.Equal(
            "<link foo=\"bar\" href=\"style.css\">",
            _abbreviationService.ExpandAbbreviation("link:css", config)
        );

        Assert.Equal(
            "<div>\n\t<ul>\n\t\t<li>Hello World</li>\n\t\t<li>Hello World</li>\n\t\t<li>Hello World</li>\n\t</ul>\n</div>",
            _abbreviationService.ExpandAbbreviation("repeat", config)
        );
    }

    [Fact]
    public void Test_FormatterOptions()
    {

    }

    [Fact]
    public void Test_JSX()
    {
        var config = new UserConfig { Syntax = "jsx" };

        Assert.Equal(
            "<div id=\"foo\" className=\"bar\"></div>",
            _abbreviationService.ExpandAbbreviation("div#foo.bar", config)
        );

        Assert.Equal(
            "<label htmlFor=\"a\"></label>",
            _abbreviationService.ExpandAbbreviation("label[for=a]", config)
        );

        Assert.Equal(
            "<Foo.Bar></Foo.Bar>",
            _abbreviationService.ExpandAbbreviation("Foo.Bar", config)
        );

        Assert.Equal(
            "<div className={theme.style}></div>",
            _abbreviationService.ExpandAbbreviation("div.{theme.style}", config)
        );
    }

    [Fact]
    public void Test_OverrideAttributes()
    {

    }

    [Fact]
    public void Test_WrapWithAbbreviation()
    {

    }

    [Fact]
    public void Test_ClassNames()
    {

    }

    [Fact]
    public void Test_Pug_Basics()
    {

    }
}
