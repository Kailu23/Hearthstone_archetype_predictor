using Hearthstone_Archetype_Predictor.Models;
using Hearthstone_Archetype_Predictor.Tests.Helpers;
using Hearthstone_Archetype_Predictor.Tests.PageObjects;
using Xunit;

namespace Hearthstone_Archetype_Predictor.Tests;

/// <summary>
/// Unit tests for HearthstoneSerializer — using the POM (SerializerPage)
/// and WaitHelper for asynchronous state verification.
/// </summary>
public class HearthstoneSerializerTests
{
    private const string DeckStringPreSideboards =
        "AAECAQcCrwSRvAIOHLACkQP/A44FqAXUBaQG7gbnB+8HgrACiLACub8CAAA=";

    private const string ClassicDeckstring =
        "AAEDAaa4AwTTlQSvlgT6oASPowQN25UE3JUEppYEsJYEtpYEvZYE1JYE3ZYE6aEE8KEE8aEE86EE1KIEAA==";

    private const string SideboardedDeckstring =
        "AAECAbSKAwrbnwSboASjoAS4oASD1ASY1ASZ1ASh1ASxmAX9xAUKh58EiZ8Erp8E2Z8E2p8EuaAE3KAEgdQEo9QEjaQFAAEB8L8E/cQFAdqhBf3EBQA=";

    [Fact]
    public void IsValidDeckString_EmptyString_ReturnsFalse()
    {
        //Given
        var page = new SerializerPage();
        //When
        bool result = page.SubmitDeckString(string.Empty);
        //Then
        Assert.False(result);
    }

    [Fact]
    public void IsValidDeckString_NullString_ReturnsFalse()
    {
        //Given
        var page = new SerializerPage();
        //When
        bool result = page.SubmitDeckString(null!);
        //Then
        Assert.False(result);
    }

    [Fact]
    public void IsValidDeckString_WhitespaceOnly_ReturnsFalse()
    {
        //Given
        var page = new SerializerPage();
        //When
        bool result = page.SubmitDeckString("    ");
        //Then
        Assert.False(result);
    }

    [Fact]
    public void IsValidDeckString_RandomGarbage_ReturnFalse()
    {
        //Given
        var page = new SerializerPage();
        const string Garbage = "garbege to test";
        //When
        bool result = page.SubmitDeckString(Garbage);
        //Then
        Assert.False(result);
    }

    [Fact]
    public void IsValidDeckString_PartialBase64_ReturnFalse()
    {
        //Given
        var page = new SerializerPage();
        const string PartialBase64 = "SGVsbG8gV29ybGQ";
        //When
        bool result = page.SubmitDeckString(PartialBase64);
        //Then
        Assert.False(result);
    }

    [Fact]
    public void IsValidDeckString_DeckstringPreSideboards_ReturnsTrue()
    {
        //Given
        var page = new SerializerPage();
        //When
        bool actual = page.SubmitDeckString(DeckStringPreSideboards);
        //Then
        Assert.True(actual);
    }

    [Fact]
    public void IsValidDeckString_ClassicDeckstring_ReturnsTrue()
    {
        //Given
        var page = new SerializerPage();
        //When
        bool actual = page.SubmitDeckString(ClassicDeckstring);
        //Then
        Assert.True(actual);
    }

    [Fact]
    public void IsValidDeckString_SideboradedDeckstring_ReturnsTrue()
    {
        //Given
        var page = new SerializerPage();
        //When
        bool actual = page.SubmitDeckString(SideboardedDeckstring);
        //Then
        Assert.True(actual);
    }

    [Fact]
    public void DeckString_DefaultConstructor_UsesDefaultDeckstring()
    {
        //Given
        const string _DefaultDeckstring =
            "AAECAQcCrwSRvAIOHLACkQP/A44FqAXUBaQG7gbnB+8HgrACiLACub8CAAA=";
        var page = new SerializerPage();
        var pageWithDefaultDeckstring = new SerializerPage(_DefaultDeckstring);
        //When
        string expected = pageWithDefaultDeckstring.CurrentDeckString;
        string actual = page.CurrentDeckString;
        //Then
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DeckString_ChangeDeckstringValue_ReturnsUpdatedDeckstring()
    {
        //Given
        var page = new SerializerPage();
        string expected = ClassicDeckstring;
        //When
        page.SubmitDeckString(expected);
        //Then
        Assert.Equal(expected, page.CurrentDeckString);
    }

    [Fact]
    public void DeckString_CustomConstructoString_ReturnsSameString()
    {
        //Given
        var page = new SerializerPage(DeckStringPreSideboards);
        //When
        string actual = page.CurrentDeckString;
        //Then
        Assert.Equal(DeckStringPreSideboards, actual);
    }

    [Fact]
    public void Cards_BeforeDeserialization_ReturnEmpty()
    {
        //Given
        var page = new SerializerPage();
        //When
        var value = page.Cards;
        //Then
        Assert.Empty(value);
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    [InlineData("INVALID")]
    [InlineData("21312789y321")]
    public void IsValidDeckString_MultipleInvalidInputs_AllReturnFalse(string input)
    {
        //Given
        var page = new SerializerPage();
        //When
        bool result = page.SubmitDeckString(input);
        //Then
        Assert.False(result);
    }

    [Fact]
    public async Task ImplicitWait_DoesNotThrow_CompletesInTime()
    {
        // Given
        var before = DateTime.UtcNow;
        // When
        await WaitHelper.ImplicitWait(300);
        var elapsed = DateTime.UtcNow - before;
        // Then
        Assert.True(
            elapsed.TotalMilliseconds >= 280,
            $"ImplicitWait was supposed to last >= 280ms, but it lasted {elapsed.TotalMilliseconds}ms."
        );
    }

    [Fact]
    public async Task ExplicitWait_ConditionMet_DoesNotThrow()
    {
        //Given
        bool ready = false;
        var setTask = Task.Run(async () =>
        {
            await Task.Delay(150);
            ready = true;
        });
        //When
        await WaitHelper.WaitUntil(condition: () => ready, timeout: TimeSpan.FromSeconds(2));
        //Then
        Assert.True(ready);
    }

    [Fact]
    public async Task ExplicitWait_ConditionNeverMet_ThrowsTimeoutException()
    {
        //Given
        var timeout = TimeSpan.FromMilliseconds(300);
        //When
        var exception = () =>
            WaitHelper.WaitUntil(
                condition: () => false,
                timeout: timeout,
                timeoutMessage: "Test timeout"
            );
        //Then
        await Assert.ThrowsAsync<TimeoutException>(exception);
    }
}
