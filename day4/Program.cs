using System;

public class Program
{
    public static void Main()
    {
        //var inFile = File.ReadAllLines("./sample.txt");
        var inFile = File.ReadAllLines("./input.txt");

        var cardSize = 5;
        var calledNumbers = inFile[0].Split(',').Select(Int32.Parse).ToList();

        var cardData = inFile.Where(line => line != "" && !line.Contains(',')).ToList()
            .Select((v, i) => new { index = i, value = v })
            .GroupBy(x => x.index / cardSize)
            .Select(x => x.Select(v => v.value.Split(' ',StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToList() ).ToList())
            .ToList();

        var cards = new List<Card>();
        foreach (var cData in cardData)
        {
            cards.Add(new Card(cData,cardSize));
        }

        var winnerFound = false;
        int finalNumber = -1;
        var winnerIndex = -1;
        calledNumbers.ForEach(number =>
        {
            var indexedCards = cards.Select((c, i) => new { card = c, index = i }).ToList();
            foreach (var card in indexedCards)            
            {
                if (winnerFound) { break; }
                card.card.MarkNumber(number);
                if (card.card.CheckWinner())
                {
                    winnerFound = true;
                    finalNumber = number;
                    winnerIndex = card.index;
                }
                
            };            
        });

        Console.WriteLine("Winning Card Index: " + winnerIndex);
        Console.WriteLine("Winning Card Score: " + cards[winnerIndex].SumUnMarkedNumbers());
        Console.WriteLine("Final Score: " + cards[winnerIndex].SumUnMarkedNumbers() * finalNumber);

        
        finalNumber = -1;
        var lastWinnerIndex = -1;
        var lastWinnerFound = false;
        calledNumbers.ForEach(number =>
        {
            var indexedCards = cards.Select((c, i) => new { card = c, index = i }).ToList();
            foreach (var card in indexedCards)
            {
                if (lastWinnerFound) { break; }
                card.card.MarkNumber(number);
                if (card.card.CheckWinner() && !card.card.Winner)
                {                    
                    if (indexedCards.Where( c => !c.card.Winner).Count() == 1)
                    {
                        lastWinnerFound = true;
                        finalNumber = number;
                        lastWinnerIndex = card.index;
                    }
                    card.card.Winner = true;
                }
            };
        });

        Console.WriteLine("Last Winning Card Index: " + lastWinnerIndex);
        Console.WriteLine("Last Winning Card Score: " + cards[lastWinnerIndex].SumUnMarkedNumbers());
        Console.WriteLine("Last Final Score: " + cards[lastWinnerIndex].SumUnMarkedNumbers() * finalNumber);
        return;
    }
}

public class CardEntry
{
    public CardEntry()
    {
        Value = 0;
        Marked = false;
    }
    public int Value { get; set; }     
    public bool Marked { get; set; } 
}

public class Card
{
    private readonly int cardSize;
    public bool Winner { get; set; } = false;
    private CardEntry[,] Numbers { get; set; }
    public Card(List<List<int>> input, int cardSize)
    {
        this.cardSize = cardSize;
        Numbers = new CardEntry[cardSize,cardSize];
        for(int i=0; i< cardSize;i++)
        {
            for(int j=0; j< cardSize;j++)
            {
                Numbers[i,j] = new CardEntry();
                Numbers[i, j].Value = input[i][j];
                Numbers[i, j].Marked = false;
            }
        }
    }

    public void MarkNumber(int number)
    {
        for(int i=0; i < cardSize;i++)
        {
            for( int j=0; j< cardSize;j++)
            {
                if (Numbers[i,j].Value == number)
                {
                    Numbers[i,j].Marked = true;
                }
            }
        }
    }
    
    public bool CheckWinner()
    {
        for(int i = 0;i<cardSize;i++)
        {
            var rowMarked = true;
            var colMarked = true;
            for(int j=0;j< cardSize;j++)
            {
                if (!Numbers[i, j].Marked) { rowMarked = false; }
                if (!Numbers[j, i].Marked) { colMarked = false; }
            }
            if (rowMarked) { return true; }
            if (colMarked) { return true; }
        }
        return false;
    }

    public int SumUnMarkedNumbers()
    {
        var sum = 0;
        for (int i = 0; i < cardSize; i++)
        {
            for (int j = 0; j < cardSize; j++)
            {
                if (!Numbers[i, j].Marked)
                {
                    sum += Numbers[i, j].Value;
                }
            }
        }
        return sum;
    }
}

