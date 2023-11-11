namespace EmmetNetSharp.Tests.Abbreviation;

/// <summary>
/// Represents a class that contains tests for tokenizer abbreviation functionality.
/// </summary>
internal class TokenizerTests
{
    #region Fields

    private readonly ITestOutputHelper _output;

    private readonly IAbbreviationService _abbreviationService;

    #endregion

    #region Ctor

    public TokenizerTests(ITestOutputHelper output)
    {
        _output = output;
        _abbreviationService = new AbbreviationService();
    }

    #endregion

    #region Methods



    #endregion
}
