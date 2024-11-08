static void Main()
{   
    List<string> deck = Declaration();
    Shuffle1(deck);
}

static List<string> Declaration()
{
    List<string> deck = new List<string>();
    List<string> tipe = new List<string>();
    tipe.Add("♥Of Hearts♥");
    tipe.Add("♦Of Diamonds♦");
    tipe.Add("♣Of Clubs♣");
    tipe.Add("♠Of Spades♠");

    string womp;
    for (int i = 1; i < 14; i++)
    {
        switch (i)
        {
            case 11:
                womp = "Jumbo";
                break;
            case 12:
                womp = "Dama";
                break;
            case 13:
                womp = "King";
                break;
            case 14:
                womp = "Ace";
                break;
            default:
                womp = Convert.ToString(i);
                break;
        }
        foreach (var item in tipe)
        {
            deck.Add(womp + " " + item);
        }

        
    }   
    return deck;
}


static List<string> Shuffle1(List<string> deck)
{
    Random random = new Random();
    List<string> help = new List<string>();
    int r;
    for (int i = 0; i < 5; i++)
    {
        while (deck.Count > 0)
        {
            r = random.Next(0, deck.Count);
            help.Add(deck[r]);
            deck.RemoveAt(r);

        }
        while (help.Count > 0)
        {
            r = random.Next(0, help.Count);
            deck.Add(help[r]);
            help.RemoveAt(r);
        }
    }
    return deck;
}


Main();