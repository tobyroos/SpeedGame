using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
/*using Newtonsoft.Json;*/

namespace SignalRChat.Hubs
{
    public static class UserHandler
    {
        public static HashSet<string> ConnectedIds = new HashSet<string>();
    }
    public class GameHub : Hub
    {
        public async Task InitiateGame()
        {
            if (UserHandler.ConnectedIds.Count == 2)
            {
                int count = 0;
                string p1 = "";
                string p2 = "";
                foreach (string a in UserHandler.ConnectedIds)
                {
                    if (count == 0)
                    {
                        p1 = a;
                        count++;
                    }
                    else
                    {
                        p2 = a;
                    }
                }
                Deck deck = new Deck();
                deck.FillDeck();
                deck.Shuffle();
                PlayerStack playerStack1 = new PlayerStack();
                PlayerStack playerStack2 = new PlayerStack();
                ExtraStack extraStack1 = new ExtraStack();
                ExtraStack extraStack2 = new ExtraStack();
                PlayStack playStack1 = new PlayStack();
                PlayStack playStack2 = new PlayStack();
                DrawStack drawStack1 = new DrawStack();
                DrawStack drawStack2 = new DrawStack();
                playerStack1.CreatePlayerStack(deck);

                playerStack2.CreatePlayerStack(deck);
                //Create Draw Piles

                drawStack1.CreateDrawStack(deck);

                drawStack2.CreateDrawStack(deck);
                //Create Stacks to play on

                playStack1.CreatePlayStack(deck);

                playStack2.CreatePlayStack(deck);
                //Create Stacks of extras

                extraStack1.CreateExtraStack(deck);

                extraStack2.CreateExtraStack(deck);

                string p1Hand = JsonSerializer.Serialize(playerStack1.getHand());
                string p2Hand = JsonSerializer.Serialize(playerStack2.getHand());
                string ps1 = JsonSerializer.Serialize(playStack1.getPlayStack());
                string ps2 = JsonSerializer.Serialize(playStack2.getPlayStack());
                string ds1 = JsonSerializer.Serialize(drawStack1.getDraw());
                string ds2 = JsonSerializer.Serialize(drawStack2.getDraw());
                string es1 = JsonSerializer.Serialize(extraStack1.getExtraStack());
                string es2 = JsonSerializer.Serialize(extraStack2.getExtraStack());
                string psTop1 = JsonSerializer.Serialize(playStack1.ShowTop());
                string psTop2 = JsonSerializer.Serialize(playStack2.ShowTop());

                await Clients.Client(p1).SendAsync("CreateGame", p1Hand, playerStack2.getHand().Count, ds1, drawStack2.getDraw().Count, ps1, ps2, es1, es2, psTop1, psTop2);
                await Clients.Client(p2).SendAsync("CreateGame", p2Hand, playerStack1.getHand().Count, ds2, drawStack1.getDraw().Count, ps1, ps2, es1, es2, psTop1, psTop2);
            }
            //await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task RestartGame(int opponentChoice)
        {
            string[,] stuck = new string[,] { { "0", "0" }, { "0", "0" } };
            List<string> cIds = UserHandler.ConnectedIds.ToList<string>();
            stuck[0, 0] = cIds[0];
            stuck[1, 0] = cIds[1];
            string User = string.Empty;
            string Opponent = string.Empty;
            if (stuck[0, 0] == Context.ConnectionId)
            {
                stuck[0, 1] = "1";
                User = cIds[0];
                Opponent = cIds[1];

            }
            else if (stuck[1, 0] == Context.ConnectionId)
            {
                stuck[1, 1] = "1";
                User = cIds[1];
                Opponent = cIds[0];
            }
            if (stuck[0, 1].Equals("1") && opponentChoice == 1 || stuck[1, 1].Equals("1") && opponentChoice == 1)
            {
                Deck deck = new Deck();
                deck.FillDeck();
                deck.Shuffle();
                PlayerStack playerStack1 = new PlayerStack();
                PlayerStack playerStack2 = new PlayerStack();
                ExtraStack extraStack1 = new ExtraStack();
                ExtraStack extraStack2 = new ExtraStack();
                PlayStack playStack1 = new PlayStack();
                PlayStack playStack2 = new PlayStack();
                DrawStack drawStack1 = new DrawStack();
                DrawStack drawStack2 = new DrawStack();
                playerStack1.CreatePlayerStack(deck);

                playerStack2.CreatePlayerStack(deck);
                //Create Draw Piles

                drawStack1.CreateDrawStack(deck);

                drawStack2.CreateDrawStack(deck);
                //Create Stacks to play on

                playStack1.CreatePlayStack(deck);

                playStack2.CreatePlayStack(deck);
                //Create Stacks of extras

                extraStack1.CreateExtraStack(deck);

                extraStack2.CreateExtraStack(deck);

                string p1Hand = JsonSerializer.Serialize(playerStack1.getHand());
                string p2Hand = JsonSerializer.Serialize(playerStack2.getHand());
                string ps1 = JsonSerializer.Serialize(playStack1.getPlayStack());
                string ps2 = JsonSerializer.Serialize(playStack2.getPlayStack());
                string ds1 = JsonSerializer.Serialize(drawStack1.getDraw());
                string ds2 = JsonSerializer.Serialize(drawStack2.getDraw());
                string es1 = JsonSerializer.Serialize(extraStack1.getExtraStack());
                string es2 = JsonSerializer.Serialize(extraStack2.getExtraStack());
                string psTop1 = JsonSerializer.Serialize(playStack1.ShowTop());
                string psTop2 = JsonSerializer.Serialize(playStack2.ShowTop());

                await Clients.Client(User).SendAsync("Restart", p1Hand, playerStack2.getHand().Count, ds1, drawStack2.getDraw().Count, ps1, ps2, es1, es2, psTop1, psTop2);
                await Clients.Client(Opponent).SendAsync("Restart", p2Hand, playerStack1.getHand().Count, ds2, drawStack1.getDraw().Count, ps1, ps2, es1, es2, psTop1, psTop2);
                await Clients.All.SendAsync("UpdatePlayField", ps1, ps2, psTop1, psTop2, es1, es2, 0, 0);
                return;
            }
            await Clients.Client(Opponent).SendAsync("PlayAgainChoice", 1);
        }
        public async Task compareCard(string PlayStack1JS, string CardFromHand, string Hand, int OpponentHandCountJS, string PlayerDrawStackJS, int OpponentDrawStackCountJS, string PlayStack1JSTop, string exStack1, string exStack2, int stackNum)
        {
            Card card = JsonSerializer.Deserialize<Card>(PlayStack1JSTop);
            Card cardFromHand = JsonSerializer.Deserialize<Card>(CardFromHand);

            //Make Stacks
            List<Card> hand = JsonSerializer.Deserialize<List<Card>>(Hand);
            List<Card> PlayerDrawStack = JsonSerializer.Deserialize<List<Card>>(PlayerDrawStackJS);
            List<Card> PlayStack1 = JsonSerializer.Deserialize<List<Card>>(PlayStack1JS);
            //List<Card> PlayStack2 = JsonSerializer.Deserialize<List<Card>>(PlayStack2JS);
            List<Card> ExStack1 = JsonSerializer.Deserialize<List<Card>>(exStack1);
            List<Card> ExStack2 = JsonSerializer.Deserialize<List<Card>>(exStack2);

            Stack<Card> playerDrawStackStack = new Stack<Card>();
            Stack<Card> PlayStack1Stack = new Stack<Card>();
            /*Stack<Card> PlayStack2Stack = new Stack<Card>();*/
            Stack<Card> ExStack1Stack = new Stack<Card>();
            Stack<Card> ExStack2Stack = new Stack<Card>();

            PlayerDrawStack.Reverse();
            foreach (Card c in PlayerDrawStack)
            {
                playerDrawStackStack.Push(c);
            }

            PlayStack1.Reverse();
            foreach (Card c in PlayStack1)
            {
                PlayStack1Stack.Push(c);
            }

           /* PlayStack2.Reverse();
            foreach (Card c in PlayStack2)
            {
                PlayStack2Stack.Push(c);
            }*/

            ExStack1.Reverse();
            foreach (Card c in ExStack1)
            {
                ExStack1Stack.Push(c);
            }

            ExStack2.Reverse();
            foreach (Card c in ExStack2)
            {
                ExStack2Stack.Push(c);
            }

            int pos = 0, res1 = 0, res2 = 0;
            if (cardFromHand.Value == 14)
            {
                if (card.Value == 13 || card.Value == 2)
                {
                    Console.WriteLine("Top");
                    PlayStack1Stack.Push(cardFromHand);
                    for (int i = 0; i < hand.Count; i++)
                    {
                        if (hand[i].Name.CompareTo(cardFromHand.Name) == 0)
                        {
                            pos = i;
                            hand.RemoveAt(i);
                        }
                    }
                    if (playerDrawStackStack.Count != 0)
                    {
                        hand.Insert(pos, playerDrawStackStack.Pop());
                    } 
                    else if (hand.Count == 0)
                    {
                        res1 = 2;
                        res2 = 1;
                    }
                }
            }
            else if (card.Value == 14)
            {
                if (cardFromHand.Value == 13 || cardFromHand.Value == 2)
                {
                    Console.WriteLine("Top");
                    PlayStack1Stack.Push(cardFromHand);
                    for (int i = 0; i < hand.Count; i++)
                    {
                        if (hand[i].Name.CompareTo(cardFromHand.Name) == 0)
                        {
                            pos = i;
                            hand.RemoveAt(i);
                        }
                    }
                    if (playerDrawStackStack.Count != 0)
                    {
                        hand.Insert(pos, playerDrawStackStack.Pop());
                    }
                    else if (hand.Count == 0)
                    {
                        res1 = 2;
                        res2 = 1;
                    }
                }
            }
            else if (cardFromHand.Value == card.Value + 1 || cardFromHand.Value == card.Value - 1)
            {
                Console.WriteLine("Top");
                PlayStack1Stack.Push(cardFromHand);
                for (int i = 0; i < hand.Count; i++)
                {
                    if (hand[i].Name.CompareTo(cardFromHand.Name) == 0)
                    {
                        pos = i;
                        hand.RemoveAt(i);
                        break;
                    }
                }
                if (playerDrawStackStack.Count != 0)
                {
                    hand.Insert(pos, playerDrawStackStack.Pop());
                }
                else if (hand.Count == 0)
                {
                    res1 = 2;
                    res2 = 1;
                }
            }
            else
            {
                Console.WriteLine("Very Bottom");
                return;
            }

            string p1 = String.Empty; 
            string p2 = String.Empty;
            List<string> cIds = UserHandler.ConnectedIds.ToList<string>();
            if (cIds[0] == Context.ConnectionId)
            {
                p1 = cIds[0];
                p2 = cIds[1];
            } 
            else
            {
                p1 = cIds[1];
                p2 = cIds[0];
            }

            string handStr = JsonSerializer.Serialize(hand);
            string playerDrawStackStr = JsonSerializer.Serialize(playerDrawStackStack);
            string playStack1Str = JsonSerializer.Serialize(PlayStack1Stack);
            string exStack1Str = JsonSerializer.Serialize(ExStack1Stack);
            string exStack2Str = JsonSerializer.Serialize(ExStack2Stack);
            string playStack1Top = JsonSerializer.Serialize(PlayStack1Stack.Peek());

            int handCount = hand.Count();
           
            await Clients.Client(p1).SendAsync("UpdateGame", handStr, playerDrawStackStr, playStack1Str, exStack1Str, exStack2Str, playStack1Top, stackNum, handCount, res1);
            await Clients.Client(p2).SendAsync("UpdateGameOpp", playerDrawStackStack.Count, playStack1Top, stackNum, handCount, res2);
        }

        public async Task CardFlip(string exStack1JS, string exStack2JS, string playStack1JS, string playStack2JS, string playStack1JSTop, string playStack2JSTop, int opponentStuck, int playerStuck)
        {
            string[,] stuck = new string[,] { { "0", "0" }, { "0", "0" } };
            List<string> cIds = UserHandler.ConnectedIds.ToList<string>();
            stuck[0,0] = cIds[0];
            stuck[1,0] = cIds[1];
            string User = string.Empty;
            string Opponent = string.Empty;
            if (stuck[0,0] == Context.ConnectionId)
            {
                stuck[0,1] = "1";
                User = cIds[0];
                Opponent = cIds[1];
                
            }
            else if (stuck[1,0] == Context.ConnectionId)
            {
                stuck[1,1] = "1";
                User = cIds[1];
                Opponent = cIds[0];
            }
            List<Card> playStack1 = JsonSerializer.Deserialize<List<Card>>(playStack1JS);
            List<Card> playStack2 = JsonSerializer.Deserialize<List<Card>>(playStack2JS);
            List<Card> exStack1 = JsonSerializer.Deserialize<List<Card>>(exStack1JS);
            List<Card> exStack2 = JsonSerializer.Deserialize<List<Card>>(exStack2JS);

            Stack<Card> PlayStack1 = new Stack<Card>();
            Stack<Card> PlayStack2 = new Stack<Card>();
            Stack<Card> ExStack1 = new Stack<Card>();
            Stack<Card> ExStack2 = new Stack<Card>();


            if (opponentStuck == 1 && playerStuck == 1)
            {
                stuck[0,1] = "0";
                stuck[1,1] = "0";
                opponentStuck = 0;
                playerStuck = 0;

                playStack1.Reverse();
                foreach (Card c in playStack1)
                {
                    PlayStack1.Push(c);
                }
                playStack2.Reverse();
                foreach (Card c in playStack2)
                {
                    PlayStack2.Push(c);
                }
                exStack1.Reverse();
                foreach (Card c in exStack1)
                {
                    ExStack1.Push(c);
                }
                exStack2.Reverse();
                foreach (Card c in exStack2)
                {
                    ExStack2.Push(c);
                }

                if (ExStack1.Count == 0)
                {
                    Stack<Card> shuffleStack = new Stack<Card>();
                    int count = PlayStack1.Count();
                    for(int i = 0; i < count; i++) 
                    {
                        shuffleStack.Push(PlayStack1.Pop());
                    }
                    count = PlayStack2.Count();
                    for (int i = 0; i < count; i++)
                    {
                        shuffleStack.Push(PlayStack2.Pop());
                    }

                    count = (shuffleStack.Count()/2)-1;
                    for (int i = 0; i < count; i++)
                    {
                        ExStack1.Push((Card)shuffleStack.Pop());
                        ExStack2.Push((Card)shuffleStack.Pop());
                    }
                    PlayStack1.Push(shuffleStack.Pop());
                    PlayStack2.Push(shuffleStack.Pop());
                }
                else
                {
                    PlayStack1.Push(ExStack1.Pop());
                    PlayStack2.Push(ExStack2.Pop());
                }

                string playStack1Str = JsonSerializer.Serialize(PlayStack1);
                string playStack2Str = JsonSerializer.Serialize(PlayStack2);
                string playStack1Top = JsonSerializer.Serialize(PlayStack1.Peek());
                string playStack2Top = JsonSerializer.Serialize(PlayStack2.Peek());
                string exStack1Str = JsonSerializer.Serialize(ExStack1);
                string exStack2Str = JsonSerializer.Serialize(ExStack2);

                await Clients.All.SendAsync("UpdatePlayField", playStack1Str, playStack2Str, playStack1Top, playStack2Top, exStack1Str, exStack2Str, opponentStuck, playerStuck);
            }
            else
            {

                await Clients.Client(User).SendAsync("UpdatePlayField", playStack1JS, playStack2JS, playStack1JSTop, playStack2JSTop, exStack1JS, exStack2JS, opponentStuck, playerStuck);
                await Clients.Client(Opponent).SendAsync("UpdatePlayField", playStack1JS, playStack2JS, playStack1JSTop, playStack2JSTop, exStack1JS, exStack2JS, playerStuck, opponentStuck);
            }
        }

        public override Task OnConnectedAsync()
        {
            UserHandler.ConnectedIds.Add(Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            UserHandler.ConnectedIds.Remove(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}