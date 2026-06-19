using Hearthstone_Archetype_Predictor.Models;
using Xunit;

namespace Hearthstone_Archetype_Predictor.Tests;

public class HearthstoneSerializerIntegrationTests
{
    private const string WarriorDeckstring = "AAECAQcCrwSRvAIOHLACkQP/A44FqAXUBaQG7gbnB+8HgrACiLACub8CAAA=";

    private const string DruidDeckstring = "AAECAbSKAwrbnwSboASjoAS4oASD1ASY1ASZ1ASh1ASxmAX9xAUKh58EiZ8Erp8E2Z8E2p8EuaAE3KAEgdQEo9QEjaQFAAEB8L8E/cQFAdqhBf3EBQA=";

    [Fact]
    public async Task IsValidDeckstring_WarriorDeckstring_PassesDeserialization()
    {
    //Given
        var serializer = new HearthstoneSerializer(WarriorDeckstring);
    //When
        bool result  = serializer.IsValidDeckString(WarriorDeckstring);
    //Then
        Assert.True(result, "Warrior deck isn't valid");
    }

    [Fact]
    public void IsValidDeckString_ChangingDeckstring_BothPassValidation()
    {
        var serializer = new HearthstoneSerializer(WarriorDeckstring);
    //When
        bool result  = serializer.IsValidDeckString(WarriorDeckstring);
    //Then
        Assert.True(result, "Warrior deck isn't valid");

        serializer.DeckString = DruidDeckstring;

        result  = serializer.IsValidDeckString(DruidDeckstring);

        Assert.True(result, "Druid deck isn't valid");
    }

    [Fact]
    public void IsValidDeckString_InvalidDeckstring_FailsDeserialization()
    {
    //Given
        var serializer = new HearthstoneSerializer();
        const string InvalidDeckstring = "AAECAQcCrwSRvAIOHLACkQP/A44FqAXUBaQG7gbnB+8HgrACiLACub8CAAA=ddas";
    //When
        bool result = serializer.IsValidDeckString(InvalidDeckstring);
    //Then
        Assert.False(result, "Invalid deck string should have failed deserialization.");
    }
}
