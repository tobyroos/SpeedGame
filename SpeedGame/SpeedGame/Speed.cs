using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Card
{
    public enum Suites
    {
        Hearts = 0,
        Diamonds,
        Clubs,
        Spades
    }

    public int Value
    {
        get;
        set;
    }

    public Suites Suite
    {
        get;
        set;
    }

    //Used to get full name, also useful 
    //if you want to just get the named value
    public string NamedValue
    {
        get
        {
            string name = string.Empty;
            switch (Value)
            {
                case (14):
                    name = "Ace";
                    break;
                case (13):
                    name = "King";
                    break;
                case (12):
                    name = "Queen";
                    break;
                case (11):
                    name = "Jack";
                    break;
                default:
                    name = Value.ToString();
                    break;
            }

            return name;
        }
    }

    public string Name
    {
        get
        {
            return NamedValue + " of  " + Suite.ToString();
        }
    }

    public string associatedImg
    {
        get
        {
            string imgSrc = "/img/";
            switch (Value)
            {
                case (14):
                    imgSrc += "a";
                    break;
                case (13):
                    imgSrc += "k";
                    break;
                case (12):
                    imgSrc += "q";
                    break;
                case (11):
                    imgSrc += "j";
                    break;
                default:
                    imgSrc += Value.ToString();
                    break;
            }

            switch (Suite.ToString())
            {
                case ("Hearts"):
                    imgSrc += "Heart.png";
                    break;
                case ("Diamonds"):
                    imgSrc += "Diamond.png";
                    break;
                case ("Clubs"):
                    imgSrc += "Club.png";
                    break;
                default:
                    imgSrc += "Spade.png";
                    break;
            }

            return imgSrc;
        }
    }

    public Card(int Value, Suites Suite)
    {
        this.Value = Value;
        this.Suite = Suite;
    }
}

public class Deck
{
    public List<Card> Cards = new List<Card>();
    public void FillDeck()
    {
        for (int i = 0; i < 52; i++)
        {
            Card.Suites suite = (Card.Suites)(Math.Floor((decimal)i / 13));
            int val = i % 13 + 2;
            Cards.Add(new Card(val, suite));
        }

    }

    public void Shuffle()
    {
        var rnd = new Random();
        var randomized = Cards.OrderBy(item => rnd.Next());
        List<Card> Cards2 = new List<Card>();
        //this.Cards.Clear();
        foreach (Card card in randomized)
        {
            Cards2.Add(card);
        }
        Cards.Clear();
        foreach (Card card in Cards2)
        {
            Cards.Add(card);
        }

    }
    public void PrintDeck()
    {
        foreach (Card card in this.Cards)
        {
            Console.WriteLine(card.Name);
        }
    }
}

public class PlayerStack
{
    List<Card> hand = new List<Card>();
    public void CreatePlayerStack(Deck d)
    {
        for (int i = 0; i < 5; i++)
        {
            hand.Add(d.Cards[0]);
            d.Cards.Remove(d.Cards[0]);
        }
    }
    public void AddToHand(Card c)
    {
        hand.Add((Card)c);
    }
    public void RemoveFromHand(Card c)
    {
        hand.Remove(c);
    }
    public List<Card> getHand()
    {
        return hand;
    }
    public List<string> ReturnHand()
    {
        List<string> handStr = new List<string>();
        foreach (Card c in hand)
        {
            handStr.Add(c.Name);
        }
        return handStr;
    }
    public Card CardChoice(int index)
    {
        Card choice = hand[index];
        return choice;
    }
}
public class DrawStack
{
    Stack<Card> stack = new Stack<Card>();
    public void CreateDrawStack(Deck d)
    {
        for (int i = 0; i < 15; i++)
        {
            stack.Push(d.Cards[0]);
            d.Cards.Remove(d.Cards[0]);
        }
    }
    public Stack<Card> getDraw()
    {
        return stack;
    }

    public Card RemoveFromDrawStack()
    {
        Card card = stack.Pop();
        return card;
    }
}

public class PlayStack
{
    Stack<Card> stack = new Stack<Card>();
    public void CreatePlayStack(Deck d)
    {
        stack.Push(d.Cards[0]);
        d.Cards.Remove(d.Cards[0]);
    }
    public void AddToPlayStack(Card c)
    {
        stack.Push((Card)c);
    }
    public Card RemoveFromPlayStack()
    {
        Card card = stack.Pop();
        return card;
    }
    public Card ShowTop()
    {
        Card card = stack.Peek();
        return card;
    }

    public Stack<Card> getPlayStack()
    {
        return stack;
    }
}
public class ExtraStack
{
    Stack<Card> stack = new Stack<Card>();
    public void CreateExtraStack(Deck d)
    {
        for (int i = 0; i < 5; i++)
        {
            stack.Push(d.Cards[0]);
            d.Cards.Remove(d.Cards[0]);
        }
    }

    public Card RemoveFromExtraStack()
    {
        Card card = stack.Pop();
        return card;
    }
    public bool isEmpty()
    {
        bool isEmpty = true;
        if (stack.Count > 0)
        {
            isEmpty = true;
        }
        return isEmpty;
    }

    public Stack<Card> getExtraStack()
    {
        return stack;
    }
}


public class GameLogic
{

    Card selected;

    public void selectedCard(Card c)
    {
        selected = c;
    }
}
//You can add these to a main to call this program and it will run up to this point
/*          Deck deck = new Deck();
            deck.FillDeck();
            deck.Shuffle();
            deck.PrintDeck();
            //Create hand
            PlayerStack playerStack1 = new PlayerStack();
            playerStack1.CreatePlayerStack(deck);
            PlayerStack playerStack2 = new PlayerStack();
            playerStack2.CreatePlayerStack(deck);
            //Create Draw Piles
            DrawStack drawStack1 = new DrawStack();
            drawStack1.CreateDrawStack(deck);
            DrawStack drawStack2 = new DrawStack();
            drawStack2.CreateDrawStack(deck);
            //Create Stacks to play on
            PlayStack playStack1 = new PlayStack();
            playStack1.CreatePlayStack(deck);
            PlayStack playstack2 = new PlayStack();
            playstack2.CreatePlayStack(deck);
            //Create Stacks of extras
            ExtraStack extraStack1 = new ExtraStack();
            extraStack1.CreateExtraStack(deck);
            ExtraStack extraStack2 = new ExtraStack();
            extraStack2.CreateExtraStack(deck);
            Console.ReadLine();*/

//Add this to main for a single iteration of playing a valid card in console
/*          Console.WriteLine("Play stack:");
            Console.WriteLine(playStack1.ShowTop().Name + " (1)");
            Console.WriteLine(playstack2.ShowTop().Name + " (2)");
            Console.WriteLine("Enter a card to play from your hand:");
            playerStack1.PrintHand();
            int choice = int.Parse(Console.ReadLine()) - 1;
            Card cardChoice = playerStack1.CardChoice(choice);
            Console.WriteLine("Enter a the stack to place your card on:");
            int stackChoice = int.Parse(Console.ReadLine());
            if (stackChoice == 1)
            {
                if (cardChoice.Value == playStack1.ShowTop().Value - 1 || cardChoice.Value == playStack1.ShowTop().Value + 1)
                {
                    playStack1.AddToPlayStack(cardChoice);
                    playerStack1.RemoveFromHand(cardChoice);
                    playerStack1.AddToHand(drawStack1.RemoveFromDrawStack());
                    playerStack1.PrintHand();
                }
                else
                {
                    Console.WriteLine("Invalid Play");
                }
            }
            else if (stackChoice == 2)
            {
                if (cardChoice.Value == playstack2.ShowTop().Value - 1 || cardChoice.Value == playstack2.ShowTop().Value + 1)
                {
                    playstack2.AddToPlayStack(cardChoice);
                    playerStack1.RemoveFromHand(cardChoice);
                    playerStack1.AddToHand(drawStack1.RemoveFromDrawStack());
                    playerStack1.PrintHand();
                }
                else
                {
                    Console.WriteLine("Invalid Play");
                }
            }*/
