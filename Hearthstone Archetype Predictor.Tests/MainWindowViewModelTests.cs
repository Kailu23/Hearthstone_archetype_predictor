using Avalonia.Controls;
using Hearthstone_Archetype_Predictor.Tests.Helpers;
using Hearthstone_Archetype_Predictor.Tests.PageObjects;
using Hearthstone_Archetype_Predictor.ViewModels;
using Xunit;

namespace Hearthstone_Archetype_Predictor.Tests;

/// <summary>
/// Unit test for MainWindowViewModel - they use POM (MainWindowPage).
/// </summary>
public class MainWindowViewModelTests
{
    private const string ValidDeckstring =
        "AAECAQcCrwSRvAIOHLACkQP/A44FqAXUBaQG7gbnB+8HgrACiLACub8CAAA=";

    [Fact]
    public void Constructor_Images_InitiallyEmpty()
    {
        //Given
        var page = new MainWindowPage();
        const int Expected = 0;
        //When
        var actual = page.ImageCount;
        //Then
        Assert.Equal(Expected, actual);
    }

    [Fact]
    public void Constructor_CardNamesAndCopies_InitiallyEmpty()
    {
        //Given
        var page = new MainWindowPage();
        //When
        var cardNamesAndCopies = page.CardNamesAndCopies;
        //Then
        Assert.Empty(cardNamesAndCopies);
    }

    [Fact]
    public void Scale_WhenWindowSizeIsZero_ReturnsZero()
    {
        //Given
        var page = new MainWindowPage(windowWidth: 0, windowHeight: 0);
        const double Expected = 0.0d;
        //When
        double actual = page.Scale;
        //Then
        Assert.Equal(Expected, actual, precision: 5);
    }

    [Fact]
    public void Scale_WhenWindowSizeIsBaseSize_ReturnsOne()
    {
        //Given
        var page = new MainWindowPage(windowWidth: 900, windowHeight: 850);
        const double Expected = 1.0d;
        //When
        double actual = page.Scale;
        //Then
        Assert.Equal(Expected, actual, precision: 5);
    }

    [Fact]
    public void Scale_WhenWindowSizeisDoubleBaseSize_ReturnsTwo()
    {
        //Given
        var page = new MainWindowPage(windowWidth: 1800, windowHeight: 1700);
        const double Expected = 2.0d;
        //When
        double actual = page.Scale;
        //Then
        Assert.Equal(Expected, actual, precision: 5);
    }

    [Fact]
    public void Scale_ConstrainedByNarrowerDimension_ReturnOneHalf()
    {
        //Given
        var page = new MainWindowPage(windowWidth: 450, windowHeight: 850);
        const double Expected = 0.5d;
        //When
        double actual = page.Scale;
        //Then
        Assert.Equal(Expected, actual, precision: 5);
    }

    [Fact]
    public void FontSizeTitle_AtScale_Is28()
    {
        //Given
        var page = new MainWindowPage(windowWidth: 900, windowHeight: 850);
        const double Expected = 28.0d;
        //When
        double actual = page.FontSizeTitle;
        //Then
        Assert.Equal(Expected, actual, precision: 5);
    }

    [Fact]
    public void ImageWidth_AtScaleOne_Is315()
    {
        //Given
        var page = new MainWindowPage(windowWidth: 900, windowHeight: 850);
        const double Expected = 315.0d;
        //When
        double actual = page.ImageWidth;
        //Then
        Assert.Equal(Expected, actual, precision: 5);
    }

    [Fact]
    public void ImageHeight_AtScaleOne_Is512()
    {
        //Given
        var page = new MainWindowPage(windowWidth: 900, windowHeight: 850);
        const double Expected = 512.0d;
        //When
        double actual = page.ImageHeight;
        //Then
        Assert.Equal(Expected, actual, precision: 5);
    }

    [Fact]
    public void ResizeWindow_ChangeDimensions_ScalUpdatesAccordingly()
    {
        //Given
        double windowWidth = 900, windowHeight = 850;
        var page = new MainWindowPage(windowWidth, windowHeight);
        const double Expected = 1.0d;
        //When
        double actual = page.Scale;
        //Then
        Assert.Equal(Expected, actual, precision: 5);

        //Given
        const double ExpectedAfterResize = 0.5d;
        //When
        page.ResizeWindow(windowWidth / 2, windowHeight);
        actual = page.Scale;
        //Then
        Assert.Equal(ExpectedAfterResize, actual, precision: 5);
    }

    [Fact]
    public async Task EnterDeckString_InvalidInput_ReturnsFalse()
    {
        //Given
        var page = new MainWindowPage();
        //When
        bool result = await page.EnterDeckstring("notadeckstring");
        //Then
        Assert.False(result);
    }

    [Fact]
    public async Task NewDeckstring_EmptyString_ReturnsFalse()
    {
        //Given
        var page = new MainWindowPage();
        //When
        bool result = await page.EnterDeckstring(string.Empty);
        //Then
        Assert.False(result);
    }

    [Fact]
    public async Task FluentWait_ConditionMet_ReturnsValue()
    {
    //Given
        string? value = null;
        const string Expected = "loaded";
        var setTask = Task.Run(async () => {
                await Task.Delay(200);
                value = Expected;
                });
    //When
        var result = await WaitHelper.FluentWait(supplier: () => value, until: v => v == Expected, timeout: TimeSpan.FromSeconds(2));
    //Then
        Assert.Equal(Expected, result);
    }
}
