// Author: Idan Gurevich
// File Name: Program.cs
// Project Name: GurevichI_MP1
// Creation Date: 2023-03-08
// Modified Date: 2023-03-17
// Description: A Console version of the game Go Fish which simulates most features you would expect
// In a real world scenario.

using System;
using System.Text.RegularExpressions;

namespace GurevichI_MP1
{
    class MainClass
    {
        public const int START_CARDS = 5;

        private const bool PLAYER_TURN = false;

        private static Random rng = new Random();

        static bool gameOn = false;
        static bool screenOn = true;
        static bool winCheck = false;

        static bool playerTurn = PLAYER_TURN;

        public static void Main(string[] args)
        {
            Deck deck = new Deck();

            Hand playerHand = new Hand();
            Hand cpuHand = new Hand();

            bool menuInput;

            int userInput;
            string userInputChoice;
            int userStealChoice;

            DealCards(playerHand, cpuHand, deck);

            while (screenOn)
            {
                Console.Clear();
                Console.WriteLine("Welcome to Go Fish Game!");
                Console.WriteLine("Press 1 to Play or 2 to Exit");

                var keyInfo = Console.ReadKey(true);

                switch (keyInfo.KeyChar)
                {
                    case '1':
                        gameOn = true;
                        break;
                    case '2':
                        Console.WriteLine("Thanks for playing!");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to try again.");
                        Console.ReadKey(true);
                        break;
                }

                while (gameOn)
                {
                    if ((playerHand.GetNumMatches() + cpuHand.GetNumMatches()) != 26)
                    {
                        if (!playerTurn)
                        {
                            menuInput = true;

                            DrawGame(playerHand, cpuHand, deck);

                            Console.WriteLine("1. Ask for a card\n2. Drop a pair\n3. Draw a card and end your turn");

                            if (playerHand.HasAPair() >= 1)
                            {
                                Console.Clear();
                                DrawGame(playerHand, cpuHand, deck);
                                Console.WriteLine("1. Ask for a card");

                                Console.BackgroundColor = ConsoleColor.Yellow;
                                Console.ForegroundColor = ConsoleColor.Black;

                                Console.WriteLine("2. Drop a pair");
                                Console.ResetColor();

                                Console.WriteLine("3. Draw a card and end your turn");
                            }

                            if (playerHand.GetSize() == 0 && deck.GetSize() == 0)
                            {
                                Console.WriteLine("4. End Turn (No cards in your hand or deck");
                            }

                            Console.Write("\nEnter input: ");

                            userInput = Console.ReadKey().KeyChar;

                            while (menuInput)
                            {
                                switch (userInput)
                                {
                                    case '1':
                                        if (playerHand.GetSize() != 0)
                                        {
                                            DrawGame(playerHand, cpuHand, deck);
                                            Console.Write("Enter card to ask for (followed by ENTER): ");

                                            userInputChoice = Console.ReadLine();

                                            userInputChoice = Regex.Replace(userInputChoice, @"[^0-9]+", "");

                                            if (!string.IsNullOrEmpty(userInputChoice))
                                            {
                                                if (int.TryParse(userInputChoice, out userStealChoice) && userStealChoice <= playerHand.GetSize() && userStealChoice > 0)
                                                {
                                                    int matchingCard = cpuHand.HasCardMatch(playerHand.GetCard(userStealChoice - 1));
                                                    if (matchingCard != -1)
                                                    {
                                                        playerHand.AddCard(cpuHand.StealCard(matchingCard));

                                                        DrawGame(playerHand, cpuHand, deck);
                                                        Console.WriteLine("You stole a " + playerHand.GetCard(playerHand.GetSize() - 1).GetRank() + "!");
                                                        Console.WriteLine("\nPress ENTER to continue your turn");

                                                        while (Console.ReadKey().Key != ConsoleKey.Enter)
                                                        {
                                                        }

                                                        menuInput = false;
                                                    }

                                                    else
                                                    {
                                                        if (deck.GetSize() != 0)
                                                        {
                                                            DrawGame(playerHand, cpuHand, deck);
                                                            Console.WriteLine("The CPU does not have a " + playerHand.GetCard(userStealChoice - 1).GetRank() + "! GO FISH!");
                                                            Console.WriteLine("\nPress ENTER to pick up a card");

                                                            while (Console.ReadKey().Key != ConsoleKey.Enter)
                                                            {
                                                            }

                                                            playerHand.AddCard(deck.DrawCard());

                                                            DrawGame(playerHand, cpuHand, deck);
                                                            Console.WriteLine("You got a " + playerHand.GetCard(playerHand.GetSize() - 1).GetRank() + "!");
                                                            Console.WriteLine("\nPress ENTER to begin the CPU's turn");

                                                            while (Console.ReadKey().Key != ConsoleKey.Enter)
                                                            {
                                                            }

                                                            playerTurn = !playerTurn;
                                                        }

                                                        else
                                                        {
                                                            DrawGame(playerHand, cpuHand, deck);
                                                            Console.WriteLine("You can't pick up a card because the CPU doesn't have that card!");
                                                            Console.WriteLine("\nPress ENTER to begin the CPU's turn");

                                                            while (Console.ReadKey().Key != ConsoleKey.Enter)
                                                            {
                                                            }

                                                            playerTurn = !playerTurn;
                                                        }
                                                        menuInput = false;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Invalid input. Please enter a number between 1 and " + playerHand.GetSize() + ".");
                                                Console.WriteLine("\nPress ENTER to continue");

                                                while (Console.ReadKey().Key != ConsoleKey.Enter)
                                                {
                                                }
                                            }
                                        }
                                        else
                                        {
                                            DrawGame(playerHand, cpuHand, deck);
                                            Console.WriteLine("You cant ask for the CPU's cards because you do not have any cards!");
                                            Console.WriteLine("\nPress ENTER to return to menu!");

                                            while (Console.ReadKey().Key != ConsoleKey.Enter)
                                            {
                                            }

                                            menuInput = false;
                                        }
                                        break;

                                    case '2':
                                        if (playerHand.GetSize() != 0)
                                        {
                                            if (playerHand.HasAPair() != -1)
                                            {
                                                DrawGame(playerHand, cpuHand, deck);


                                                Card tempCard = playerHand.GetCard(playerHand.HasAPair());

                                                playerHand.DropCards(playerHand.HasAPair(), playerHand.HasCardMatch(playerHand.GetCard(playerHand.HasAPair())));

                                                DrawGame(playerHand, cpuHand, deck);
                                                Console.WriteLine("You dropped a pair of " + tempCard.GetRank() + "'s!");
                                                Console.WriteLine("\nPress ENTER to continue your turn!");

                                                while (Console.ReadKey().Key != ConsoleKey.Enter)
                                                {
                                                }
                                            }
                                            else
                                            {
                                                DrawGame(playerHand, cpuHand, deck);
                                                Console.WriteLine("You do not have any pairs to drop!");
                                                Console.WriteLine("\nPress ENTER to return to menu!");
                                                while (Console.ReadKey().Key != ConsoleKey.Enter)
                                                {
                                                }
                                            }
                                        }
                                        else
                                        {
                                            DrawGame(playerHand, cpuHand, deck);
                                            Console.WriteLine("You dont have any cards to drop!");
                                            Console.WriteLine("\nPress ENTER to return to menu!");
                                            while (Console.ReadKey().Key != ConsoleKey.Enter)
                                            {
                                            }
                                        }
                                        menuInput = false;
                                        break;

                                    case '3':
                                        if (deck.GetSize() != 0)
                                        {
                                            DrawGame(playerHand, cpuHand, deck);

                                            playerHand.AddCard(deck.DrawCard());

                                            DrawGame(playerHand, cpuHand, deck);
                                            Console.WriteLine("You picked up a " + playerHand.GetCard(playerHand.GetSize() - 1).GetRank() + "!");
                                            Console.WriteLine("\nPress ENTER to begin the CPU's turn");

                                            playerTurn = !playerTurn;

                                            while (Console.ReadKey().Key != ConsoleKey.Enter)
                                            {
                                            }

                                        }
                                        else
                                        {
                                            DrawGame(playerHand, cpuHand, deck);
                                            Console.WriteLine("You can not pick up cards because the deck is empty!");
                                            Console.WriteLine("\nPress ENTER to return to menu!");

                                            while (Console.ReadKey().Key != ConsoleKey.Enter)
                                            {
                                            }
                                        }
                                        menuInput = false;
                                        break;

                                    case '4':
                                        if (playerHand.GetSize() == 0 && deck.GetSize() == 0)
                                        {
                                            menuInput = false;
                                            playerTurn = !playerTurn;
                                        }
                                        else
                                        {

                                            DrawGame(playerHand, cpuHand, deck);

                                            Console.WriteLine("You cannot end your turn until you have played all cards in your hand and deck.");
                                            Console.WriteLine("Press ENTER to return to menu.");

                                            while (Console.ReadKey().Key != ConsoleKey.Enter)
                                            {
                                            }

                                            menuInput = false;
                                        }
                                        break;


                                    default:
                                        DrawGame(playerHand, cpuHand, deck);
                                        Console.WriteLine("Invalid input!");
                                        Console.WriteLine("\nPress ENTER to try again!");

                                        while (Console.ReadKey().Key != ConsoleKey.Enter)
                                        {
                                        }

                                        menuInput = false;
                                        break;
                                }
                            }
                        }
                        else
                        {
                            bool cpuTurn = true;

                            while (cpuHand.HasAPair() != -1)
                            {
                                DrawGame(playerHand, cpuHand, deck);
                                Console.WriteLine("The CPU is dropping a match!");
                                Console.WriteLine("\nPress ENTER to continue");

                                while (Console.ReadKey().Key != ConsoleKey.Enter)
                                {
                                }

                                cpuHand.DropCards(cpuHand.HasAPair(), cpuHand.HasCardMatch(cpuHand.GetCard(cpuHand.HasAPair())));

                            }

                            while (cpuTurn)
                            {
                                int randIndx = rng.Next(0, cpuHand.GetSize());
                                int matchingCard = playerHand.HasCardMatch(cpuHand.GetCard(randIndx));

                                if (matchingCard != -1)
                                {
                                    DrawGame(playerHand, cpuHand, deck);
                                    Console.WriteLine("The CPU asked for a " + cpuHand.GetCard(randIndx).GetRank());
                                    Console.WriteLine("\nPress ENTER to give him your " + cpuHand.GetCard(randIndx).GetRank() + playerHand.GetCard(matchingCard).GetSuit());

                                    while (Console.ReadKey().Key != ConsoleKey.Enter)
                                    {
                                    }

                                    cpuHand.AddCard(playerHand.StealCard(matchingCard));
                                }
                                else
                                {
                                    if (deck.GetSize() != 0)
                                    {
                                        DrawGame(playerHand, cpuHand, deck);


                                        if (cpuHand != null && cpuHand.GetSize() > randIndx)
                                        {
                                            Console.WriteLine("The CPU asked for a " + cpuHand.GetCard(randIndx).GetRank());
                                        }

                                        Console.WriteLine("\nPress ENTER to make him GO FISH!");

                                        while (Console.ReadKey().Key != ConsoleKey.Enter)
                                        {
                                        }

                                        cpuHand.AddCard(deck.DrawCard());
                                    }
                                    else
                                    {
                                        if ((cpuHand.GetNumMatches() + playerHand.GetNumMatches()) == 26)
                                        {

                                        }
                                        else
                                        {
                                            DrawGame(playerHand, cpuHand, deck);
                                            Console.WriteLine("The CPU can't pick up a card since the deck is empty");
                                            Console.WriteLine("\nPress ENTER to begin your turn!");
                                        }
                                    }
                                    cpuTurn = false;
                                }
                            }
                            playerTurn = !playerTurn;
                        }
                    }
                    else
                    {
                        gameOn = false;
                    }

                    CheckWin(playerHand, cpuHand, deck);
                }
            }
        }

        private static void DealCards(Hand playerHand, Hand cpuHand, Deck deck)
        {
            deck.ResetDeck();

            for (int i = 0; i < START_CARDS; i++)
            {
                playerHand.AddCard(deck.DrawCard());
                cpuHand.AddCard(deck.DrawCard());
            }
        }

        private static void DrawGame(Hand playerHand, Hand cpuHand, Deck deck)
        {
            if (playerTurn)
            {
                Console.WriteLine("Player's Turn");
            }
            else
            {
                Console.WriteLine("CPU's Turn");
            }

            if (deck.GetSize() >= 10)
            {
                Console.Clear();
                DrawHand(playerHand, true);

                Console.WriteLine(@"
 Deck
┌────┐
│    │
│ " + deck.GetSize() + @" |
│    │
└────┘");
                Console.WriteLine("\n");

                DrawHand(cpuHand, false);
                Console.WriteLine("");
            }

            else if (deck.GetSize() <= 10)
            {
                Console.Clear();
                DrawHand(playerHand, true);

                Console.WriteLine(@"
 Deck
┌────┐
│    │
│ " + deck.GetSize() + @"  |
│    │
└────┘");
                Console.WriteLine("\n");

                DrawHand(cpuHand, false);
                Console.WriteLine("");
            }
        }

        private static void DrawHand(Hand hand, bool visible)
        {

            LoopHand(hand, "┌─────┐");
            LoopHand(hand, "│     │");
            hand.DisplayHand(visible);
            Console.WriteLine("\t\t# of Matches: " + hand.GetNumMatches());
            LoopHand(hand, "│     │");
            LoopHand(hand, "└─────┘");

            for (int i = 0; i < hand.GetSize(); i++)
            {
                Console.Write(Convert.ToString(i + 1).PadLeft(4).PadRight(7));
            }
            Console.WriteLine();
        }

        private static void LoopHand(Hand hand, string output)
        {
            for (int i = 0; i < hand.GetSize(); i++)
            {
                Console.Write(output);
            }

            Console.WriteLine();
        }

        public static void CheckWin(Hand playerHand, Hand cpuHand, Deck deck)
        {
            if ((playerHand.GetNumMatches() + cpuHand.GetNumMatches()) == 26 || playerHand.GetNumMatches() == 26 || cpuHand.GetNumMatches() == 26)
            {
                DrawGame(playerHand, cpuHand, deck);
                Console.WriteLine("Gave Over\nCheck to see results");

                var endPick = Console.ReadKey(true);

                switch (endPick.Key)
                {
                    case ConsoleKey.Enter:
                        winCheck = true;
                        break;
                }

                while (winCheck)
                {
                    if (playerHand.GetNumMatches() > cpuHand.GetNumMatches())
                    {
                        Console.Clear();
                        Console.WriteLine("Congratulations, You won with " + playerHand.GetNumMatches() + " Matches\nThe CPU had " + cpuHand.GetNumMatches() + " Matches");
                    }
                    else if (cpuHand.GetNumMatches() > playerHand.GetNumMatches())
                    {
                        Console.Clear();
                        Console.WriteLine("You lost sucker\n\nYou had " + playerHand.GetNumMatches() + " Matches\nThe CPU had " + cpuHand.GetNumMatches() + " Matches");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Its a Tie");
                    }

                    Console.WriteLine("\n\nWhat would you like to do\n(R)estart\n(E)xit");

                    var playerPick = Console.ReadKey(true);

                    switch (playerPick.Key)
                    {
                        case ConsoleKey.R:
                            Console.Clear();
                            Main(new string[] { });
                            break;


                        case ConsoleKey.E:
                            Console.WriteLine("Thanks for playing!");
                            Environment.Exit(0);
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Press any key to try again.");
                            Console.ReadKey(true);
                            break;
                    }
                }
            }
        }
    }
}