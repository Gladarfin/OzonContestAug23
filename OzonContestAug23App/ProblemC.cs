namespace OzonContestAug23App;

public class ProblemC
{
    public void Solve()
    {
        var count = int.Parse(Console.ReadLine());
        for (var i = 0; i < count; i++)
        {
            var emplCount = int.Parse(Console.ReadLine());
            var cur = new []{15, 30};
            var temperatures = new List<(string, int)>();
            for (var j = 0; j < emplCount; j++)
            {
                var curValue = Console.ReadLine().Split(' ');
                temperatures.Add((curValue[0], int.Parse(curValue[1])));
            }

            foreach (var value in temperatures)
            {
                switch (value.Item1)
                {
                    case ">=" when cur[0] < value.Item2:
                        cur[0] = value.Item2;
                        break;
                    case "<=" when cur[1] > value.Item2:
                        cur[1] = value.Item2;
                        break;
                }

                if (cur[0] > cur[1])
                {
                    Console.WriteLine(-1);
                }
                else
                {
                    Console.WriteLine(cur[0]);
                }
            }
    
            Console.WriteLine();
        }


    }
}