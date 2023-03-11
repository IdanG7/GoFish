using System;
using System.Collections.Generic;
using System.Linq;

namespace GurevichI_MP1
{
    public class Hand
    {
        private List<Card> cards = new List<Card>();
        private int numMatches;

        public Hand()
        {
            Reset();
        }

        public int GetNumMatches()
        {
            return numMatches;
        }

        public int GetSize()
        {
            return cards.Count();
        }

        public Card GetCard(int idx)
        {
            if (idx < 0 || idx >= cards.Count)
            {
                // Handle the case where the index is out of range
                return null;
            }
            return cards[idx];
        }


        public void Reset()
        {
            cards.Clear();

            numMatches = 0;
        }

        public void DisplayHand(bool visible)
        {
            for (int i = 0; i < GetSize(); i++)
            {
                cards[i].Display(visible, i + 1);

            }
        }

        public void AddCard(Card card)
        {
            cards.Add(card);
        }

        public int HasAPair()
        {
            for (int i = 0; i < GetSize(); i++)
            {
                for (int j = 0; j < GetSize(); j++)
                {
                    if (i != j && cards[i].GetRank() == cards[j].GetRank())
                    {
                        return j;
                    }
                }
            }

            return -1;
        }

        public int HasCardMatch(Card card)
        {
            for (int i = 0; i < cards.Count(); i++)
            {
                if (cards[i].GetRank() == card.GetRank())
                {
                    return i;
                }
            }

            return -1;
        }

        public bool DropCards(int idx1, int idx2)
        {
            if (cards[idx1].GetRank() == cards[idx2].GetRank())
            {
                numMatches++;

                if (idx1 > idx2)
                {
                    cards.RemoveAt(idx1);
                    cards.RemoveAt(idx2);
                }
                else
                {
                    cards.RemoveAt(idx2);
                    cards.RemoveAt(idx1);
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public Card StealCard(int idx)
        {
            Card tempCard = cards[idx];

            cards.RemoveAt(idx);

            return tempCard;
        }
    }
}