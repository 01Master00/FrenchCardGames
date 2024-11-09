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
    for (int i = 1; i < 14; i++)
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




static int Blackjack(int money)
{
    Dictionary<string, string> dealer = new Dictionary<string, string>();
    Dictionary<string, string> you = new Dictionary<string, string>();
    Dictionary<string, string> deck = Declaration();
    Shuffle(deck);

    Console.Clear();
    Console.WriteLine("Wellcome to Blackjack! Your goal is to have higher cards then the Dealer, but be aware: DON'T go above 21!\nPress anything to continue...\n->");
    Console.ReadLine();
    Console.Clear();

    bool play = false;
    string y = "", d = "";
    do
    {
        for (int i = 0; i < 4; i++)
        {
            if (i % 2 == 0)
            {
                d += $"\n- {deck[deck.ElementAt(0).Key]}";
                deck.Remove(deck.ElementAt(0).Key);
            }
            else
            {
                y += $"\n- {deck[deck.ElementAt(0).Key]}";
                deck.Remove(deck.ElementAt(0).Key);
            }

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
            Thread.Sleep(3000);
            Console.Clear() ;
        }
        Console.ReadLine();


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
        Thread.Sleep(3000);


    }
    while (!play);

    return money;
}




Main();