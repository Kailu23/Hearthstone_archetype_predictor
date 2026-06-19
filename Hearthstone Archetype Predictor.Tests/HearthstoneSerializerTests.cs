using Hearthstone_Archetype_Predictor.Models;
using Xunit;

namespace Hearthstone_Archetype_Predictor.Tests;

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
        var serializer = new HearthstoneSerializer();
        //When
        bool result = serializer.IsValidDeckString(string.Empty);
        //Then
        Assert.False(result);
    }

    [Fact]
    public void IsValidDeckString_NullString_ReturnsFalse()
    {
        //Given
        var serializer = new HearthstoneSerializer();
        //When
        bool result = serializer.IsValidDeckString(null!);
        //Then
        Assert.False(result);
    }

    [Fact]
    public void IsValidDeckString_WhitespaceOnly_ReturnsFalse()
    {
        //Given
        var serializer = new HearthstoneSerializer();
        //When
        bool result = serializer.IsValidDeckString("     ");
        //Then
        Assert.False(result);
    }

    [Fact]
    public void IsValidDeckString_RandomGarbage_ReturnFalse()
    {
        //Given
        var serializer = new HearthstoneSerializer();
        const string Garbage = "garbege to test";
        //When
        bool result = serializer.IsValidDeckString(Garbage);
        //Then
        Assert.False(result);
    }

    [Fact]
    public void IsValidDeckString_PartialBase64_ReturnFalse()
    {
        //Given
        var serializer = new HearthstoneSerializer();
        const string PartialBase64 = "SGVsbG8gV29ybGQ";
        //When
        bool result = serializer.IsValidDeckString(PartialBase64);
        //Then
        Assert.False(result);
    }

    [Fact]
    public void IsValidDeckString_DeckstringPreSideboards_ReturnsTrue()
    {
        //Given
        var serializer = new HearthstoneSerializer();
        //When
        bool actual = serializer.IsValidDeckString(DeckStringPreSideboards);
        //Then
        Assert.True(actual);
    }

    [Fact]
    public void IsValidDeckString_ClassicDeckstring_ReturnsTrue()
    {
        //Given
        var serializer = new HearthstoneSerializer();
        //When
        bool actual = serializer.IsValidDeckString(ClassicDeckstring);
        //Then
        Assert.True(actual);
    }

    [Fact]
    public void IsValidDeckString_SideboradedDeckstring_ReturnsTrue()
    {
        //Given
        var serializer = new HearthstoneSerializer();
        //When
        bool actual = serializer.IsValidDeckString(SideboardedDeckstring);
        //Then
        Assert.True(actual);
    }

    [Fact]
    public void DeckString_DefaultConstructor_UsesDefaultDeckstring()
    {
        //Given
        const string _DefaultDeckstring =
            "AAECAQcCrwSRvAIOHLACkQP/A44FqAXUBaQG7gbnB+8HgrACiLACub8CAAA=";
        var serializer = new HearthstoneSerializer();
        var serializerWithDefaultDeckstring = new HearthstoneSerializer(_DefaultDeckstring);
        //When
        string expected = serializerWithDefaultDeckstring.DeckString;
        string actual = serializer.DeckString;
        //Then
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DeckString_ChangeDeckstringValue_ReturnsUpdatedDeckstring()
    {
        //Given
        var serializer = new HearthstoneSerializer();
        string expected = ClassicDeckstring;
        //When
        serializer.DeckString = expected;
        //Then
        Assert.Equal(expected, serializer.DeckString);
    }

    [Fact]
    public void DeckString_CustomConstructoString_ReturnsSameString()
    {
        //Given
        var serializer = new HearthstoneSerializer(DeckStringPreSideboards);
        //When
        string actual = serializer.DeckString;
        //Then
        Assert.Equal(DeckStringPreSideboards, actual);
    }

    [Fact]
    public void Cards_BeforeDeserialization_ReturnEmpty()
    {
        //Given
        var serializer = new HearthstoneSerializer();
        //When
        var value = serializer.Cards;
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
        var serializer = new HearthstoneSerializer();
        //When
        bool result = serializer.IsValidDeckString(input);
        //Then
        Assert.False(result);
    }
}
