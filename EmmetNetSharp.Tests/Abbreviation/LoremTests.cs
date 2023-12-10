using EmmetNetSharp.Models;

namespace EmmetNetSharp.Tests.Abbreviation;

/// <summary>
/// Represents a class that contains tests for lorem ipsum functionalities.
/// </summary>
public class LoremTests
{
    private readonly IAbbreviationService _abbreviationService = new AbbreviationService();

    [Fact]
    public void Test_Single()
    {
        var output1 = _abbreviationService.ExpandAbbreviation("lorem");
        var output1Words = output1.Split(new char[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;
        Assert.Matches(@"^Lorem,?\sipsum", output1);
        Assert.True(output1Words > 20);

        var output2 = _abbreviationService.ExpandAbbreviation("lorem5");
        var output2Words = output2.Split(new char[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;
        Assert.Matches(@"^Lorem,?\sipsum", output2);
        Assert.Equal(5, output2Words);

        var output3 = _abbreviationService.ExpandAbbreviation("lorem5-10");
        var output3Words = output3.Split(new char[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;
        Assert.Matches(@"^Lorem,?\sipsum", output3);
        Assert.True(output3Words >= 5 && output3Words <= 10);

        var output4 = _abbreviationService.ExpandAbbreviation("loremru4");
        var output4Words = output4.Split(new char[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;
        Assert.Matches(@"^Далеко-далеко,?\sза,?\sсловесными", output4);
        Assert.Equal(4, output4Words);

        var output5 = _abbreviationService.ExpandAbbreviation("p>lorem");
        Assert.Matches(@"^<p>Lorem,?\sipsum", output5);

        var output6 = _abbreviationService.ExpandAbbreviation("(p)lorem2");
        Assert.Matches(@"^<p><\/p>\nLorem,?\sipsum", output6);

        var output7 = _abbreviationService.ExpandAbbreviation("p(lorem10)");
        Assert.Matches(@"^<p><\/p>\nLorem,?\sipsum", output7);
    }

    [Fact]
    public void Test_Multiple()
    {
        var output1 = _abbreviationService.ExpandAbbreviation("lorem6*3");
        var output1Lines = output1.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;
        Assert.Matches(@"^Lorem,?\sipsum", output1);
        Assert.Equal(3, output1Lines);

        var output2 = _abbreviationService.ExpandAbbreviation("lorem6*2");
        var output2Lines = output2.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;
        Assert.Matches(@"^Lorem,?\sipsum", output2);
        Assert.Equal(2, output2Lines);

        var output3 = _abbreviationService.ExpandAbbreviation("p*3>lorem");
        var output3Lines = output3.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
        Assert.Matches(@"^<p>Lorem,?\sipsum", output3Lines[0]);

        var output4 = _abbreviationService.ExpandAbbreviation("ul>lorem5*3", new UserConfig { Options = new AbbreviationOptions { OutputIndent = "" } });
        var output4Lines = output4.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
        Assert.Equal(5, output4Lines.Length);
        Assert.Matches(@"^^<li>Lorem,?\sipsum", output4Lines[1]);
    }
}
