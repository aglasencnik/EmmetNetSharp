namespace EmmetNetSharp.Tests;

/// <summary>
/// Represents a class that contains tests for formatter functionalities.
/// </summary>
public class FormatTests
{
    private readonly IAbbreviationService _abbreviationService = new AbbreviationService();

    #region HTML Tests

    [Fact]
    public void Test_HTML_Basic()
    {
        
    }

    [Fact]
    public void Test_HTML_InlineElements()
    {

    }

    [Fact]
    public void Test_HTML_GenerateFields()
    {

    }

    [Fact]
    public void Test_HTML_MixedContent()
    {

    }

    [Fact]
    public void Test_HTML_SelfClosing()
    {

    }

    [Fact]
    public void Test_HTML_BooleanAttributes()
    {

    }

    [Fact]
    public void Test_HTML_NoFormatting()
    {

    }

    [Fact]
    public void Test_HTML_FormatSpecificNodes()
    {

    }

    [Fact]
    public void Test_HTML_Comment()
    {

    }

    #endregion

    #region HAML Tests

    [Fact]
    public void Test_HAML_Basic()
    {

    }

    [Fact]
    public void Test_HAML_NodesWithText()
    {

    }

    [Fact]
    public void Test_HAML_GenerateFields()
    {

    }

    #endregion

    #region PUG Tests

    [Fact]
    public void Test_PUG_Basic()
    {

    }

    [Fact]
    public void Test_PUG_NodesWithText()
    {

    }

    [Fact]
    public void Test_PUG_GenerateFields()
    {

    }

    #endregion

    #region Slim Tests

    [Fact]
    public void Test_Slim_Basic()
    {

    }

    [Fact]
    public void Test_Slim_NodesWithText()
    {

    }

    [Fact]
    public void Test_Slim_GenerateFields()
    {

    }

    #endregion
}
