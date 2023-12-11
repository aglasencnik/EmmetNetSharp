using EmmetNetSharp.Models;

namespace EmmetNetSharp.Tests.Abbreviation;

/// <summary>
/// Represents a class that contains tests for markup functionalities.
/// </summary>
public class MarkupTests
{
    private readonly IAbbreviationService _abbreviationService = new AbbreviationService();

    private readonly UserConfig _bem = new UserConfig
    {
        Options = new AbbreviationOptions
        {
            BemEnabled = true,
            OutputNewLine = "",
            OutputIndent = ""
        }
    };

    [Fact]
    public void Test_ImplicitTags()
    {
        var config = new UserConfig
        {
            Options = new AbbreviationOptions
            {
                OutputIndent = "",
                OutputNewLine = ""
            }
        };

        Assert.Equal(
            "<div class=\"\"></div>",
            _abbreviationService.ExpandAbbreviation(".", config)
        );

        Assert.Equal(
            "<div class=\"foo\"><div class=\"bar\"></div></div>",
            _abbreviationService.ExpandAbbreviation(".foo>.bar", config)
        );

        Assert.Equal(
            "<p class=\"foo\"><span class=\"bar\"></span></p>",
            _abbreviationService.ExpandAbbreviation("p.foo>.bar", config)
        );

        Assert.Equal(
            "<ul><li class=\"item\"></li><li class=\"item\"></li></ul>",
            _abbreviationService.ExpandAbbreviation("ul>.item*2", config)
        );

        Assert.Equal(
            "<table><tr class=\"row\"><td class=\"cell\"></td></tr></table>",
            _abbreviationService.ExpandAbbreviation("table>.row>.cell", config)
        );

        Assert.Equal(
            "test",
            _abbreviationService.ExpandAbbreviation("{test}", config)
        );

        Assert.Equal(
            "<div class=\"\">test</div>",
            _abbreviationService.ExpandAbbreviation(".{test}", config)
        );

        Assert.Equal(
            "<ul><li class=\"item1\"></li><li class=\"item2\"></li></ul>",
            _abbreviationService.ExpandAbbreviation("ul>.item$*2", config)
        );
    }

    [Fact]
    public void Test_Xsl()
    {
        var config = new UserConfig
        {
            Syntax = "xsl",
            Options = new AbbreviationOptions
            {
                OutputIndent = "",
                OutputNewLine = ""
            }
        };

        Assert.Equal(
            "<xsl:variable select=\"\"></xsl:variable>",
            _abbreviationService.ExpandAbbreviation("xsl:variable[select]", config)
        );

        Assert.Equal(
            "<xsl:with-param select=\"\"></xsl:with-param>",
            _abbreviationService.ExpandAbbreviation("xsl:with-param[select]", config)
        );

        Assert.Equal(
            "<xsl:variable><div></div></xsl:variable>",
            _abbreviationService.ExpandAbbreviation("xsl:variable[select]>div", config)
        );

        Assert.Equal(
            "<xsl:with-param>foo</xsl:with-param>",
            _abbreviationService.ExpandAbbreviation("xsl:with-param[select]{foo}", config)
        );
    }

    [Fact]
    public void Test_BEM_Modifiers()
    {
        Assert.Equal(
            "<div class=\"b b_m\"></div>",
            _abbreviationService.ExpandAbbreviation("div.b_m", _bem)
        );

        Assert.Equal(
            "<div class=\"b b_m\"></div>",
            _abbreviationService.ExpandAbbreviation("div.b._m", _bem)
        );

        Assert.Equal(
            "<div class=\"b b_m1 b_m2\"></div>",
            _abbreviationService.ExpandAbbreviation("div.b_m1._m2", _bem)
        );

        Assert.Equal(
            "<div class=\"b\"><div class=\"b b_m\"></div></div>",
            _abbreviationService.ExpandAbbreviation("div.b>div._m", _bem)
        );

        Assert.Equal(
            "<div class=\"b\"><div class=\"b b_m1\"><div class=\"b b_m2\"></div></div></div>",
            _abbreviationService.ExpandAbbreviation("div.b>div._m1>div._m2", _bem)
        );

        // classnames with -
        Assert.Equal(
            "<div class=\"b\"><div class=\"b b_m1-m2\"></div></div>",
            _abbreviationService.ExpandAbbreviation("div.b>div._m1-m2", _bem)
        );
    }

    [Fact]
    public void Test_BEM_Elements()
    {
        Assert.Equal(
            "<div class=\"b\"><div class=\"b__e\"></div></div>",
            _abbreviationService.ExpandAbbreviation("div.b>div.-e", _bem)
        );

        Assert.Equal(
            "<div class=\"b\"><div class=\"b__e\"></div></div>",
            _abbreviationService.ExpandAbbreviation("div.b>div.---e", _bem)
        );

        Assert.Equal(
            "<div class=\"b\"><div class=\"b__e\"><div class=\"b__e\"></div></div></div>",
            _abbreviationService.ExpandAbbreviation("div.b>div.-e>div.-e", _bem)
        );

        Assert.Equal(
            "<div></div>",
            _abbreviationService.ExpandAbbreviation("div", _bem)
        );

        // get block name from proper ancestor
        Assert.Equal(
            "<div class=\"b1\"><div class=\"b2 b2_m1\"><div class=\"b2__e1\"></div><div class=\"b1__e2 b1__e2_m2\"></div></div></div>",
            _abbreviationService.ExpandAbbreviation("div.b1>div.b2_m1>div.-e1+div.---e2_m2", _bem)
        );

        // class names with -
        Assert.Equal(
            "<div class=\"b\"><div class=\"b__m1-m2\"></div></div>",
            _abbreviationService.ExpandAbbreviation("div.b>div.-m1-m2", _bem)
        );

        // class names with _
        Assert.Equal(
            "<div class=\"b b_m_o\"></div>",
            _abbreviationService.ExpandAbbreviation("div.b_m_o", _bem)
        );
    }

    [Fact]
    public void Test_BEM_CustomizeModifier()
    {
        var config = new UserConfig
        {
            Options = new AbbreviationOptions
            {
                BemEnabled = true,
                BemElement = "-",
                BemModifier = "__"
            }
        };

        Assert.Equal(
            "<div class=\"b b__m\"></div>",
            _abbreviationService.ExpandAbbreviation("div.b_m", config)
        );

        Assert.Equal(
            "<div class=\"b b__m\"></div>",
            _abbreviationService.ExpandAbbreviation("div.b._m", config)
        );
    }

    [Fact]
    public void Test_BEM_MultipleClasses()
    {
        Assert.Equal(
            "<div class=\"b b_m c\"></div>",
            _abbreviationService.ExpandAbbreviation("div.b_m.c", _bem)
        );

        Assert.Equal(
            "<div class=\"b\"><div class=\"b b_m c\"></div></div>",
            _abbreviationService.ExpandAbbreviation("div.b>div._m.c", _bem)
        );

        Assert.Equal(
            "<div class=\"b\"><div class=\"b__m c\"></div></div>",
            _abbreviationService.ExpandAbbreviation("div.b>div.-m.c", _bem)
        );
    }

    [Fact]
    public void Test_BEM_ParentContext()
    {
        var config = new UserConfig
        {
            Context = new AbbreviationContext
            {
                Name = "div",
                Attributes = new Dictionary<string, string>
                {
                    { "class", "bl" }
                }
            },
            Options = new AbbreviationOptions
            {
                BemEnabled = true,
            }
        };

        Assert.Equal(
            "<div class=\"bl__e bl__e_m\"></div>",
            _abbreviationService.ExpandAbbreviation(".-e_m", config)
        );
    }
}
