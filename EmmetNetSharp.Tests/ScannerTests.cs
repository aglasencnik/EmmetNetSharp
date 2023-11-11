namespace EmmetNetSharp.Tests;

/// <summary>
/// Represents a class that contains tests for scanner functionalities.
/// </summary>
internal class ScannerTests
{
    #region Fields

    private readonly ITestOutputHelper _output;

    private readonly IScannerService _scannerService;

    #endregion

    #region Ctor

    public ScannerTests(ITestOutputHelper output)
    {
        _output = output;
        _scannerService = new ScannerService();
    }

    #endregion

    #region Methods



    #endregion
}
