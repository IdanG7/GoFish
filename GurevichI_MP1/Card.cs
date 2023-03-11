using System;
namespace GurevichI_MP1
{
    public class Card
    {
        private const int NUM_RANKS = 13;

        private const string ranks = "A23456789TJQK";
        private const string suits = "♥♠♦♣";
        private string rank;
        private string suit;

        ConsoleColor colour;

        public Card(int cardNum)
        {
            this.rank = Convert.ToString(ranks[cardNum % NUM_RANKS]);

            this.suit = Convert.ToString(suits[cardNum / NUM_RANKS]);

            if (cardNum / NUM_RANKS % 2 == 0)
            {
                colour = ConsoleColor.Red;
            }
            else
            {
                colour = ConsoleColor.Blue;
            }
        }

        public string GetRank()
        {
            return rank;
        }

        public string GetSuit()
        {
            return suit;
        }

        public void Display(bool visible, int index)
        {
            Console.Write("│  ");

            if (visible)
            {
                Console.ForegroundColor = colour;
                Console.Write(rank + suit);
                Console.ResetColor();
            }
            else
            {
                Console.Write("**");
            }

            Console.Write(" │");
        }

        public bool MatchCard(Card card)
        {
            return card.GetRank().Equals(this.rank);
        }
    }
}
