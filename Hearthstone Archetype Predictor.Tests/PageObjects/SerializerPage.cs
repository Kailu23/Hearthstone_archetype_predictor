using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Hearthstone_Archetype_Predictor.Models;
using Hearthstone_Archetype_Predictor.Tests.Helpers;

namespace Hearthstone_Archetype_Predictor.Tests.PageObjects;

/// <summary>
/// Page Object for HearthstoneSerializer — encapsulates access to the model.
///
/// In a Selenium POM you might have something like a "DeckInputPage" that knows where the input field is.
/// Here, "SerializerPage" knows how to communicate with the HearthstoneSerializer class.
/// </summary>
public class SerializerPage
{
    private readonly HearthstoneSerializer _serializer;

    public SerializerPage(string? initialDeck = null)
    {
        _serializer = initialDeck is not null
            ? new HearthstoneSerializer(initialDeck)
            : new HearthstoneSerializer();
    }

    public string CurrentDeckString => _serializer.DeckString;

    public Dictionary<HearthDb.Card, int> Cards => new(_serializer.Cards);

    public bool AreCardsLoaded => _serializer.Cards.Count > 0;

    public bool SubmitDeckString(string deckString)
    {
        if (_serializer.IsValidDeckString(deckString) is false)
            return false;

        _serializer.DeckString = deckString;
        return true;
    }

    /// <summary>
    /// Implicit wait — a pause before checking (simulates network latency).
    /// </summary>
    public Task WaitForNetworkDelay(int milliseconds = 200) =>
        WaitHelper.ImplicitWait(milliseconds);

    /// <summary>
    /// Waits until Cards is populated (after a GetImagesAsync call).
    /// </summary>
    public Task WaitUntilCardsAreLoaded(TimeSpan? timeout = null) =>
        WaitHelper.WaitUntil(
            condition: () => AreCardsLoaded,
            timeout: timeout ?? TimeSpan.FromSeconds(15),
            timeoutMessage: "Cards weren't loaded within specified time."
        );
}
