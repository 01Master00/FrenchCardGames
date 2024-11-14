static void Main()
{
    bool end = false;
    int menu, money = 1000;


    do
    {

        Console.WriteLine("Wellcome your money is: " + money + " please choose an option:\n1. Blackjack\nOr any other number to exit\n->");
        menu = Convert.ToInt32(Console.ReadLine());
        if (money <= 0)
        {
            Console.WriteLine("You don't have enught money left to play!\nexiting...");
            menu = 0;
        }
        switch (menu)
        {
            case 1:
                money = Blackjack(money);
                break;
            default:
                Console.WriteLine("You choose to exit");
                Console.WriteLine("Goodbye!");
                end = true;
                break;
        }
    } while (!end);
}


static Dictionary<string, string> Declaration()
{
    Dictionary<string, string> deck = new Dictionary<string, string>();
    List<string> tipe = new List<string>();
    tipe.Add("H");
    tipe.Add("D");
    tipe.Add("C");
    tipe.Add("S");
    List<string> trans = new List<string>();
    trans.Add("Of Hearts");
    trans.Add("Of Diamonds");
    trans.Add("Of Clubs");
    trans.Add("Of Spades");
    string womp;
    for (int i = 2; i <= 14; i++)
    {
        switch (i)
        {
            case 11:
                womp = "J";
                break;
            case 12:
                womp = "D";
                break;
            case 13:
                womp = "K";
                break;
            case 14:
                womp = "A";
                break;
            default:
                womp = Convert.ToString(i);
                Console.WriteLine(womp);
                break;
        }
        for (int j = 0; j < 4; j++)
        {
            deck.Add($"{womp}{tipe[j]}", $"{womp} {trans[j]}");
        }


    }
    return deck;
}


static Dictionary<string, string> Shuffle(Dictionary<string, string> deck)
{
    Random random = new Random();
    Dictionary<string, string> help = new Dictionary<string, string>();
    int r;
    for (int i = 0; i < 50; i++)
    {
        while (deck.Count > 0)
        {
            r = random.Next(0, deck.Count);
            help.Add(deck.ElementAt(r).Key, deck.ElementAt(r).Value);
            deck.Remove(deck.ElementAt(r).Key);

        }
        while (help.Count > 0)
        {
            r = random.Next(0, help.Count);
            deck.Add(help.ElementAt(r).Key, help.ElementAt(r).Value);
            help.Remove(help.ElementAt(r).Key);
        }


        for (int j = 0; j < 10; j++)
        {
            help.Add(deck.ElementAt(0).Key, deck.ElementAt(0).Value);
            deck.Remove(deck.ElementAt(0).Key);
        }
        for (int j = 0; j < 10; j++)
        {
            deck.Add(help.ElementAt(0).Key, help.ElementAt(0).Value);
            help.Remove(help.ElementAt(0).Key);
        }

    }
    return deck;
}

static int calc(Dictionary<string, string> hand)
{
    int sum = 0, aces = 0;
    foreach (var item in hand)
    {
        if (Convert.ToString(item.Key[0]) == "J" || Convert.ToString(item.Key[0]) == "D" || Convert.ToString(item.Key[0]) == "K")
        {
            sum += 10;
        }
        else if (Convert.ToString(item.Key[0]) == "A")
        {
            aces++;
        }
        else if (item.Value[0] + "" == "1")
        {
            sum += 10;
        }
        else
        {
            int temp = int.Parse(Convert.ToString(item.Key[0]));
            sum += temp;
        }

    }
    if (aces > 0)
    {
        for (int i = 0; i < aces; i++)
        {
            if (sum + 11 <= 21)
            {
                sum += 11;
            }
            else
            {
                sum += 1;
            }
        }
    }
    return sum;
}


static void print(Dictionary<string, string> you, Dictionary<string, string> dealer)
{
    Console.Clear();
    int dhand = calc(dealer);
    int yhand = calc(you);
    string d = "", y = "";
    foreach (var item in dealer) 
    {
        d += $"\n-{item.Value}";
    }
    foreach (var item in you) 
    {
        y += $"\n-{item.Value}";
    }

    Console.WriteLine($"""
                ---------
                | o   o |
                |   _   |
                |  ---  |
                ---------
                Dealer's hand: {dhand} {d}
            

                  /------\
                 /        \
                |  o    o |
                |   \-/   | 
                |         |
                 \ \___/  /
                  \______/
                 Your hand: {yhand} {y}


                """);
    Thread.Sleep(2000);
}


static bool dealing(Dictionary<string, string> dealer)
{
    bool end = true;
    if (calc(dealer) > 17)
    {
        end = false;    
    }

    return end;
}


static int Blackjack(int money)
{
    Dictionary<string, string> dealer = new Dictionary<string, string>();
    Dictionary<string, string> you = new Dictionary<string, string>();
    Dictionary<string, string> deck = Shuffle(Declaration());

    Console.Clear();
    Console.WriteLine("Wellcome to Blackjack! Your goal is to have higher cards then the Dealer, but be aware: DON'T go above 21!\nPress anything to continue...\n->");
    Console.ReadLine();
    Console.Clear();

    int bet, another, hit;
    bool play = false, game = false, deal = false, miss = true;
    do
    {
        if (money <= 0)
        {
            return 0;
        }
        play = false;
        game = false; 
        deal = false;
        if (deck.Count < 20)
        {
            deck.Clear();
            deck = Shuffle(Declaration());
            
        }
        Console.WriteLine($"give me your bet, you'r money is: {money}\n->");
        bet = Convert.ToInt32(Console.ReadLine());
        if (money - bet > 0 || bet > 0)
        {
            money -= bet;
            game = true;
        }

      
        you.Clear();
        dealer.Clear();
        //card dealing
        for (int i = 0; i < 4; i++)
        {
            if (i % 2 == 0)
            {
                dealer.Add(deck.ElementAt(0).Key, deck.ElementAt(0).Value);
                deck.Remove(deck.ElementAt(0).Key);
            }
            else
            {
                you.Add(deck.ElementAt(0).Key, deck.ElementAt(0).Value);
                deck.Remove(deck.ElementAt(0).Key);
            }

            print(you, dealer);
        }

        // BLACKJACK detection
        if (calc(dealer) == 21)
        {
            Console.WriteLine("dealer wins with BLACKJACK!");
            game = false;
        }
        else if (calc(you) == 21)
        {
            Console.WriteLine("You win with BLACKJACK!");
            game = false;
            money += Convert.ToInt32(bet * 2.5);
        }

        // user interface
        do
        {
            deal = true;
            if (game)
            {
                Console.WriteLine("Do you want any adictional cards?\n1. YES (hit)\n2. NO (stand)\n->");
                hit = Convert.ToInt32(Console.ReadLine());
                if (hit == 1)
                {
                    you.Add(deck.ElementAt(0).Key, deck.ElementAt(0).Value);
                    deck.Remove(deck.ElementAt(0).Key);
                    deal = false;
                }
                else
                {
                    deal = true;
                }
                print(you, dealer);
                if (calc(you) >= 21)
                {
                    Console.WriteLine("You went over 21. YOU LOOSE");
                    deal = true;
                    game = false;
                }
            }
        }
        while (!deal);

        // dealer mechanism
        do
        {
            miss = false;
            if (game)
            {
                miss = dealing(dealer);
                if (miss)
                {
                    dealer.Add(deck.ElementAt(0).Key, deck.ElementAt(0).Value);
                    deck.Remove(deck.ElementAt(0).Key);
                }
                print(you, dealer);
                if (calc(dealer) > 21)
                {
                    Console.WriteLine("The dealer went over 21. YOU WIN");
                    money += bet * 2;
                    game = false;
                }
            }
        }
        while (miss);
        
        
        // final calculations
        if (game)
        {
            if (calc(dealer) < calc(you))
            {
                Console.WriteLine("YOU WIN!!!");
                money += bet * 2;
                game = false;
            }
            else if (calc(you) < calc(dealer) && game)
            {
                Console.WriteLine("The house wins!");
                game = false;
            }
        }
            


        Console.WriteLine("another round?\n1. YES\n 2. NO\n->");
        another = Convert.ToInt32(Console.ReadLine());
        you.Clear();
        dealer.Clear();
        if (another == 2)
        {
            play = true;
        }
    }
    while (!play);

    return money;
}




Main();