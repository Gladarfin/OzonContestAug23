namespace OzonContestAug23App;

public class ProblemD
{
    public void Solve()
    {
        var count = int.Parse(Console.ReadLine());
        for (var i = 0; i < count; i++)
        {
            var knm = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            var inputData = new List<char[][]>();
            for (var j = 0; j < knm[0]; j++)
            {
                inputData.Add(new char[knm[1]][]);
                for (var n = 0; n < knm[1]; n++)
                {
                    inputData[j][n] = Console.ReadLine().ToCharArray();
                }
                if (j + 1 < knm[0])
                    Console.ReadLine();
            }

            inputData.Reverse();
            var output = inputData[0];

            foreach (var mount in inputData.Skip(1))
            {
                for (var n = 0; n < mount.Length; n++)
                {
                    for (var m = 0; m < mount[0].Length; m++)
                    {
                        if (mount[n][m] != '.')
                            output[n][m] = mount[n][m];
                    }
                }
            }

            foreach (var row in output)
            {
                Console.WriteLine(row);
            }
            Console.WriteLine();
        }
    }

}