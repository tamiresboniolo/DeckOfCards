using System;

namespace DeckOfCards
{ //starts with 0 because it depends on the Deck class
    //variable suitVal and it starts with 0
    public enum CardSuit
    {
        Club = 0,
        Diamond = 1,
        Heart = 2,
        Spade = 3,
    }

    public enum CardValue
    {
        Ace = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13,
    }

    public class Card
    {
        //the card has two properties the suit and the value
        public CardSuit SuitValue { get; private set; }
        public CardValue CardValue { get; private set; }

        //default constructor
        public Card(CardSuit suitVal, CardValue cardVal)
        {
            this.SuitValue = suitVal;
            this.CardValue = cardVal;
        }

        //return the string representation of card
        public override string ToString()
        {
            return "[" + CardValue + " of " + SuitValue + "s]";
        }
    }

    public class Deck
    {
        private Card[] deck;
        private const int NUMBER_OF_CARDS = 52;
        private Random randomCard = new Random();
        private int currentIndex = 0;


        //Default constructor, which creates and assigns 52 cards in the 'cards' field.
        public Deck()
        {
            this.deck = new Card[NUMBER_OF_CARDS];//instance the array
                                                  //creating the deck of 52 cards  
            for (int suitVal = 0; suitVal < 4; suitVal++)
            {
                for (int cardVal = 1; cardVal < 14; cardVal++)
                {
                    deck[suitVal * 13 + cardVal - 1] =
                        new Card((CardSuit)suitVal, (CardValue)cardVal);
                }
            }
        }

        public void ShuffleDeck()
        {
            //after shuffling, dealing should start at deck[0]               
            for (int first = 0; first < deck.Length; first++)
            {
                //select a random number between 0 and 51
                int second = randomCard.Next(NUMBER_OF_CARDS);

                //swap the current card with randomly card
                Card temp = deck[first];
                deck[first] = deck[second];
                deck[second] = temp;
            }
        }

        public void CutDeck()
        {
            //this.deck = new Card[NUMBER_OF_CARDS];//instance the array
            Card[] newDeck = new Card[NUMBER_OF_CARDS];
            int cutPoint = randomCard.Next(0, NUMBER_OF_CARDS - 1); //random number between 0 and 51
            int j = 0;
            int i;
            //copy from cutPoint to the end of the deck array, for the start of newDeck
            for (i = cutPoint; i < NUMBER_OF_CARDS; i++)
            {
                newDeck[j] = deck[i];
                j++;
            }
            i = 0;
            //copy the start of the deck array to the end of newDeck
            while (j < NUMBER_OF_CARDS)
            {
                newDeck[j] = deck[i];
                j++;
                i++;
            }
            deck = newDeck;
        }

        public void DealCards(Player player)
        {
            //giving the cards to the player
            for (int i = 0; i < 5; i++)
            {
                player.AddCard(this.deck[this.currentIndex]);
                this.currentIndex++;
            }
        }

        public override string ToString()
        {
            string s = "[";
            for (int i = this.currentIndex; i < NUMBER_OF_CARDS; i++)
                s += this.deck[i].ToString();
            s += "]";
            return s;
        }
    }

    public class Player
    {
        private readonly Card[] playerHand = new Card[5];
        public int CardsOnHand { get; private set; } //how many cards player has on hand

        //default constructor
        public Player()
        {
            this.CardsOnHand = 0;
        }

        public void AddCard(Card cardToAdd)
        {
            this.playerHand[this.CardsOnHand] = cardToAdd;
            this.CardsOnHand++; //increment the number of cards on player hand
        }

        public override string ToString()
        {
            string s = "[";
            foreach (Card c in playerHand)
                s += c.ToString();
            s += "]";
            return s;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Author: Tamires Boniolo");
            Console.WriteLine();
            Console.WriteLine("Standard 52 - Card Deck");
            Console.WriteLine("==================================================================================================================================================================================================================");
            Console.WriteLine();

            //creating a new Deck
            Deck deck1 = new Deck();

            //Creating 4 players
            Player player1 = new Player();
            Player player2 = new Player();
            Player player3 = new Player();
            Player player4 = new Player();


            Console.WriteLine("New Deck: ");
            Console.WriteLine(deck1);


            Console.WriteLine();

            Console.WriteLine("Shuffled Deck: ");
            deck1.ShuffleDeck();
            Console.WriteLine(deck1);

            Console.WriteLine();

            Console.WriteLine("Cut Deck: ");
            deck1.CutDeck();
            Console.WriteLine(deck1);

            Console.WriteLine();

            Console.WriteLine("Dealt Hands: ");
            deck1.DealCards(player1);
            Console.WriteLine("Player 1 Hand: ");
            Console.WriteLine(player1);
            Console.WriteLine();

            deck1.DealCards(player2);
            Console.WriteLine("Player 2 Hand: ");
            Console.WriteLine(player2);
            Console.WriteLine();

            deck1.DealCards(player3);
            Console.WriteLine("Player 3 Hand: ");
            Console.WriteLine(player3);
            Console.WriteLine();

            deck1.DealCards(player4);
            Console.WriteLine("Player 4 Hand: ");
            Console.WriteLine(player4);
            Console.WriteLine();

            Console.WriteLine();

            Console.WriteLine("Remaing cards in deck: ");
            Console.WriteLine(deck1);

            Console.ReadLine();
        }
    }
}