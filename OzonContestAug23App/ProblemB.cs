namespace OzonContestAug23App;

public class ProblemB
{
    public void Solve()
    {
        var originalLine = Console.ReadLine().ToArray();
        var count = int.Parse(Console.ReadLine());

        for (var i = 0; i < count; i++)
        {
            var curString = Console.ReadLine().Split(' ');
            var index = 0;
            for (var j = int.Parse(curString[0]) - 1; j < int.Parse(curString[1]); j++)
            {
                originalLine[j] = curString[2][index];
                index++;
            }
        }
        Console.WriteLine(new string(originalLine));
    }
    
}