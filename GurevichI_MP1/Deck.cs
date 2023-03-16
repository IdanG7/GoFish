// Author: Idan Gurevich
// File Name: Deck.cs
// Project Name: GurevichI_MP1
// Creation Date: 2023-03-08
// Modified Date: 2023-03-15
// Description: The Deck class represents a deck of playing cards in a card game. It provides methods for shuffling the deck, dealing cards to players,
// and checking if the deck is empty. The class also includes a collection of Card objects that make up the deck.

using System;
using System.Collections.Generic;

namespace GurevichI_MP1
{
    class Deck
    {
        public const int DECK_SIZE = 52;

        private List<Card> cards;
        private Random rng;

        public Deck()
        {
            cards = new List<Card>();
            rng = new Random();

            ResetDeck();
        }

        public void ResetDeck()
        {
            cards.Clear();

            for (int i = 0; i < DECK_SIZE; i++)
            {
                cards.Add(new Card(i));
            }

            ShuffleDeck();
        }

        private void ShuffleDeck()
        {
            for (int i = 0; i < cards.Count - 1; i++)
            {
                int j = rng.Next(i, cards.Count);
                Card temp = cards[i];
                cards[i] = cards[j];
                cards[j] = temp;
            }
        }

        public Card DrawCard()
        {
            Card temp = cards[0];
            cards.RemoveAt(0);

            return temp;
        }

        public bool IsEmpty()
        {
            return cards.Count == 0;
        }

        public int GetSize()
        {
            return cards.Count;

        }


    }
}