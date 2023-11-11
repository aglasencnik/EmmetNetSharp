﻿namespace EmmetNetSharp.Tests;

/// <summary>
/// Represents a class that contains tests for snippet functionalities.
/// </summary>
internal class SnippetsTests
{
    #region Fields

    private readonly ITestOutputHelper _output;

    private readonly IAbbreviationService _abbreviationService;

    #endregion

    #region Ctor

    public SnippetsTests(ITestOutputHelper output)
    {
        _output = output;
        _abbreviationService = new AbbreviationService();
    }

    #endregion

    #region Methods



    #endregion
}
