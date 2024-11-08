static void Main()
{

    Declaration();

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
            Console.WriteLine(womp + " " + item);
        }

        
    }   
    return deck;
}



Main();