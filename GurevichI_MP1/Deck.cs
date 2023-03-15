// Author: Idan Gurevich
// File Name: Deck.cs
// Project Name: GurevichI_MP1
// Creation Date: 2023-03-08
// Modified Date: 2023-03-15
// Description: 

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