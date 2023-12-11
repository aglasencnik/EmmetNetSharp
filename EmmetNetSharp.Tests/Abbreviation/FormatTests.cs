using EmmetNetSharp.Enums;
using EmmetNetSharp.Models;

namespace EmmetNetSharp.Tests.Abbreviation;

/// <summary>
/// Represents a class that contains tests for formatter functionalities.
/// </summary>
public class FormatTests
{
    private readonly IAbbreviationService _abbreviationService = new AbbreviationService();

    private readonly UserConfig _hamlConfig = new UserConfig
    {
        Syntax = "haml"
    };

    private readonly UserConfig _pugConfig = new UserConfig
    {
        Syntax = "pug"
    };

    private readonly UserConfig _slimConfig = new UserConfig
    {
        Syntax = "slim"
    };

    #region HTML Tests

    [Fact]
    public void Test_HTML_Basic()
    {
        Assert.Equal(
            "<div>\n\t<p></p>\n</div>", 
            _abbreviationService.ExpandAbbreviation("div>p")
        );

        Assert.Equal(
            "<div>\n\t<p></p>\n\t<p></p>\n\t<p></p>\n</div>",
            _abbreviationService.ExpandAbbreviation("div>p*3")
        );

        Assert.Equal(
            "<div id=\"a\">\n\t<p class=\"b\"><span></span></p>\n\t<p class=\"b\"><span></span></p>\n</div>",
            _abbreviationService.ExpandAbbreviation("div#a>p.b*2>span")
        );

        Assert.Equal(
            "<div>\n\t<div>\n\t\t<div></div>\n\t</div>\n</div>",
            _abbreviationService.ExpandAbbreviation("div>div>div")
        );

        Assert.Equal(
            "<table>\n\t<tr>\n\t\t<td>item</td>\n\t\t<td>item</td>\n\t</tr>\n\t<tr>\n\t\t<td>item</td>\n\t\t<td>item</td>\n\t</tr>\n</table>",
            _abbreviationService.ExpandAbbreviation("table>tr*2>td{item}*2")
        );
    }

    [Fact]
    public void Test_HTML_InlineElements()
    {
        var profile = new UserConfig
        {
            Options = new AbbreviationOptions
            {
                OutputInlineBreak = 3
            }
        };

        var breakInline = new UserConfig
        {
            Options = new AbbreviationOptions
            {
                OutputInlineBreak = 1
            }
        };

        var keepInline = new UserConfig
        {
            Options = new AbbreviationOptions
            {
                OutputInlineBreak = 0
            }
        };

        var xhtml = new UserConfig
        {
            Options = new AbbreviationOptions
            {
                OutputSelfClosingStyle = SelfClosingStyle.Xhtml
            }
        };

        Assert.Equal(
            "<div>\n\t<a href=\"\">\n\t\t<b></b>\n\t\t<b></b>\n\t\t<b></b>\n\t</a>\n</div>",
            _abbreviationService.ExpandAbbreviation("div>a>b*3", xhtml)
        );

        Assert.Equal(
            "<p><i></i></p>",
            _abbreviationService.ExpandAbbreviation("p>i", profile)
        );

        Assert.Equal(
            "<p><i></i><i></i></p>",
            _abbreviationService.ExpandAbbreviation("p>i*2", profile)
        );

        Assert.Equal(
            "<p>\n\t<i></i>\n\t<i></i>\n</p>",
            _abbreviationService.ExpandAbbreviation("p>i*2", breakInline)
        );

        Assert.Equal(
            "<p>\n\t<i></i>\n\t<i></i>\n\t<i></i>\n</p>",
            _abbreviationService.ExpandAbbreviation("p>i*3", profile)
        );

        Assert.Equal(
            "<p><i></i><i></i><i></i></p>",
            _abbreviationService.ExpandAbbreviation("p>i*3", keepInline)
        );

        Assert.Equal(
            "<i></i><i></i>",
            _abbreviationService.ExpandAbbreviation("i*2", profile)
        );

        Assert.Equal(
            "<i></i>\n<i></i>\n<i></i>",
            _abbreviationService.ExpandAbbreviation("i*3", profile)
        );

        Assert.Equal(
            "<i>a</i><i>b</i>",
            _abbreviationService.ExpandAbbreviation("i{a}+i{b}", profile)
        );

        Assert.Equal(
            "<img src=\"\" alt=\"\" />\n<p></p>",
            _abbreviationService.ExpandAbbreviation("img[src]/+p", xhtml)
        );

        Assert.Equal(
            "<div>\n\t<img src=\"\" alt=\"\" />\n\t<p></p>\n</div>",
            _abbreviationService.ExpandAbbreviation("div>img[src]/+p", xhtml)
        );

        Assert.Equal(
            "<div>\n\t<p></p>\n\t<img src=\"\" alt=\"\" />\n</div>",
            _abbreviationService.ExpandAbbreviation("div>p+img[src]/", xhtml)
        );

        Assert.Equal(
            "<div>\n\t<p></p>\n\t<img src=\"\" alt=\"\" />\n\t<p></p>\n</div>",
            _abbreviationService.ExpandAbbreviation("div>p+img[src]/+p", xhtml)
        );

        Assert.Equal(
            "<div>\n\t<p></p>\n\t<img src=\"\" alt=\"\" /><img src=\"\" alt=\"\" />\n\t<p></p>\n</div>",
            _abbreviationService.ExpandAbbreviation("div>p+img[src]/*2+p", xhtml)
        );

        Assert.Equal(
            "<div>\n\t<p></p>\n\t<img src=\"\" alt=\"\" />\n\t<img src=\"\" alt=\"\" />\n\t<img src=\"\" alt=\"\" />\n\t<p></p>\n</div>",
            _abbreviationService.ExpandAbbreviation("div>p+img[src]/*3+p", xhtml)
        );
    }

    [Fact]
    public void Test_HTML_GenerateFields()
    {
        Assert.Equal(
            "<a href=\"\"></a>",
            _abbreviationService.ExpandAbbreviation("a[href]")
        );

        Assert.Equal(
            "<a href=\"\"></a><a href=\"\"></a>",
            _abbreviationService.ExpandAbbreviation("a[href]*2")
        );

        Assert.Equal(
            " foo bar\n foo bar",
            _abbreviationService.ExpandAbbreviation("{${0} ${1:foo} ${2:bar}}*2")
        );

        Assert.Equal(
            " foo bar\n foo bar",
            _abbreviationService.ExpandAbbreviation("{${0} ${1:foo} ${2:bar}}*2")
        );

        Assert.Equal(
            "<ul>\n\t<li></li>\n\t<li></li>\n</ul>",
            _abbreviationService.ExpandAbbreviation("ul>li*2")
        );

        Assert.Equal(
            "<div><img src=\"\" alt=\"\"></div>",
            _abbreviationService.ExpandAbbreviation("div>img[src]/")
        );
    }

    [Fact]
    public void Test_HTML_MixedContent()
    {
        Assert.Equal(
            "<div>foo</div>",
            _abbreviationService.ExpandAbbreviation("div{foo}")
        );

        Assert.Equal(
            "<div>foo</div>",
            _abbreviationService.ExpandAbbreviation("div>{foo}")
        );

        Assert.Equal(
            "<div>\n\tfoo\n\tbar\n</div>",
            _abbreviationService.ExpandAbbreviation("div>{foo}+{bar}")
        );

        Assert.Equal(
            "<div>\n\tfoo\n\tbar\n\t<p></p>\n</div>",
            _abbreviationService.ExpandAbbreviation("div>{foo}+{bar}+p")
        );

        Assert.Equal(
            "<div>\n\tfoo\n\tbar\n\t<p></p>\n\tfoo\n\tbar\n\t<p></p>\n</div>",
            _abbreviationService.ExpandAbbreviation("div>{foo}+{bar}+p+{foo}+{bar}+p")
        );

        Assert.Equal(
            "<div>\n\tfoo\n\t<p></p>\n\tbar\n</div>",
            _abbreviationService.ExpandAbbreviation("div>{foo}+p+{bar}")
        );

        Assert.Equal(
            "<div>\n\tfoo\n\t<p></p>\n</div>",
            _abbreviationService.ExpandAbbreviation("div>{foo}>p")
        );

        Assert.Equal(
            "<div><!--  --></div>",
            _abbreviationService.ExpandAbbreviation("div>{<!-- ${0} -->}")
        );

        Assert.Equal(
            "<div>\n\t<!--  -->\n\t<p></p>\n</div>",
            _abbreviationService.ExpandAbbreviation("div>{<!-- ${0} -->}+p")
        );

        Assert.Equal(
            "<div>\n\t<p></p>\n\t<!--  -->\n</div>",
            _abbreviationService.ExpandAbbreviation("div>p+{<!-- ${0} -->}")
        );

        Assert.Equal(
            "<div>\n\t<!-- <p></p> -->\n</div>",
            _abbreviationService.ExpandAbbreviation("div>{<!-- ${0} -->}>p")
        );

        Assert.Equal(
            "<div>\n\t<!-- <p></p> -->\n\t<!-- <p></p> -->\n</div>",
            _abbreviationService.ExpandAbbreviation("div>{<!-- ${0} -->}*2>p")
        );

        Assert.Equal(
            "<div>\n\t<!-- \n\t<p></p>\n\t<p></p>\n\t-->\n</div>",
            _abbreviationService.ExpandAbbreviation("div>{<!-- ${0} -->}>p*2")
        );

        Assert.Equal(
            "<div>\n\t<!-- \n\t<p></p>\n\t<p></p>\n\t-->\n\t<!-- \n\t<p></p>\n\t<p></p>\n\t-->\n</div>",
            _abbreviationService.ExpandAbbreviation("div>{<!-- ${0} -->}*2>p*2")
        );

        Assert.Equal(
            "<div>\n\t<!-- <b></b> -->\n</div>",
            _abbreviationService.ExpandAbbreviation("div>{<!-- ${0} -->}>b")
        );

        Assert.Equal(
            "<div>\n\t<!-- <b></b><b></b> -->\n</div>",
            _abbreviationService.ExpandAbbreviation("div>{<!-- ${0} -->}>b*2")
        );

        Assert.Equal(
            "<div>\n\t<!-- \n\t<b></b>\n\t<b></b>\n\t<b></b>\n\t-->\n</div>",
            _abbreviationService.ExpandAbbreviation("div>{<!-- ${0} -->}>b*3")
        );

        Assert.Equal(
            "<div><!--  --></div>",
            _abbreviationService.ExpandAbbreviation("div>{<!-- ${0} -->}")
        );

        Assert.Equal(
            "<div>\n\t<!-- <b></b> -->\n</div>",
            _abbreviationService.ExpandAbbreviation("div>{<!-- ${0} -->}>b")
        );
    }

    [Fact]
    public void Test_HTML_SelfClosing()
    {
        var xmlStyle = new UserConfig
        {
            Options = new AbbreviationOptions
            {
                OutputSelfClosingStyle = SelfClosingStyle.Xml
            }
        };

        var xhtmlStyle = new UserConfig
        {
            Options = new AbbreviationOptions
            {
                OutputSelfClosingStyle = SelfClosingStyle.Xhtml
            }
        };

        var htmlStyle = new UserConfig
        {
            Options = new AbbreviationOptions
            {
                OutputSelfClosingStyle = SelfClosingStyle.Html
            }
        };

        Assert.Equal(
            "<img src=\"\" alt=\"\">",
            _abbreviationService.ExpandAbbreviation("img[src]/", htmlStyle)
        );

        Assert.Equal(
            "<img src=\"\" alt=\"\" />",
            _abbreviationService.ExpandAbbreviation("img[src]/", xhtmlStyle)
        );

        Assert.Equal(
            "<img src=\"\" alt=\"\"/>",
            _abbreviationService.ExpandAbbreviation("img[src]/", xmlStyle)
        );

        Assert.Equal(
            "<div><img src=\"\" alt=\"\" /></div>",
            _abbreviationService.ExpandAbbreviation("div>img[src]/", xhtmlStyle)
        );
    }

    [Fact]
    public void Test_HTML_BooleanAttributes()
    {
        var compact = new UserConfig
        {
            Options = new AbbreviationOptions
            {
                OutputCompactBoolean = true
            }
        };

        var notCompact = new UserConfig
        {
            Options = new AbbreviationOptions
            {
                OutputCompactBoolean = false
            }
        };

        Assert.Equal(
            "<p b=\"b\"></p>",
            _abbreviationService.ExpandAbbreviation("p[b.]", notCompact)
        );

        Assert.Equal(
            "<p b></p>",
            _abbreviationService.ExpandAbbreviation("p[b.]", compact)
        );

        Assert.Equal(
            "<p contenteditable></p>",
            _abbreviationService.ExpandAbbreviation("p[contenteditable]", compact)
        );

        Assert.Equal(
            "<p contenteditable=\"contenteditable\"></p>",
            _abbreviationService.ExpandAbbreviation("p[contenteditable]", notCompact)
        );

        Assert.Equal(
            "<p contenteditable=\"foo\"></p>",
            _abbreviationService.ExpandAbbreviation("p[contenteditable=foo]", compact)
        );
    }

    [Fact]
    public void Test_HTML_NoFormatting()
    {
        var config = new UserConfig
        {
            Options = new AbbreviationOptions
            {
                OutputFormat = false
            }
        };

        Assert.Equal(
            "<div><p></p></div>",
            _abbreviationService.ExpandAbbreviation("div>p", config)
        );

        Assert.Equal(
            "<div>foo<p></p>bar</div>",
            _abbreviationService.ExpandAbbreviation("div>{foo}+p+{bar}", config)
        );

        Assert.Equal(
            "<div>foo<p></p></div>",
            _abbreviationService.ExpandAbbreviation("div>{foo}>p", config)
        );

        Assert.Equal(
            "<div><!-- <p></p> --></div>",
            _abbreviationService.ExpandAbbreviation("div>{<!-- ${0} -->}>p", config)
        );
    }

    [Fact]
    public void Test_HTML_FormatSpecificNodes()
    {
        Assert.Equal(
            "<!DOCTYPE html>\n<html>\n<head>\n\t<meta charset=\"UTF-8\">\n\t<title>Document</title>\n</head>\n<body>\n\t\n</body>\n</html>",
            _abbreviationService.ExpandAbbreviation("{<!DOCTYPE html>}+html>(head>meta[charset=${charset}]/+title{${1:Document}})+body")
        );
    }

    [Fact]
    public void Test_HTML_Comment()
    {
        var config = new UserConfig
        {
            Options = new AbbreviationOptions
            {
                CommentEnabled = true
            }
        };

        Assert.Equal(
            "<ul>\n\t<li class=\"item\"></li>\n\t<!-- /.item -->\n</ul>",
            _abbreviationService.ExpandAbbreviation("ul>li.item", config)
        );

        Assert.Equal(
            "<div>\n\t<ul>\n\t\t<li class=\"item\" id=\"foo\"></li>\n\t\t<!-- /#foo.item -->\n\t</ul>\n</div>",
            _abbreviationService.ExpandAbbreviation("div>ul>li.item#foo", config)
        );

        config.Options.CommentAfter = " { [%ID] } ";

        Assert.Equal(
            "<div>\n\t<ul>\n\t\t<li class=\"item\" id=\"foo\"></li> { %foo } \n\t</ul>\n</div>",
            _abbreviationService.ExpandAbbreviation("div>ul>li.item#foo", config)
        );
    }

    #endregion

    #region HAML Tests

    [Fact]
    public void Test_HAML_Basic()
    {
        Assert.Equal(
            "#header\n\t%ul.nav\n\t\t%li.nav-item(title=\"test\") \n\t\t%li.nav-item(title=\"test\") ",
            _abbreviationService.ExpandAbbreviation("div#header>ul.nav>li[title=test].nav-item*2", _hamlConfig)
        );

        Assert.Equal(
            "%li\n\t%a(href=\"\") ",
            _abbreviationService.ExpandAbbreviation("li>a", _hamlConfig)
        );

        Assert.Equal(
            "#foo.bar(data-n1=\"v1\" title=\"test\" data-n2=\"v2\") ",
            _abbreviationService.ExpandAbbreviation("div#foo[data-n1=v1 title=test data-n2=v2].bar", _hamlConfig)
        );

        var compactProfile = new UserConfig
        {
            Syntax = "haml",
            Options = new AbbreviationOptions
            {
                OutputCompactBoolean = true
            }
        };
        Assert.Equal(
            "%input(type=\"text\" disabled foo=\"\" title=\"test\")/",
            _abbreviationService.ExpandAbbreviation("input[disabled. foo title=test]/", compactProfile)
        );

        var notCompactProfile = new UserConfig
        {
            Syntax = "haml",
            Options = new AbbreviationOptions
            {
                OutputCompactBoolean = false
            }
        };
        Assert.Equal(
            "%input(type=\"text\" disabled=true foo=\"\" title=\"test\")/",
            _abbreviationService.ExpandAbbreviation("input[disabled. foo title=test]/", notCompactProfile)
        );
    }

    [Fact]
    public void Test_HAML_NodesWithText()
    {
        Assert.Equal(
            "Text 1",
            _abbreviationService.ExpandAbbreviation("{Text 1}", _hamlConfig)
        );

        Assert.Equal(
            "%span Text 1",
            _abbreviationService.ExpandAbbreviation("span{Text 1}", _hamlConfig)
        );

        Assert.Equal(
            "%span Text 1\n\t%b Text 2",
            _abbreviationService.ExpandAbbreviation("span{Text 1}>b{Text 2}", _hamlConfig)
        );

        Assert.Equal(
            "%span\n\tText 1 |\n\tText 2 |\n\t%b Text 3",
            _abbreviationService.ExpandAbbreviation("span{Text 1\nText 2}>b{Text 3}", _hamlConfig)
        );


        Assert.Equal(
            "%div\n\t%span\n\t\tText 1 |\n\t\tText 2 |\n\t\t%b Text 3",
            _abbreviationService.ExpandAbbreviation("div>span{Text 1\nText 2}>b{Text 3}", _hamlConfig)
        );
    }

    [Fact]
    public void Test_HAML_GenerateFields()
    {
        Assert.Equal(
            "%a(href=\"\") ",
            _abbreviationService.ExpandAbbreviation("a[href]", _hamlConfig)
        );

        Assert.Equal(
            "%a(href=\"\") \n%a(href=\"\") ",
            _abbreviationService.ExpandAbbreviation("a[href]*2", _hamlConfig)
        );

        Assert.Equal(
            " foo bar foo bar",
            _abbreviationService.ExpandAbbreviation("{${0} ${1:foo} ${2:bar}}*2", _hamlConfig)
        );

        Assert.Equal(
            " foo bar foo bar",
            _abbreviationService.ExpandAbbreviation("{${0} ${1:foo} ${2:bar}}*2", _hamlConfig)
        );


        Assert.Equal(
            "%ul\n\t%li \n\t%li ",
            _abbreviationService.ExpandAbbreviation("ul>li*2", _hamlConfig)
        );

        Assert.Equal(
            "%div\n\t%img(src=\"\" alt=\"\")/",
            _abbreviationService.ExpandAbbreviation("div>img[src]/", _hamlConfig)
        );
    }

    #endregion

    #region PUG Tests

    [Fact]
    public void Test_PUG_Basic()
    {
        Assert.Equal(
            "#header\n\tul.nav\n\t\tli.nav-item(title=\"test\") \n\t\tli.nav-item(title=\"test\") ",
            _abbreviationService.ExpandAbbreviation("div#header>ul.nav>li[title=test].nav-item*2", _pugConfig)
        );

        Assert.Equal(
            "#foo.bar(data-n1=\"v1\", title=\"test\", data-n2=\"v2\") ",
            _abbreviationService.ExpandAbbreviation("div#foo[data-n1=v1 title=test data-n2=v2].bar", _pugConfig)
        );

        Assert.Equal(
            "input(type=\"text\", disabled, foo=\"\", title=\"test\")",
            _abbreviationService.ExpandAbbreviation("input[disabled. foo title=test]", _pugConfig)
        );

        Assert.Equal(
            "input(type=\"text\", disabled, foo=\"\", title=\"test\")/",
            _abbreviationService.ExpandAbbreviation("input[disabled. foo title=test]", new UserConfig { Syntax = "pug", Options = new AbbreviationOptions { OutputSelfClosingStyle = SelfClosingStyle.Xml } })
        );
    }

    [Fact]
    public void Test_PUG_NodesWithText()
    {
        Assert.Equal(
            "Text 1",
            _abbreviationService.ExpandAbbreviation("{Text 1}", _pugConfig)
        );

        Assert.Equal(
            "span Text 1",
            _abbreviationService.ExpandAbbreviation("span{Text 1}", _pugConfig)
        );

        Assert.Equal(
            "span Text 1\n\tb Text 2",
            _abbreviationService.ExpandAbbreviation("span{Text 1}>b{Text 2}", _pugConfig)
        );

        Assert.Equal(
            "span\n\t| Text 1\n\t| Text 2\n\tb Text 3",
            _abbreviationService.ExpandAbbreviation("span{Text 1\nText 2}>b{Text 3}", _pugConfig)
        );


        Assert.Equal(
            "div\n\tspan\n\t\t| Text 1\n\t\t| Text 2\n\t\tb Text 3",
            _abbreviationService.ExpandAbbreviation("div>span{Text 1\nText 2}>b{Text 3}", _pugConfig)
        );
    }

    [Fact]
    public void Test_PUG_GenerateFields()
    {
        Assert.Equal(
            "a(href=\"\") ",
            _abbreviationService.ExpandAbbreviation("a[href]", _pugConfig)
        );

        Assert.Equal(
            "a(href=\"\") \na(href=\"\") ",
            _abbreviationService.ExpandAbbreviation("a[href]*2", _pugConfig)
        );

        Assert.Equal(
            " foo bar foo bar",
            _abbreviationService.ExpandAbbreviation("{${0} ${1:foo} ${2:bar}}*2", _pugConfig)
        );

        Assert.Equal(
            " foo bar foo bar",
            _abbreviationService.ExpandAbbreviation("{${0} ${1:foo} ${2:bar}}*2", _pugConfig)
        );


        Assert.Equal(
            "ul\n\tli \n\tli ",
            _abbreviationService.ExpandAbbreviation("ul>li*2", _pugConfig)
        );

        Assert.Equal(
            "div\n\timg(src=\"\", alt=\"\")",
            _abbreviationService.ExpandAbbreviation("div>img[src]/", _pugConfig)
        );
    }

    #endregion

    #region Slim Tests

    [Fact]
    public void Test_Slim_Basic()
    {
        Assert.Equal(
            "#header\n\tul.nav\n\t\tli.nav-item title=\"test\" \n\t\tli.nav-item title=\"test\" ",
            _abbreviationService.ExpandAbbreviation("div#header>ul.nav>li[title=test].nav-item*2", _slimConfig)
        );

        Assert.Equal(
            "#foo.bar data-n1=\"v1\" title=\"test\" data-n2=\"v2\" ",
            _abbreviationService.ExpandAbbreviation("div#foo[data-n1=v1 title=test data-n2=v2].bar", _slimConfig)
        );
    }

    [Fact]
    public void Test_Slim_NodesWithText()
    {
        Assert.Equal(
            "Text 1",
            _abbreviationService.ExpandAbbreviation("{Text 1}", _slimConfig)
        );

        Assert.Equal(
            "span Text 1",
            _abbreviationService.ExpandAbbreviation("span{Text 1}", _slimConfig)
        );

        Assert.Equal(
            "span Text 1\n\tb Text 2",
            _abbreviationService.ExpandAbbreviation("span{Text 1}>b{Text 2}", _slimConfig)
        );

        Assert.Equal(
            "span\n\t| Text 1\n\t| Text 2\n\tb Text 3",
            _abbreviationService.ExpandAbbreviation("span{Text 1\nText 2}>b{Text 3}", _slimConfig)
        );


        Assert.Equal(
            "div\n\tspan\n\t\t| Text 1\n\t\t| Text 2\n\t\tb Text 3",
            _abbreviationService.ExpandAbbreviation("div>span{Text 1\nText 2}>b{Text 3}", _slimConfig)
        );
    }

    [Fact]
    public void Test_Slim_GenerateFields()
    {
        Assert.Equal(
            "a href=\"\" ",
            _abbreviationService.ExpandAbbreviation("a[href]", _slimConfig)
        );

        Assert.Equal(
            "a href=\"\" \na href=\"\" ",
            _abbreviationService.ExpandAbbreviation("a[href]*2", _slimConfig)
        );

        Assert.Equal(
            " foo bar foo bar",
            _abbreviationService.ExpandAbbreviation("{${0} ${1:foo} ${2:bar}}*2", _slimConfig)
        );

        Assert.Equal(
            " foo bar foo bar",
            _abbreviationService.ExpandAbbreviation("{${0} ${1:foo} ${2:bar}}*2", _slimConfig)
        );


        Assert.Equal(
            "ul\n\tli \n\tli ",
            _abbreviationService.ExpandAbbreviation("ul>li*2", _slimConfig)
        );

        Assert.Equal(
            "div\n\timg src=\"\" alt=\"\"/",
            _abbreviationService.ExpandAbbreviation("div>img[src]/", _slimConfig)
        );
    }

    #endregion
}
