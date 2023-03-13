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
            if (rank == "T")
            {
                return "10";
            }
            else
            {
                return rank;
            }
        }


        public string GetSuit()
        {
            return suit;
        }

        public void Display(bool visible, int index)
        {
            Console.Write("│ ");

            if (visible)
            {
                Console.ForegroundColor = colour;

                if (GetRank() == "10")
                {
                    Console.Write(GetRank() + suit);
                }
                else
                {
                    Console.Write(" " + GetRank() + suit);
                }

                Console.ResetColor();
                Console.Write(" │");
            }
            else
            {
                Console.Write("**");
                Console.Write("  │");
            }
        }



        public bool MatchCard(Card card)
        {
            string thisRank = GetRank();

            string otherRank = card.GetRank();

            if (thisRank == "10")
            {
                thisRank = "T";
            }

            if (otherRank == "10")
            {
                otherRank = "T";
            }

            return thisRank.Equals(otherRank);
        }

    }
}
