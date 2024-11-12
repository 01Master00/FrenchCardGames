
static void Main()
{
    bool end = false;
    int menu, money = 1000;

    Console.WriteLine("Wellcome you'r money is: " + money + " please choose an option:\n1. Blackjack\nOr any other option to exit\n->");
    menu = Convert.ToInt32(Console.ReadLine());

    do
    {

        switch (menu)
        {
            case 1:
                Blackjack(money);
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
    for (int i = 2; i < 14; i++)
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
    for (int i = 0; i < 5; i++)
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


static int Blackjack(int money)
{
    Dictionary<string, string> dealer = new Dictionary<string, string>();
    Dictionary<string, string> you = new Dictionary<string, string>();
    Dictionary<string, string> deck = Declaration();
    Shuffle(deck);
    int dhand, yhand, bet, another, hit;
    Console.Clear();
    Console.WriteLine("Wellcome to Blackjack! Your goal is to have higher cards then the Dealer, but be aware: DON'T go above 21!\nPress anything to continue...\n->");
    Console.ReadLine();
    Console.Clear();

    bool play = false, game = false, deal = false;
    string y = "", d = "";
    do
    {
        Console.WriteLine($"give me your bet, you'r money is: {money}\n->");
        bet = Convert.ToInt32(Console.ReadLine());
        if (money - bet < 0 || bet < 0)
        {
            money -= bet;
            game = true;
        }
        do
        {
            you.Clear();
            dealer.Clear();

            //card dealing
            for (int i = 0; i < 4; i++)
            {
                if (i % 2 == 0)
                {
                    dealer.Add(deck.ElementAt(0).Key, deck.ElementAt(0).Value);
                    d += $"\n- {deck[deck.ElementAt(0).Key]}";
                    deck.Remove(deck.ElementAt(0).Key);
                }
                else
                {
                    you.Add(deck.ElementAt(0).Key, deck.ElementAt(0).Value);
                    y += $"\n- {deck[deck.ElementAt(0).Key]}";
                    deck.Remove(deck.ElementAt(0).Key);
                }

                Console.Clear();
                Console.WriteLine($"""
                ---------
                | o   o |
                |   _   |
                |  ---  |
                ---------
                Dealer's hand: {d}
            

                  /------\
                 /        \
                |  o    o |
                |   \-/   | 
                |         |
                 \ \___/  /
                  \______/
                 Your hand: {y}


                """);
                Thread.Sleep(2000);
            }

            dhand = calc(dealer);
            yhand = calc(you);

            //showing cards
            Console.Clear();
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

            // BLACKJACK detection
            if (dhand == 21)
            {
                Console.WriteLine("dealer wins with BLACKJACK!");
                game = false;
            }
            else if (yhand == 21)
            {
                Console.WriteLine("You win with BLACKJACK!");
                game = false;
                money += Convert.ToInt32(bet * 2.5);
            }

            do
            {
                Console.WriteLine("Do you want any adictional cards?\n1. YES (hit)\n2. NO (stand)\n->");
                hit = Convert.ToInt32(Console.ReadLine());
                if (hit == 1)
                {
                    you.Add(deck.ElementAt(0).Key, deck.ElementAt(0).Value);
                    y += $"\n- {deck[deck.ElementAt(0).Key]}";
                    deck.Remove(deck.ElementAt(0).Key);
                }
                else
                {
                    deal = true;
                }
                dhand = calc(dealer);
                yhand = calc(you);
                Console.Clear();
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
            }
            while (!deal);
        } while (game);

        Console.WriteLine("another round?\n1. YES\n 2. NO\n->");
        another = Convert.ToInt32(Console.ReadLine());
        if (another == 2)
        {
            play = true;
        }
    }
    while (!play);

    return money;
}




Main();
