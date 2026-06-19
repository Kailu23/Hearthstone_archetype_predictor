using Avalonia.Controls;
using Hearthstone_Archetype_Predictor.ViewModels;
using Xunit;

namespace Hearthstone_Archetype_Predictor.Tests;

public class MainWindowViewModelTests
{
    private const string ValidDeckstring =
        "AAECAQcCrwSRvAIOHLACkQP/A44FqAXUBaQG7gbnB+8HgrACiLACub8CAAA=";

    [Fact]
    public void Constructor_Images_InitiallyEmpty()
    {
    //Given
        var viewModel = new MainWindowViewModel();
    //When
        var images = viewModel.Images;
    //Then
        Assert.Empty(images);
    }

    [Fact]
    public void Constructor_CardNamesAndCopies_InitiallyEmpty()
    {
    //Given
        var viewModel = new MainWindowViewModel();
    //When
        var cardNamesAndCopies = viewModel.CardNamesAndCopies;
    //Then
        Assert.Empty(cardNamesAndCopies);
    }

    [Fact]
    public void Scale_WhenWindowSizeIsZero_ReturnsZero()
    {
    //Given
        var viewModel = new MainWindowViewModel();
        const double Expected = 0.0d;
    //When
        viewModel.WindowWidth = 0;
        viewModel.WindowHeight = 0;
    //Then
        Assert.Equal(Expected, viewModel.Scale);
    }

    [Fact]
    public void Scale_WhenWindowSizeIsBaseSize_ReturnsOne()
    {
    //Given
        var viewModel = new MainWindowViewModel();
        const double Expected = 1.0d;
    //When
        viewModel.WindowWidth = 900;
        viewModel.WindowHeight = 850;
        double actual = viewModel.Scale;
    //Then
        Assert.Equal(Expected, actual, precision: 5);
    }

    [Fact]
    public void Scale_WhenWindowSizeisDoubleBaseSize_ReturnsTwo()
    {
    //Given
        var viewModel = new MainWindowViewModel();
        const double Expected = 2.0d;
    //When
        viewModel.WindowWidth = 1800;
        viewModel.WindowHeight = 1700;
        double actual = viewModel.Scale;
    //Then
        Assert.Equal(Expected, actual, precision: 5);
    }

    [Fact]
    public void Scale_ConstrainedByNarrowerDimension_ReturnOneHalf()
    {

    //Given
        var viewModel = new MainWindowViewModel();
        const double Expected = 0.5d;
    //When
        viewModel.WindowWidth = 450;
        viewModel.WindowHeight = 850;
        double actual = viewModel.Scale;
    //Then
        Assert.Equal(Expected, actual, precision: 5);
    }

    [Fact]
    public void FontSizeTitle_AtScale_Is28()
    {
    //Given
        var viewModel = new MainWindowViewModel()    ;
        const double Expected = 28.0d;
    //When
        viewModel.WindowWidth = 900;
        viewModel.WindowHeight = 850;
        double actual = viewModel.FontSizeTitle;
    //Then
        Assert.Equal(Expected, actual);
    }

    [Fact]
    public void ImageWidth_AtScaleOne_Is315()
    {
    //Given
        var viewModel = new MainWindowViewModel();
        const double Expected = 315.0d;
    //When
        viewModel.WindowWidth = 900;
        viewModel.WindowHeight = 850;
        double actual = viewModel.ImageWidth;
    //Then
        Assert.Equal(Expected, actual, precision: 5);
    }

    [Fact]
    public void ImageHeight_AtScaleOne_Is512()
    {
    //Given
        var viewModel = new MainWindowViewModel();
        const double Expected = 512.0d;
    //When
        viewModel.WindowWidth = 900;
        viewModel.WindowHeight = 850;
        double actual = viewModel.ImageHeight;
    //Then
        Assert.Equal(Expected, actual, precision: 5);
    }

    [Fact]
    public async Task NewDeckstring_InvalidInput_ReturnsFalse()
    {
    //Given
        var viewModel = new MainWindowViewModel();
    //When
        bool result = await viewModel.NewDeckstring("notadeckstring");
    //Then
        Assert.False(result);
    }

    [Fact]
    public async Task NewDeckstring_EmptyString_ReturnsFalse()
    {
    //Given
        var viewModel = new MainWindowViewModel();
    //When
        bool result = await viewModel.NewDeckstring(string.Empty);
    //Then
        Assert.False(result);
    }
}
