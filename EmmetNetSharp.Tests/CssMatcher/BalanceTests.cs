namespace EmmetNetSharp.Tests.CssMatcher;

/// <summary>
/// Represents a class that contains tests for balance css matcher functionality.
/// </summary>
public class BalanceTests
{
    private readonly ICssMatcherService _cssMatcherService = new CssMatcherService();

    [Fact]
    public void Test_Outward()
    {
        var code = File.ReadAllText(Path.Combine("Assets", "cssMatcherSample.scss")).Replace("\r\n", "\n") ?? string.Empty;

        Assert.Equal(new[]
        {
            (145, 149),
            (137, 150),
            (110, 182),
            (75, 192),
            (61, 198),
            (43, 281),
            (0, 283)
        }, _cssMatcherService.BalanceOutward(code, 140));

        Assert.Equal(new[]
        {
            (110, 182),
            (75, 192),
            (61, 198),
            (43, 281),
            (0, 283)
        }, _cssMatcherService.BalanceOutward(code, 77));

        Assert.Equal(new[]
        {
            (273, 281),
            (43, 281),
            (0, 283)
        }, _cssMatcherService.BalanceOutward(code, 277));
    }

    [Fact]
    public void Test_Inward()
    {
        var code = File.ReadAllText(Path.Combine("Assets", "cssMatcherSample.scss")).Replace("\r\n", "\n") ?? string.Empty;

        Assert.Equal(new[]
        {
            (61, 198),
            (75, 192),
            (110, 182),
            (110, 124),
            (119, 123)
        }, _cssMatcherService.BalanceInward(code, 62));

        Assert.Equal(new[]
        {
            (43, 56),
            (51, 55)
        }, _cssMatcherService.BalanceInward(code, 46));

        Assert.Equal(new[]
        {
            (204, 267),
            (218, 261),
            (218, 236),
            (231, 235)
        }, _cssMatcherService.BalanceInward(code, 206));

        Assert.Equal(new[]
        {
            (330, 336),
            (334, 335)
        }, _cssMatcherService.BalanceInward(code, 333));
    }
}
