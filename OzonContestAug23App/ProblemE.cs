namespace OzonContestAug23App;

public class ProblemE
{
    public void Solve()
    {
        var input = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
        var friendsCards = Console.ReadLine()
            .Split(' ')
            .Select((x, i) => new {cards = int.Parse(x), index = i})
            .OrderBy(x => x.cards);
        var cards = new Stack<int>();
        for (var i = input[1]; i > 0; i--)
        {
            cards.Push(i);
        }
        var gifts = new List<(int, int)>();
        var isGifted = false;
        foreach (var friend in friendsCards)
        {
            isGifted = false;
            while (cards.Count > 0 && cards.Peek() <= friend.cards)
            {
                cards.Pop();
            }
    
            if (cards.Count == 0)
                break;
            gifts.Add((friend.index, cards.Pop()));
            isGifted = true;
        }

        if (!isGifted)
        {
            Console.WriteLine(-1);
        }
        else
        {
            Console.WriteLine(string.Join(" ", gifts.OrderBy(x => x.Item1).Select(x => x.Item2).ToArray()));
        }

    }
}